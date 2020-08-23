using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebApi.Dtos.Internal;
using WebApi.Extensions;

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

        [HttpPost]
        public async Task<ActionResult<AuthenticateResponseDto>> Authorize([FromBody] AuthenticateRequestDto payload)
        {
            payload.TrimProperties();
            var hash = Utilities.ComputeSha256Hash(payload.Password, _salt);
            var user = await _context.Users.SingleOrDefaultAsync(c => c.Login.Equals(payload.Login) && c.Password.Equals(hash));
            if (user != null)
            {
                var jwt = Utilities.GenerateJWTToken(_privateKey, user.Id);
                var res = new AuthenticateResponseDto(user, jwt);
                return Ok(res);
            }
            else return StatusCode(410, "Login failed.");
        }

    }
}