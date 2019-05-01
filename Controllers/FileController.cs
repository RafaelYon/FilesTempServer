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
    [Route("api/pack/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly SystemContext _context;

        public FileController(SystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<File[]>> Get()
        {
            return Ok(await _context.Files.ToArrayAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<File>> Get(int id)
        {
            return Ok(await _context.Files.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<UploadResponse>> Create(File file)
        {
            file.file_name = $"{Guid.NewGuid()}{System.IO.Path.GetExtension(file.file_name)}";
            
            EntityEntry<File> created = await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = created.Entity.id }, new UploadResponse
            {
                upload_src = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", created.Entity.file_name),
                file_data = created.Entity
            });
        }

        [HttpPatch]
        public async Task<ActionResult<File>> Update(File file)
        {
            File updated = await Task.Run(() => 
            {
                return _context.Update(file).Entity;
            });

            await _context.SaveChangesAsync();

            return Ok(updated);
        }

		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(File), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(int id)
		{
			File deleted = await Task.Run(() => 
			{
				File target = _context.Files.Find(id);

				if (target != null)
					_context.Files.Remove(target);

				return target;
			});

			if (deleted == null)
				return NotFound();

			return Ok(deleted);
		}
    }
}