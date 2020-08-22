using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApi.Models.Internal;

namespace WebApi
{

    public interface IDbInitializer
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Adds some default values to the Db
        /// </summary>
        void SeedData();
    }

    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            context.Database.Migrate();
        }

        public void SeedData()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            if (!context.Users.Any())
            {
                var appAdmin = new User
                {
                    Id = 1,
                    Login = "app-admin",
                    Password = "no-password",
                    Email = "admin@flooir.com",
                    FirstName = "Floiir",
                    LastName = "Admin"
                };
                context.Users.Add(appAdmin);
            }

            if (!context.InvitationKeys.Any())
            {
                var invKeyDate = new DateTime(2005, 4, 2, 21, 37, 00);
                var firstInvKey = new InvitationKey
                {
                    Id = Constants.FIRST_INV_KEY_ID,
                    InviterId = Constants.APP_ADMIN_ID,
                    CreatedAt = invKeyDate,
                    Key = "1234567890",
                };
                context.InvitationKeys.Add(firstInvKey);
            }

            context.SaveChanges();
        }
    }
}
