

namespace WebApi
{

    public class Configuration
    {
        public ConfigEnvironment Dev { get; set; }
        public ConfigEnvironment Prd { get; set; }
    }

    public class ConfigEnvironment
    {
        public Urls Urls { get; set; }
        public Connections Connections { get; set; }
    }

    public class Urls
    {
        public string Main { get; set; }
    }

    public class Connections
    {
        public string Main { get; set; }
    }

}
