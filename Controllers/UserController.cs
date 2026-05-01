using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Data;
using TaskFlowAPI.Models;

using static TaskFlowAPI.Models.User;

namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ApiContext _context;

        public UserController(ApiContext context)
        {
            _context = context;
        }
        // GET: UserController
        public async Task<ActionResult<List<User>>> Index()
        {
            List<User> users = await _context.Users.ToListAsync();
            return Ok(users);
        }


        // GET: UserController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Details(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: UserController/Create
        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Details), new { id = user.Id }, user);
        }

        
    }
}
