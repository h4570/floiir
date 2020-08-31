using System;

namespace WebApi.Models.External
{
    public class ReCaptchaResponse
    {
        public bool Success { get; set; }
        public string[] Errorcodes { get; set; }
        public DateTime Challenge_ts { get; set; }
        public string Hostname { get; set; }
    }
}
