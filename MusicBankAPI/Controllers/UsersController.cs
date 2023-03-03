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
    public class UsersController : ControllerBase
    {
        private readonly MusicBankContext _context;
        private readonly IMapper _mapper;

        public UsersController(MusicBankContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> PutUser(int id, UserViewModel userViewModel)
        {
            try
            {
                User user = await _context.Users.FindAsync(id);
                if (user is null)
                {
                    return NotFound("User not found."); ;
                }
                user.Name = userViewModel.Name;
                _context.Entry(user).State = EntityState.Modified;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch
            {
                return BadRequest("Problem!");
            }
        }

        
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserViewModel userViewModel)
        {
            try
            {
                User user = _mapper.Map<User>(userViewModel);
                var usersList = await _context.Users.ToListAsync();

                var result = usersList.Where(x => x.Name == userViewModel.Name).FirstOrDefault();
                if (result is not null)
                {
                    return Conflict("Already registered user!");
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = user.Id }, user);

            }

            catch
            {
                return BadRequest("Invalid data.");
            }
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

       
    }
}
