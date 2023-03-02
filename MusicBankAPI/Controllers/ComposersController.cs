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
    public class ComposersController : ControllerBase
    {
        private readonly MusicBankContext _context;

        public ComposersController(MusicBankContext context)
        {
            _context = context;
        }

        // GET: api/Composers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComposerViewModel>>> GetComposers()
        {
          if (_context.Composers == null)
          {
              return NotFound();
          }
            return await _context.Composers.ToListAsync();
        }

        // GET: api/Composers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComposerViewModel>> GetComposer(int id)
        {
          if (_context.Composers == null)
          {
              return NotFound();
          }
            var composer = await _context.Composers.FindAsync(id);

            if (composer == null)
            {
                return NotFound();
            }

            return composer;
        }

        // PUT: api/Composers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComposer(int id, ComposerViewModel composer)
        {
            if (id != composer.Id)
            {
                return BadRequest();
            }

            _context.Entry(composer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComposerExists(id))
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

        // POST: api/Composers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComposerViewModel>> PostComposer(ComposerViewModel composer)
        {
          if (_context.Composers == null)
          {
              return Problem("Entity set 'MusicBankContext.Composers'  is null.");
          }
            _context.Composers.Add(composer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComposer", new { id = composer.Id }, composer);
        }

        // DELETE: api/Composers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComposer(int id)
        {
            if (_context.Composers == null)
            {
                return NotFound();
            }
            var composer = await _context.Composers.FindAsync(id);
            if (composer == null)
            {
                return NotFound();
            }

            _context.Composers.Remove(composer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComposerExists(int id)
        {
            return (_context.Composers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
