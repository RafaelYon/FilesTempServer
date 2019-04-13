using System;
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
    }
}