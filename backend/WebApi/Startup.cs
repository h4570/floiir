using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using WebApi.Misc.Database;
using WebApi.Misc.Auth;

namespace WebApi
{

    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var config = Configuration.GetSection("configuration").Get<Configuration>();

            ConfigEnvironment env = config.Dev;
            string envName = "dev";

            if (Environment.IsProduction())
            {
                env = config.Prd;
                envName = "prd";
            }

            var entityConnectionString = env.Connections.Postgresql;

            services.AddDbContext<AppDbContext>(options => { options.UseNpgsql(entityConnectionString); });

            services.AddCors(options =>
                        options.AddDefaultPolicy(builder =>
                        builder.WithOrigins(env.Urls.Main).AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("x-auth-token")
                        )
                    );
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<ConfigEnvironment>(Configuration.GetSection("configuration:" + envName));
            services.AddMvc().AddNewtonsoftJson();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
                dbInitializer.Initialize();
                dbInitializer.SeedData();
            }

            if (!Environment.IsProduction())
                app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseMiddleware<JwtMiddleware>();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

    }

}
