using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempFileServer.Models;

namespace TempFileServer.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SystemContext _context;

        public UserController(SystemContext context)
        {
            _context = context;

            if (_context.Users.Count() < 1)
            {
                _context.Users.Add(new User { email = "p@email.com", password = "password" });
                _context.SaveChanges();
            }
        }

		private LoginToken CreateFakeLoginToken()
		{
			return new LoginToken
			{
				access_token = $"acess_token#{DateTime.Now.ToShortDateString()}",
				token_type = "fake",
				expires_in = 10,
				access_code = "1010"
			};
		}

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginToken), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                await _context.Users.Where(u => u.email == user.email && u.password == user.password).FirstAsync();

                return Ok(CreateFakeLoginToken());
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

		[HttpPost("refresh")]
		[ProducesResponseType(typeof(LoginToken), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public IActionResult Refresh()
		{
			if (!Request.Headers["Authorization"].Contains("acess_token#"))
				return Unauthorized();

			return Ok(CreateFakeLoginToken());
		}
    }
}