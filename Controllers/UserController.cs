using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Data;
using TaskFlowAPI.Models;
using TaskFlowAPI.DTOs;

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
        [HttpGet]
        public async Task<ActionResult<List<UserResponseDto>>> Index()
        {
            var users = await _context.Users
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.UserRole.ToString()
                })
                .ToListAsync();

            return Ok(users);
        }


        // GET: UserController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> Details(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            var response = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.UserRole.ToString()
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Create(UserCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password,
                UserRole = Role.User
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var response = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.UserRole.ToString()
            };

            return CreatedAtAction(nameof(Details), new { id = user.Id }, response);
        }


    }
}
