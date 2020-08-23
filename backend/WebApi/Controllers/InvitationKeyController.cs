using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Internal;
using System.Linq;
using WebApi.Extensions;
using System.Collections.Generic;
using WebApi.Misc;
using Microsoft.AspNetCore.Cors;

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
        /// Used for checking invitation key in frontend (register component)
        /// </summary>
        /// <param name="key">Valid key should be created in database and have x chars length</param>
        /// <returns>410 when key is invalid, 420 when key was not found</returns>
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
