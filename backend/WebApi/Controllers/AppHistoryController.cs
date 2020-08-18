using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Internal;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("app-history")]
    [ApiController]
    public class AppHistoryController : ControllerBase
    {

        private readonly AppDbContext _context;

        public AppHistoryController(DbContextOptions<AppDbContext> options)
        {
            _context = new AppDbContext(options);
        }

        [HttpGet]
        [Route("table/{tableId}")]
        public async Task<ActionResult<List<AppHistory>>> GetAll(int tableId)
        {
            var result = new List<AppHistory>()
            {
                new AppHistory()
                {
                    Id = 1,
                    DateTime = DateTime.Now,
                    TableId = (AppTable)tableId,
                    Type = AppHistoryType.Add,
                    UserId = 12
                }
            };
            return Ok(result);
            //return await _context
            //    .AppHistory
            //    .AsQueryable()
            //    .Where(c => c.TableId == tableId)
            //    .ToListAsync();
        }

        [HttpGet]
        [Route("table/{tableId}/element/{elementId}")]
        public async Task<ActionResult<List<AppHistory>>> GetAll(int tableId, int elementId)
        {
            return await _context
                .AppHistory
                .AsQueryable()
                .Where(c => c.TableId == (AppTable)tableId && c.ElementId == elementId)
                .ToListAsync();
        }

    }
}
