using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBankAPI.Context;
using MusicBankAPI.Models;
using AutoMapper;
using MusicBankAPI.ViewModels;

namespace MusicBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly MusicBankContext _context;
        private readonly IMapper _mapper;

        public SongsController(MusicBankContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
          if (_context.Songs == null)
          {
              return NotFound();
          }
            return await _context.Songs
                .Include(x => x.Artist)
                .Include(x => x.Composer)
                .ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
          if (_context.Songs == null)
          {
              return NotFound();
          }
            var song = await _context.Songs
                .Include(x => x.Artist)
                .Include(x => x.Composer)
                .Where(y => y.Id == id).FirstOrDefaultAsync();

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<SongViewModel>> PutSong(int id, SongViewModel songViewModel)
        {
            try
            {

                var songsList = await _context.Songs.ToListAsync();
                Song song = songsList.Where(y => y.Id == id).FirstOrDefault();
                if (song is null)
                {
                    return NotFound("Song not found."); ;
                }
                foreach (var x in songsList)
                {
                    if (x.Title == songViewModel.Title && x.ArtistId == songViewModel.ArtistId)
                    {
                        return Conflict("This artist already has a song with the same name!");
                    }
                }
                song.Title = songViewModel.Title;
                song.ArtistId = songViewModel.ArtistId;
                song.ComposerId = songViewModel.ComposerId;
                song.StorageData = songViewModel.StorageData;
                song.Cover = songViewModel.Cover;

                _context.Entry(song).State = EntityState.Modified;
                _context.Songs.Update(song);
                await _context.SaveChangesAsync();

                return songViewModel;
            }
            catch
            {
                return BadRequest("Problem!");
            }
        }

        
        [HttpPost]
        public async Task<ActionResult<SongViewModel>> PostSong(SongViewModel songViewModel)
        {
            try
            {
                Song song = _mapper.Map<Song>(songViewModel);
                var songsList = await _context.Songs.ToListAsync();

                
                foreach (var x in songsList)
                {
                    if (x.Title == songViewModel.Title && x.ArtistId==songViewModel.ArtistId)
                    {
                        return Conflict("Already registered song!");
                    }
                }

                _context.Songs.Add(song);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetSong", new { id = song.Id }, songViewModel);

            }

            catch
            {
                return BadRequest("Invalid data.");
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            if (_context.Songs == null)
            {
                return NotFound();
            }
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
