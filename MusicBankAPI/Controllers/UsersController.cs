using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBankAPI.Context;
using MusicBankAPI.Models;
using AutoMapper;
using MusicBankAPI.ViewModels;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace MusicBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MusicBankContext _context;
        private readonly IMapper _mapper;
        private ConnectionFactory factory;



        public UsersController(MusicBankContext context, IMapper mapper)
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

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "userRegistration",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var body = JsonConvert.SerializeObject(user);
                    var userBytes = Encoding.UTF8.GetBytes(body);
                    channel.BasicPublish(exchange: "",
                                    routingKey: "userRegistration",
                                    basicProperties: null,
                                    body: userBytes);
                }


                return Ok(user);

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
