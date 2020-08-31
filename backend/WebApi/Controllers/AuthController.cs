using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebApi.Dtos.Internal;
using WebApi.Extensions;
using WebApi.Misc.Auth;

namespace WebApi.Controllers
{

    [EnableCors]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly string _privateKey;
        private readonly string _salt;

        public AuthController(DbContextOptions<AppDbContext> options, IOptions<ConfigEnvironment> config)
        {
            _context = new AppDbContext(options);
            _privateKey = config.Value.PrivateKey;
            _salt = config.Value.Salt;
        }

        /// <summary>
        /// TODO. Prototype of login endpoint
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<LoginSuccessResponseDto>> Authorize([FromBody] LoginPasswordDto payload)
        {
            payload.TrimProperties();
            var hash = AuthUtilities.ComputeSha256Hash(payload.Password, _salt);
            var user = await _context.Users.SingleOrDefaultAsync(c => c.Login.Equals(payload.Login) && c.Password.Equals(hash));
            if (user != null)
            {
                var jwt = AuthUtilities.GenerateJWTToken(_privateKey, user.Id);
                var res = new LoginSuccessResponseDto(user, jwt);
                return Ok(res);
            }
            else return StatusCode(410, "Login failed.");
        }

    }
}