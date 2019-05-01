using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempFileServer.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TempFileServer.Controllers
{
	[Route("api/pack")]
    [ApiController]
    public class PackController : ControllerBase
    {
        private readonly SystemContext _context;

        public PackController(SystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Pack[]>> Get()
        {
            return Ok(await _context.Packs.ToArrayAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pack>> Get(int id)
        {
            return Ok(await _context.Packs.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Pack>> Create(Pack pack)
        {
            EntityEntry<Pack> created = await _context.Packs.AddAsync(pack);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(Get), new { id = created.Entity.id }, created.Entity);
        }

        [HttpPatch]
        public async Task<ActionResult<Pack>> Update(Pack pack)
        {
            Pack updated = await Task.Run(() => 
            {
                return _context.Packs.Update(pack).Entity;
            });

            await _context.SaveChangesAsync();

            return Ok(updated);
        }

		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(Pack), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(int id)
		{
			Pack removed = await Task.Run(() =>
			{
				Pack target = _context.Packs.Find(id);

				if (target != null)
					_context.Packs.Remove(target);

				return target;
			}); ;

			if (removed == null)
				return NotFound();

			return Ok(removed);
		}

		[HttpPost("search-packs/{search}")]
		public async Task<ActionResult<List<Pack>>> Search(string search)
		{
			return await _context.Packs.Where(p => p.name.Contains(search) && p.main_pack == 1).ToListAsync();
		}
    }
}