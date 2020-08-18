using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Internal;
using System.Linq;
using WebApi.Extensions.InvitationKey;

namespace WebApi.Controllers
{
    [Route("invitation-key")]
    [ApiController]
    public class InvitationKeyController : ControllerBase
    {

        private readonly AppDbContext _context;

        public InvitationKeyController(DbContextOptions<AppDbContext> options)
        {
            _context = new AppDbContext(options);
        }

        [HttpGet]
        [Route("{key}")]
        public async Task<ActionResult<InvitationKey>> Get(string key)
        {
            if (key.IsValid())
            {
                var foundKeyObj = await _context.InvitationKeys
                    .AsQueryable()
                    .Include(c => c.Inviter)
                    .Include(c => c.UsedByUser)
                    .SingleOrDefaultAsync(c => c.Key == key);
                if (foundKeyObj != null)
                    return foundKeyObj;
                else return BadRequest("Given invitation key was not found.");
            }
            else return BadRequest("Given invitation key is invalid.");
        }

    }
}
