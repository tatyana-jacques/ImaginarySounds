
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBankAPI.Context;
using MusicBankAPI.Models;
using MusicBankAPI.ViewModels;
using RabbitMQ.Client;
using AutoMapper;
using Newtonsoft.Json;
using System.Text;

namespace MusicBankAPI.Controllers
{
    [Route("api/[controller]/[action]")]
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
                .Include(x => x.Song.Artist)
                .Include(x => x.Song.Composer)
                .ToListAsync();
        }

        [HttpGet]
        public ActionResult<int> GetStatusByUserId(int userId)
        {
            if (_context.UserSongs == null)
            {
                return NotFound();
            }
            return _context.StatusTable.Last(e => e.UserId == userId).Status;
        }


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
                .Include(x => x.Song.Artist)
                .Include(x => x.Song.Composer)
                .Where(y => y.Id == id).FirstOrDefaultAsync();

            if (userSongs == null)
            {
                return NotFound();
            }

            return userSongs;
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<UserSongViewModel>> PutUserSongs(int id, UserSongViewModel userSongViewModel)
        {
            try
            {

                var userSongsList = await _context.UserSongs.ToListAsync();
                UserSongs userSong = userSongsList.Where(y => y.Id == id).FirstOrDefault();
                if (userSong is null)
                {
                    return NotFound("This user does not own this asset."); ;
                }
                foreach (var x in userSongsList)
                {
                    if (x.SongId == userSongViewModel.SongId && x.UserId == userSongViewModel.UserId)
                    {
                        return Conflict("This register already exists!");
                    }
                }
                userSong.SongId = userSongViewModel.SongId;
                userSong.UserId = userSongViewModel.UserId;


                _context.Entry(userSong).State = EntityState.Modified;
                // _context.UserSongs.Update(userSong);
                await _context.SaveChangesAsync();

                return userSongViewModel;
            }
            catch
            {
                return BadRequest("Problem!");
            }

        }


        [HttpPost]
        public async Task<ActionResult<UserSongsViewModel>> PostUserSongs(UserSongsViewModel userSongViewModel)
        {
            try
            {

                //var userSongList = await _context.UserSongs.ToListAsync();
                // foreach (var x in userSongList)
                // {
                //     if (x.SongId == userSongViewModel.SongId && x.UserId == userSongViewModel.UserId)
                //     {
                //         return Conflict("This register already exists!");
                //     }
                // }

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "addToLib",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = JsonConvert.SerializeObject(userSongViewModel);
                var songBytes = Encoding.UTF8.GetBytes(body);
                channel.BasicPublish(exchange: "",
                                routingKey: "addToLib",
                                basicProperties: null,
                                body: songBytes);


                // _context.UserSongs.Add(userSong);
                // await _context.SaveChangesAsync();

                // var status = new StatusTable
                // {
                //     UserId = userSongViewModel.UserSongList.First().UserId,
                //     Status = 0
                // };

                return Ok(userSongViewModel);

            }

            catch
            {
                return BadRequest("Invalid data.");
            }
        }


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
