using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Internal;
using System.Linq;
using WebApi.Services.Internal;
using Microsoft.Extensions.Options;
using WebApi.Extensions;
using WebApi.Dtos.Internal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using WebApi.Services.External;
using WebApi.Misc.Http;
using WebApi.Misc.Auth;
using WebApi.Factories.i18n;
using System;

namespace WebApi.Controllers
{

    [EnableCors]
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly UserService _userService;
        private readonly EmailService _emailService;
        private readonly ReCaptchaService _reCaptchaService;
        private readonly string _salt;
        private readonly string _privateKey;
        private readonly string _reCaptchaSecret;

        public UserController(DbContextOptions<AppDbContext> options, IOptions<ConfigEnvironment> config)
        {
            _context = new AppDbContext(options);
            _userService = new UserService(_context);
            _emailService = new EmailService(EmailType.BlueGray);
            _reCaptchaService = new ReCaptchaService();
            _salt = config.Value.Salt;
            _privateKey = config.Value.PrivateKey;
            _reCaptchaSecret = config.Value.ReCaptchaSecret;
        }

        /// <summary>
        /// Used for user registration.
        /// On successfull registration, JWT auth token is added to response headers as 'x-auth-token'
        /// </summary>
        /// <param name="payload">Dto which have new user model, reCatpcha token and invitation key</param>
        /// <returns>
        /// 400 - when body payload is wrong or language header was not found
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
        public async Task<ActionResult<User>> Post([FromBody] UserRegisterDto payload)
        {
            var host = HttpUtilities.GetHostFromRequestHeaders(Request.Headers);
            I18nFactory i18n;
            try { i18n = HttpContext.GetUserLanguage().CreateFactory(); } catch (Exception ex) { return BadRequest(ex.Message); }
            var reCaptchaSucceed = await _reCaptchaService.IsReCaptchaSucceed(payload.ReCaptchaToken, _reCaptchaSecret, host);
            if (reCaptchaSucceed)
            {
                var payloadUser = payload.User;
                payloadUser.TrimProperties();
                if (payload.InvitationKey.IsInvKeyValid())
                {
                    var foundKeyObj = await _context.InvitationKeys.AsQueryable().SingleOrDefaultAsync(c => c.Key == payload.InvitationKey);
                    if (foundKeyObj != null)
                        if (foundKeyObj.UsedByUserId == null)
                            if (payloadUser.IsValid())
                                if (!_userService.EmailExists(payloadUser.Email))
                                    if (!_userService.LoginExists(payloadUser.Login))
                                    {
                                        var newUser = payloadUser.ToUser();
                                        newUser.Password = AuthUtilities.ComputeSha256Hash(payloadUser.Password, _salt);
                                        payloadUser.Password = null;
                                        await _context.Users.AddAsync(newUser);
                                        await _context.SaveChangesAsync();
                                        foundKeyObj.UsedByUserId = newUser.Id;
                                        await _context.SaveChangesAsync();
                                        _emailService.SendConfirmEmailEmail(newUser, i18n);
                                        var jwt = AuthUtilities.GenerateJWTToken(_privateKey, newUser.Id);
                                        HttpContext.Response.Headers.Add("x-auth-token", $"{jwt}");
                                        return Ok(payloadUser);
                                    }
                                    else return StatusCode(465, "There is already user with this login.");
                                else return StatusCode(464, "There is already user with this email.");
                            else return StatusCode(463, "User properties validation failed.");
                        else return StatusCode(462, "Given invitation key was used.");
                    else return StatusCode(461, "Given invitation key was not found.");
                }
                else return StatusCode(460, "Given invitation key is invalid.");
            }
            else return StatusCode(499, "ReCaptcha failed.");
        }

    }
}
