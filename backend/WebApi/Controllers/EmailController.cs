using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApi.Builders;
using WebApi.Extensions;
using WebApi.Misc.Auth;
using WebApi.Models.Internal;

namespace WebApi.Controllers
{

    [EnableCors]
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {

        /// <summary>
        /// TODO. Prototype of login endpoint
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ContentResult Test()
        {
            var builder = new BlueGrayEmailBuilder();
            var director = new EmailDirector(builder);
            // TODO i18n
            var emailHtml = director.GetConfirmEmailEmailHtml(HttpContext.GetUserLanguage(), HttpContext.GetUser());
            return new ContentResult
            {
                ContentType = "text/html",
                Content = emailHtml
            };
        }

    }
}