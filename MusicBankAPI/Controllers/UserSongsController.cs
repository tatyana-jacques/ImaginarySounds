using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBankAPI.Context;
using MusicBankAPI.Models;

namespace MusicBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSongsController : ControllerBase
    {
        private readonly MusicBankContext _context;

        public UserSongsController(MusicBankContext context)
        {
            _context = context;
        }

        // GET: api/UserSongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSongsViewModel>>> GetUserSongs()
        {
          if (_context.UserSongs == null)
          {
              return NotFound();
          }
            return await _context.UserSongs.ToListAsync();
        }

        // GET: api/UserSongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSongsViewModel>> GetUserSongs(int id)
        {
          if (_context.UserSongs == null)
          {
              return NotFound();
          }
            var userSongs = await _context.UserSongs.FindAsync(id);

            if (userSongs == null)
            {
                return NotFound();
            }

            return userSongs;
        }

        // PUT: api/UserSongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSongs(int id, UserSongsViewModel userSongs)
        {
            if (id != userSongs.Id)
            {
                return BadRequest();
            }

            _context.Entry(userSongs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSongsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserSongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserSongsViewModel>> PostUserSongs(UserSongsViewModel userSongs)
        {
          if (_context.UserSongs == null)
          {
              return Problem("Entity set 'MusicBankContext.UserSongs'  is null.");
          }
            _context.UserSongs.Add(userSongs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserSongs", new { id = userSongs.Id }, userSongs);
        }

        // DELETE: api/UserSongs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSongs(int id)
        {
            if (_context.UserSongs == null)
            {
                return NotFound();
            }
            var userSongs = await _context.UserSongs.FindAsync(id);
            if (userSongs == null)
            {
                return NotFound();
            }

            _context.UserSongs.Remove(userSongs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserSongsExists(int id)
        {
            return (_context.UserSongs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
