using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace WebApi.Controllers
{

    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public sealed class AppInfoAttribute : Attribute
    {
        public string Version { get; }
        public string Build { get; }
        public AppInfoAttribute(string version, string build)
        {
            Version = version;
            Build = build;
        }
    }

    [EnableCors]
    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {

        [HttpGet("/app-info")]
        public ActionResult<ActionResult<object>> GetAppInfo()
        {
            var appInfo = Assembly.GetEntryAssembly().GetCustomAttribute<AppInfoAttribute>();
            return Ok(new { version = appInfo.Version, build = appInfo.Build });
        }

    }

}