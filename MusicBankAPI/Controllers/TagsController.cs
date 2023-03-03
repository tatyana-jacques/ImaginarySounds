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
    public class TagsController : ControllerBase
    {
        private readonly MusicBankContext _context;
        private readonly IMapper _mapper;

        public TagsController(MusicBankContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTag()
        {
          if (_context.Tag == null)
          {
              return NotFound();
          }
            return await _context.Tag.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
          if (_context.Tag == null)
          {
              return NotFound();
          }
            var tag = await _context.Tag.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return tag;
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Tag>> PutTag(int id, TagViewModel tagViewModel)
        {
            try
            {
                Tag tag = await _context.Tag.FindAsync(id);
                if (tag is null)
                {
                    return NotFound("Artist not found."); ;
                }
                tag.Title = tagViewModel.Title;
                _context.Entry(tag).State = EntityState.Modified;
                _context.Tag.Update(tag);
                await _context.SaveChangesAsync();

                return tag;
            }
            catch
            {
                return BadRequest("Problem!");
            }
        }

       
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(TagViewModel tagViewModel)
        {
            try
            {
                Tag tag = _mapper.Map<Tag>(tagViewModel);
                var tagsList = await _context.Tag.ToListAsync();

                var result = tagsList.Where(x => x.Title == tagViewModel.Title).FirstOrDefault();
                if (result is not null)
                {
                    return Conflict("Already registered tag!");
                }

                _context.Tag.Add(tag);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTag", new { id = tag.Id }, tag);

            }

            catch
            {
                return BadRequest("Invalid data.");
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            if (_context.Tag == null)
            {
                return NotFound();
            }
            var tag = await _context.Tag.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.Tag.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

     
    }
}
