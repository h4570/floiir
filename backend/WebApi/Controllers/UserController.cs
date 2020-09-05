using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Internal;
using Microsoft.Extensions.Options;
using WebApi.Extensions;
using WebApi.Dtos.Internal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using WebApi.Misc.Auth;
using System;
using WebApi.BusinessLogic.Services.Internal;
using WebApi.BusinessLogic.Services.External;
using WebApi.BusinessLogic.Factories.i18n;
using WebApi.BusinessLogic.Facades;

namespace WebApi.Controllers
{

    [EnableCors]
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly UserFacade _userFacade;
        private readonly ConfigEnvironment _config;

        public UserController(DbContextOptions<AppDbContext> options, IOptions<ConfigEnvironment> config)
        {
            _config = config.Value;
            _context = new AppDbContext(options);
            _userFacade = new UserFacade(_context, _config);
        }

        /// <summary>
        /// Used for user registration.
        /// On successfull registration, JWT auth token is added to response headers as 'x-auth-token'
        /// </summary>
        /// <param name="payload">Dto which have new user model, reCatpcha token and invitation key</param>
        /// <returns>
        /// 460 - when invitation key is invalid (Constants.INV_KEY_LENGTH), 
        /// 461 - when invitation key was not found in database, 
        /// 462 - when invitation key was used by another user, 
        /// 463 - when new user object properties are invalid, 
        /// 464 - when given email from new user object is already used, 
        /// 465 - when given login from new user object is already used, 
        /// 499 - when reCaptcha validation failed, 
        /// 200 - when user was created. JWT token response header is added here.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<IUser>> Post([FromBody] UserRegisterDto payload)
        {
            var host = Request.Headers.GetHost();
            var i18n = HttpContext.GetUserLanguage().CreateFactory();
            var result = await _userFacade.Register(payload, i18n, host);
            if (result.Succeed)
            {
                var newUser = result.Data;
                var jwt = AuthUtilities.GenerateJWTToken(_config.PrivateKey, newUser.Id);
                HttpContext.Response.Headers.Add("x-auth-token", $"{jwt}");
                newUser.Password = null;
                return Ok(newUser);
            }
            else return StatusCode(result.FailStatusCode, result.FailStatusMessage);
        }

    }
}
