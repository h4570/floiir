using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.External;

namespace WebApi.Services.External
{
    public class ReCaptchaService
    {

        public async Task<bool> IsReCaptchaSucceed(string token, string secret, string host)
        {
            var client = new RestClient("https://www.google.com");
            var request = new RestRequest("recaptcha/api/siteverify", Method.GET);
            request.AddParameter("secret", secret, ParameterType.QueryString);
            request.AddParameter("response", token, ParameterType.QueryString);
            request.AddParameter("remoteip", host, ParameterType.QueryString);
            var res = await client.ExecuteAsync<ReCaptchaResponse>(request);
            if (res.IsSuccessful) return res.Data.Success;
            else throw new Exception("ReCaptcha test - Request to google servers failed.");
        }

    }
}
