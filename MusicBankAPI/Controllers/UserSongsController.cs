using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBankAPI.Context;
using MusicBankAPI.Models;
using MusicBankAPI.ViewModels;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace MusicBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSongsController : ControllerBase
    {
        private readonly MusicBankContext _context;
        private readonly IMapper _mapper;
        private ConnectionFactory factory;

        public UserSongsController(MusicBankContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

            this.factory = new ConnectionFactory()
            {
                HostName = "167.172.186.10",
                UserName = "tatyana",
                Password = "learningRabbitMQ"
            };
        }

        // GET: api/UserSongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSongs>>> GetUserSongs()
        {
            if (_context.UserSongs == null)
            {
                return NotFound();
            }
            return await _context.UserSongs
                .Include(x => x.User)
                .Include(x => x.Song)
                .ToListAsync();
        }

        // GET: api/UserSongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSongs>> GetUserSongs(int id)
        {
            if (_context.UserSongs == null)
            {
                return NotFound();
            }
            var userSongs = await _context.UserSongs
                .Include(x => x.User)
                .Include(x => x.Song)
                .Where(y => y.Id == id).FirstOrDefaultAsync();

            if (userSongs == null)
            {
                return NotFound();
            }

            return userSongs;
        }

        // PUT: api/UserSongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<UserSongViewModel>> PutUserSongs(int id, UserSongViewModel userSongViewModel)
        {
            try
            {

                var userSongsList = await _context.UserSongs.ToListAsync();
                UserSongs userSong = userSongsList.Where(y => y.Id == id).FirstOrDefault();
                if (userSong is null)
                {
                    return NotFound("Song not found."); ;
                }
                foreach (var x in userSongsList)
                {
                    if (x.SongId == userSongViewModel.SongId && x.UserId == userSongViewModel.SongId)
                    {
                        return Conflict("This user already has this song!");
                    }
                }
                userSong.UserId = userSongViewModel.UserId;
                userSong.SongId = userSongViewModel.SongId;


                _context.Entry(userSong).State = EntityState.Modified;
                _context.UserSongs.Update(userSong);
                await _context.SaveChangesAsync();

                return userSongViewModel;
            }
            catch
            {
                return BadRequest("Problem!");
            }
        }

        // POST: api/UserSongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserSongListViewModel>> PostUserSongs(UserSongListViewModel userSongListViewModel)
        {
            try
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "addToLib",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var body = JsonConvert.SerializeObject(userSongListViewModel);
                    var userBytes = Encoding.UTF8.GetBytes(body);
                    channel.BasicPublish(exchange: "",
                                    routingKey: "addToLib",
                                    basicProperties: null,
                                    body: userBytes);
                }

                return Ok();

            }

            catch
            {
                return BadRequest("Invalid data.");
            }

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

    }
}
