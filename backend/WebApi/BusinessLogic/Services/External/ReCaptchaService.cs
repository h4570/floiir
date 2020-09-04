using RestSharp;
using System.Net;
using System.Threading.Tasks;
using WebApi.Models.External;

namespace WebApi.BusinessLogic.Services.External
{
    public class ReCaptchaService
    {

        /// <exception cref="WebException">When request to google servers fail</exception>
        public async Task<bool> IsReCaptchaSucceed(string token, string secret, string host)
        {
            var client = new RestClient("https://www.google.com");
            var request = new RestRequest("recaptcha/api/siteverify", Method.GET);
            request.AddParameter("secret", secret, ParameterType.QueryString);
            request.AddParameter("response", token, ParameterType.QueryString);
            request.AddParameter("remoteip", host, ParameterType.QueryString);
            var res = await client.ExecuteAsync<ReCaptchaResponse>(request);
            if (res.IsSuccessful) return res.Data.Success;
            else throw new WebException("ReCaptcha - Request to google servers failed.");
        }

    }

}
