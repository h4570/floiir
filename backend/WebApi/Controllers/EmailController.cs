using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Builders;
using WebApi.Extensions;
using WebApi.Misc.Auth;

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
            var i18n = HttpContext.GetUserLanguage().CreateFactory();
            var director = new EmailDirector(builder, i18n);
            var emailHtml = director.GetConfirmEmailEmailHtml(HttpContext.GetUser());
            return new ContentResult
            {
                ContentType = "text/html",
                Content = emailHtml
            };
        }

    }
}