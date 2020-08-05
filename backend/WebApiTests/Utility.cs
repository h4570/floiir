using Newtonsoft.Json;
using System.IO;
using WebApi;

namespace WebApiTests
{

    public class ConfigurationWrapper { public Configuration Configuration { get; set; } }

    public static class Utility
    {

        public static ConfigEnvironment GetConfig()
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\appsettings.json";
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ConfigurationWrapper>(json).Configuration.Dev;
        }

    }
}
