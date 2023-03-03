using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBankAPI.Context;
using MusicBankAPI.Models;
using MusicBankAPI.ViewModels;
using AutoMapper;

namespace MusicBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComposersController : ControllerBase
    {
        private readonly MusicBankContext _context;
        private readonly IMapper _mapper;

        public ComposersController(MusicBankContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Composer>>> GetComposers()
        {
          if (_context.Composers == null)
          {
              return NotFound();
          }
            return await _context.Composers.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Composer>> GetComposer(int id)
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

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Composer>> PutComposer(int id, ComposerViewModel composerViewModel)
        {

            try
            {
                Composer composer = await _context.Composers.FindAsync(id);
                if (composer is null)
                {
                    return NotFound("Composer not found."); ;
                }
                composer.Name = composerViewModel.Name;
                _context.Entry(composer).State = EntityState.Modified;
                _context.Composers.Update(composer);
                await _context.SaveChangesAsync();

                return composer;
            }
            catch
            {
                return BadRequest("Problem!");
            }
        }

      
        [HttpPost]
        public async Task<ActionResult<Composer>> PostComposer(ComposerViewModel composerViewModel)
        {
            try
            {
                Composer composer = _mapper.Map<Composer>(composerViewModel);
                var composersList = await _context.Composers.ToListAsync();

                var result = composersList.Where(x => x.Name == composerViewModel.Name).FirstOrDefault();
                if (result is not null)
                {
                    return Conflict("Already registered composer!");
                }

                _context.Composers.Add(composer);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetComposer", new { id = composer.Id }, composer);

            }

            catch
            {
                return BadRequest("Invalid data.");
            }
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

    }
}
