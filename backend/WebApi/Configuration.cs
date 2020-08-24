

namespace WebApi
{

    public class Configuration
    {
        public ConfigEnvironment Dev { get; set; }
        public ConfigEnvironment Prd { get; set; }
    }

    public class ConfigEnvironment
    {
        public string ReCaptchaSecret { get; set; }
        public string Salt { get; set; }
        public string PrivateKey { get; set; }
        public Urls Urls { get; set; }
        public Connections Connections { get; set; }
    }

    public class Urls
    {
        public string Main { get; set; }
    }

    public class Connections
    {
        public string Postgresql { get; set; }
        public string Mongodb { get; set; }
    }

}
