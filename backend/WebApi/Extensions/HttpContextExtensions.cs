using Microsoft.AspNetCore.Http;
using System;
using WebApi.Misc.i18n;
using WebApi.Models.Internal;

namespace WebApi.Extensions
{
    public static class HttpContextExtensions
    {

        /// <summary>
        /// Return's user of current request
        /// Works only with [Authorize] attribute from JWT Middleware
        /// </summary>
        /// <exception cref="UserObjectNotFoundException">When request is not authorized via JWT Middleware</exception>
        public static User GetUser(this HttpContext context)
        {
            if (context.Items.ContainsKey("User"))
                return (User)context.Items["User"];
            else throw new UserObjectNotFoundException("User item was not added by JWT Middleware. Did you used PROPER [Authorize] attribute?");
        }

        /// <summary>
        /// Return's user preffered language based on User-Language request header
        /// </summary>
        /// <exception cref="LanguageNotSupportedException">When value of header have unknown language</exception>
        /// <exception cref="LanguageHeaderNotFoundException">When User-Language header was not found</exception>
        public static Language GetUserLanguage(this HttpContext context)
        {
            var headers = context.Request.Headers;
            if (headers.ContainsKey("User-Language"))
            {
                var lang = headers["User-Language"].ToString();
                return lang switch
                {
                    "pl_PL" => Language.PL,
                    "en_US" => Language.EN,
                    _ => throw new LanguageNotSupportedException("Specified language is not supported! (User-Language header)")
                };
            }
            else throw new LanguageHeaderNotFoundException("User-Language key was not found in request headers!");
        }

    }

    public class UserObjectNotFoundException : Exception { public UserObjectNotFoundException(string text) : base(text) { } }
    public class LanguageNotSupportedException : Exception { public LanguageNotSupportedException(string text) : base(text) { } }
    public class LanguageHeaderNotFoundException : Exception { public LanguageHeaderNotFoundException(string text) : base(text) { } }

}
