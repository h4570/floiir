using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebApi.BusinessLogic.Facades;
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
        private readonly UserFacade _userFacade;
        private readonly string _privateKey;

        public AuthController(DbContextOptions<AppDbContext> options, IOptions<ConfigEnvironment> config)
        {
            _context = new AppDbContext(options);
            _privateKey = config.Value.PrivateKey;
            _userFacade = new UserFacade(_context, config.Value);

        }

        /// <summary>
        /// TODO. Prototype of login endpoint
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<LoginSuccessResponseDto>> Authorize([FromBody] LoginPasswordDto payload)
        {
            var host = Request.Headers.GetHost();
            var result = await _userFacade.Login(payload, host);
            HttpContext.Response.Headers.Add("x-auth-token", $"{result.Data.Token}");
            return Ok(result);
           
        }

    }
}