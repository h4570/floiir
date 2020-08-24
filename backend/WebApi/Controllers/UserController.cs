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

namespace WebApi.Controllers
{

    [EnableCors]
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly UserService _userService;
        private readonly ReCaptchaService _reCaptchaService;
        private readonly string _salt;
        private readonly string _privateKey;
        private readonly string _reCaptchaSecret;

        public UserController(DbContextOptions<AppDbContext> options, IOptions<ConfigEnvironment> config)
        {
            _context = new AppDbContext(options);
            _userService = new UserService(_context);
            _reCaptchaService = new ReCaptchaService();
            _salt = config.Value.Salt;
            _privateKey = config.Value.PrivateKey;
            _reCaptchaSecret = config.Value.ReCaptchaSecret;
        }

        /// <summary>
        /// Used for checking invitation key in frontend (register component)
        /// </summary>
        /// <param name="key">Valid key should be created in database and have x chars length</param>
        /// <returns>410 when key is invalid, 420 when key was not found</returns>
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] UserRegisterDto payload)
        {
            var host = Utilities.GetHostFromRequestHeaders(Request.Headers);
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
                                        newUser.Password = Utilities.ComputeSha256Hash(payloadUser.Password, _salt);
                                        payloadUser.Password = null;
                                        await _context.Users.AddAsync(newUser);
                                        await _context.SaveChangesAsync();
                                        foundKeyObj.UsedByUserId = newUser.Id;
                                        await _context.SaveChangesAsync();
                                        var jwt = Utilities.GenerateJWTToken(_privateKey, newUser.Id);
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
