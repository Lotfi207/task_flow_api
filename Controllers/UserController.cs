using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Data;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Helpers;
using TaskFlowAPI.Models;


namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase

    {
        private readonly ApiContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public UserController(ApiContext context, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        // auth operations : register and login 
        //register
        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> Register(UserCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email already exists");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                UserRole = dto.Role
            };

            // Hash password
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

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

            // Verify password
            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                dto.Password
            );

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid credentials");

            // Create claims
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
                user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
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
        
