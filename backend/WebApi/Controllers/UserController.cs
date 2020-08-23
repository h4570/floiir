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

namespace WebApi.Controllers
{

    [EnableCors]
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly UserService _userService;
        private readonly string _salt;
        private readonly string _privateKey;

        public UserController(DbContextOptions<AppDbContext> options, IOptions<ConfigEnvironment> config)
        {
            _context = new AppDbContext(options);
            _userService = new UserService(_context);
            _salt = config.Value.Salt;
            _privateKey = config.Value.PrivateKey;
        }

        /// <summary>
        /// Used for checking invitation key in frontend (register component)
        /// </summary>
        /// <param name="key">Valid key should be created in database and have x chars length</param>
        /// <returns>410 when key is invalid, 420 when key was not found</returns>
        [HttpPost]
        [Route("{key}")]
        public async Task<ActionResult<User>> Post([FromBody] PayloadUserDto payload, string key)
        {
            payload.TrimProperties();
            if (key.IsInvKeyValid())
            {
                var foundKeyObj = await _context.InvitationKeys.AsQueryable().SingleOrDefaultAsync(c => c.Key == key);
                if (foundKeyObj != null)
                    if (foundKeyObj.UsedByUserId == null)
                        if (payload.IsValid())
                            if (!_userService.EmailExists(payload.Email))
                                if (!_userService.LoginExists(payload.Login))
                                {
                                    var newUser = payload.ToUser();
                                    newUser.Password = Utilities.ComputeSha256Hash(payload.Password, _salt);
                                    payload.Password = null;
                                    await _context.Users.AddAsync(newUser);
                                    await _context.SaveChangesAsync();
                                    foundKeyObj.UsedByUserId = newUser.Id;
                                    await _context.SaveChangesAsync();
                                    var jwt = Utilities.GenerateJWTToken(_privateKey, newUser.Id);
                                    HttpContext.Response.Headers.Add("x-auth-token", $"{jwt}");
                                    return Ok(payload);
                                }
                                else return StatusCode(465, "There is already user with this login.");
                            else return StatusCode(464, "There is already user with this email.");
                        else return StatusCode(463, "User properties validation failed.");
                    else return StatusCode(462, "Given invitation key was used.");
                else return StatusCode(461, "Given invitation key was not found.");
            }
            else return StatusCode(460, "Given invitation key is invalid.");
        }

    }
}
