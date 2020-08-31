using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Internal;
using System.Linq;
using WebApi.Extensions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using WebApi.Misc.Auth;

namespace WebApi.Controllers
{

    [EnableCors]
    [ApiController]
    [Route("invitation-key")]
    public class InvitationKeyController : ControllerBase
    {

        private readonly AppDbContext _context;

        public InvitationKeyController(DbContextOptions<AppDbContext> options)
        {
            _context = new AppDbContext(options);
        }

        [Authorize]
        [HttpGet("/invitation-keys")]
        public async Task<ActionResult<IEnumerable<InvitationKey>>> GetAll() // TODO REMOVE
        {
            User user = (User)HttpContext.Items["User"];
            return Ok(await _context.InvitationKeys.AsQueryable().ToListAsync());
        }

        /// <summary>
        /// Used for checking invitation key before registration
        /// </summary>
        /// <param name="key">Invitation key</param>
        /// <returns>
        /// 460 - when key is invalid (Constants.INV_KEY_LENGTH), 
        /// 461 - when key was not found in database
        /// </returns>
        [HttpGet]
        [Route("{key}")]
        public async Task<ActionResult<InvitationKey>> Get(string key)
        {
            if (key.IsInvKeyValid())
            {
                var foundKeyObj = await _context.InvitationKeys
                    .AsQueryable()
                    .Include(c => c.Inviter)
                    .Include(c => c.UsedByUser)
                    .SingleOrDefaultAsync(c => c.Key == key);
                if (foundKeyObj != null)
                    return foundKeyObj;
                else return StatusCode(461, "Given invitation key was not found.");
            }
            else return StatusCode(460, "Given invitation key is invalid.");
        }

    }
}
