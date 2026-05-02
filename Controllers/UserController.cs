using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Helpers;
using TaskFlowAPI.Models;
using TaskFLowAPI.Data;
using static TaskFlowAPI.Models.User;

namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase

    {
        private readonly ApiContext _context;
        private readonly IConfiguration _configuration;

        public UserController(ApiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // auth operations : register and login 
             //register
        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> Register(UserCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check email exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (existingUser != null)
                return BadRequest("Email already exists");

            // Hash password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Create user
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                UserRole = dto.Role
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

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, response);
        }

        //Login
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isValid)
                return Unauthorized("Invalid credentials");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
            };

            var token = JwtHelper.CreateToken(claims, _configuration);

            return Ok(new
            {
                token
            });
        }


        //basic crud operations
        // GET ALL USERS
        [Authorize]
        [HttpGet]
            public async Task<ActionResult<List<UserResponseDto>>> GetAll()
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

            // GET USER BY ID
            [HttpGet("{id}")]
            public async Task<ActionResult<UserResponseDto>> GetById(int id)
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
       

            // UPDATE USER
            [HttpPut("{id}")]
            public async Task<ActionResult<UserResponseDto>> Update(int id, UserUpdateDto dto)
            {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            // Update name if provided
            if (!string.IsNullOrEmpty(dto.Name))
                user.Name = dto.Name;

            // Update email if provided
            if (!string.IsNullOrEmpty(dto.Email))
            {
                // optional uniqueness check
                if (await _context.Users.AnyAsync(u => u.Email == dto.Email && u.Id != id))
                    return BadRequest("Email already exists");

                user.Email = dto.Email;
            }

            // Update password if provided
            if (!string.IsNullOrEmpty(dto.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            await _context.SaveChangesAsync();

            var response = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.UserRole.ToString()
            };

            return Ok(response);
            }
            //DELETE USER 
            [HttpDelete("{id}")]
            public async Task<ActionResult> Delete(int id)
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                    return NotFound();

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return NoContent();
            }

    }
}
        
