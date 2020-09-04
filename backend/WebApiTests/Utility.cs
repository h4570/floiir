using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using WebApi;

namespace WebApiTests
{

    public class ConfigurationWrapper { public Configuration Configuration { get; set; } }

    public static class Utility
    {
        private static Random random = new Random();

        public static string RandomStringGenerator(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static ConfigEnvironment GetConfig()
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\appsettings.json";
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ConfigurationWrapper>(json).Configuration.Dev;
        }

    }
}
