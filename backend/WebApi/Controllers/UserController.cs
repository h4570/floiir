using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Internal;
using System.Linq;
using WebApi.Extensions.InvitationKey;
using WebApi.Extensions.User;
using WebApi.Services.Internal;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly UserService _userService;
        private readonly string _salt;

        public UserController(DbContextOptions<AppDbContext> options, IOptions<ConfigEnvironment> config)
        {
            _context = new AppDbContext(options);
            _userService = new UserService(_context);
            _salt = config.Value.Salt;
        }

        /// <summary>
        /// Used for checking invitation key in frontend (register component)
        /// </summary>
        /// <param name="key">Valid key should be created in database and have x chars length</param>
        /// <returns>410 when key is invalid, 420 when key was not found</returns>
        [HttpPost]
        [Route("{key}")]
        public async Task<ActionResult<User>> Post([FromBody] User payload, string key)
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
                                    payload.UserPassword.Password = Utilities.ComputeSha256Hash(payload.UserPassword.Password, _salt);
                                    await _context.Users.AddAsync(payload);
                                    await _context.SaveChangesAsync();
                                    foundKeyObj.UsedByUserId = payload.Id;
                                    await _context.SaveChangesAsync();
                                    return Ok(payload);
                                }
                                else return StatusCode(460, "There is already user with this login.");
                            else return StatusCode(450, "There is already user with this email.");
                        else return StatusCode(440, "User properties validation failed.");
                    else return StatusCode(430, "Given invitation key was used.");
                else return StatusCode(420, "Given invitation key was not found.");
            }
            else return StatusCode(410, "Given invitation key is invalid.");
        }

    }
}
