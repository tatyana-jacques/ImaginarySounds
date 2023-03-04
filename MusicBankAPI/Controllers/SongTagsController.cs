
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
    public class SongTagsController : ControllerBase
    {
        private readonly MusicBankContext _context;
        private readonly IMapper _mapper;


        public SongTagsController(MusicBankContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongTags>>> GetSongTags()
        {
            if (_context.SongTags == null)
            {
                return NotFound();
            }
            return await _context.SongTags
                .Include(x => x.Tag)
                .Include(x => x.Song)
                .Include(x => x.Song.Artist)
                .Include(x => x.Song.Composer)
                .ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<SongTags>> GetSongTags(int id)
        {
            if (_context.SongTags == null)
            {
                return NotFound();
            }
            var songTags = await _context.SongTags
                .Include(x => x.Tag)
                .Include(x => x.Song)
                .Include(x => x.Song.Artist)
                .Include(x => x.Song.Composer)
                .Where(y => y.Id == id).FirstOrDefaultAsync();

            if (songTags == null)
            {
                return NotFound();
            }

            return songTags;
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<SongTagsViewModel>> PutSongTags(int id, SongTagsViewModel songTagsViewModel)
        {
            try
            {

                var songTagsList = await _context.SongTags.ToListAsync();
                SongTags songTag = songTagsList.Where(y => y.Id == id).FirstOrDefault();
                if (songTag is null)
                {
                    return NotFound("Register not found."); ;
                }
                foreach (var x in songTagsList)
                {
                    if (x.SongId == songTagsViewModel.SongId && x.TagId == songTagsViewModel.TagId)
                    {
                        return Conflict("This register already exists!");
                    }
                }
                songTag.SongId = songTagsViewModel.SongId;
                songTag.TagId = songTagsViewModel.TagId;


                _context.Entry(songTag).State = EntityState.Modified;
                _context.SongTags.Update(songTag);
                await _context.SaveChangesAsync();

                return songTagsViewModel;
            }
            catch
            {
                return BadRequest("Problem!");
            }
        }


        [HttpPost]
        public async Task<ActionResult<SongTagsViewModel>> PostSongTags(SongTagsViewModel songTagsViewModel)
        {
            try
            {
                SongTags songTag = _mapper.Map<SongTags>(songTagsViewModel);
                var songTagsList = await _context.SongTags.ToListAsync();


                foreach (var x in songTagsList)
                {
                    if (x.SongId == songTagsViewModel.SongId && x.TagId == songTagsViewModel.TagId)
                    {
                        return Conflict("This register already exists!");
                    }
                }


                _context.SongTags.Add(songTag);
                await _context.SaveChangesAsync();

                return Ok(songTagsViewModel);

            }

            catch
            {
                return BadRequest("Invalid data.");
            }
        }



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


    }
}
