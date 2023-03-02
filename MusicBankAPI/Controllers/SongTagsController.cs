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
    public class SongTagsController : ControllerBase
    {
        private readonly MusicBankContext _context;

        public SongTagsController(MusicBankContext context)
        {
            _context = context;
        }

        // GET: api/SongTags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongTags>>> GetSongTags()
        {
          if (_context.SongTags == null)
          {
              return NotFound();
          }
            return await _context.SongTags.ToListAsync();
        }

        // GET: api/SongTags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SongTags>> GetSongTags(int id)
        {
          if (_context.SongTags == null)
          {
              return NotFound();
          }
            var songTags = await _context.SongTags.FindAsync(id);

            if (songTags == null)
            {
                return NotFound();
            }

            return songTags;
        }

        // PUT: api/SongTags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSongTags(int id, SongTags songTags)
        {
            if (id != songTags.Id)
            {
                return BadRequest();
            }

            _context.Entry(songTags).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongTagsExists(id))
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

        // POST: api/SongTags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SongTags>> PostSongTags(SongTags songTags)
        {
          if (_context.SongTags == null)
          {
              return Problem("Entity set 'MusicBankContext.SongTags'  is null.");
          }
            _context.SongTags.Add(songTags);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSongTags", new { id = songTags.Id }, songTags);
        }

        // DELETE: api/SongTags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSongTags(int id)
        {
            if (_context.SongTags == null)
            {
                return NotFound();
            }
            var songTags = await _context.SongTags.FindAsync(id);
            if (songTags == null)
            {
                return NotFound();
            }

            _context.SongTags.Remove(songTags);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongTagsExists(int id)
        {
            return (_context.SongTags?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
