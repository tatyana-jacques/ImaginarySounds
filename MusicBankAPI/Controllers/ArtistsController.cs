using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBankAPI.Context;
using MusicBankAPI.Models;
using MusicBankAPI.ViewModels;
using AutoMapper;
using System.Collections;

namespace MusicBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly MusicBankContext _context;
        private readonly IMapper _mapper;

        public ArtistsController(MusicBankContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
          if (_context.Artists == null)
          {
              return NotFound();
          }
            return await _context.Artists.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
          if (_context.Artists == null)
          {
              return NotFound();
          }
            var artist = await _context.Artists.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            return artist;
        }

       
        [HttpPut("{id}")]
        public async Task<ActionResult<Artist>> PutArtist(int id, ArtistViewModel artistViewModel)
        {
            try
            {
                Artist artist = await _context.Artists.FindAsync(id);
                if (artist is null)
                {
                    return NotFound("Artist not found."); ;
                }
                artist.Name = artistViewModel.Name;
                _context.Entry(artist).State = EntityState.Modified;
                _context.Artists.Update(artist);
                await _context.SaveChangesAsync();

                return artist;
            }
            catch
            {
                return BadRequest("Problem!");
            }
        }

        
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(ArtistViewModel artistViewModel)
        {
            try
            {
                Artist artist = _mapper.Map<Artist>(artistViewModel);
                var artistsList = await _context.Artists.ToListAsync();

                var result = artistsList.Where(x => x.Name == artistViewModel.Name).FirstOrDefault();
                if (result is not null)
                {
                    return Conflict("Already registered artist!");
                }

                _context.Artists.Add(artist);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
               
            }

            catch
            {
                return BadRequest("Invalid data.");
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            if (_context.Artists == null)
            {
                return NotFound();
            }
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
