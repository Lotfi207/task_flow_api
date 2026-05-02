<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Data;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;
using static TaskFlowAPI.Models.User;

namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
=======
﻿using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFLowAPI.Data;
using TaskFLowAPI.Models;

namespace TaskFLowAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
>>>>>>> dca66342fd571c4779676ca6b18eb7081b391d3e
    {
        private readonly ApiContext _context;

        public UserController(ApiContext context)
        {
            _context = context;
        }
<<<<<<< HEAD
        // GET ALL USERS
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
        // CREATE USER
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Create(UserCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email already exists");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                UserRole = Role.User
            };
=======
        // GET: UserController
        [HttpGet]
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
>>>>>>> dca66342fd571c4779676ca6b18eb7081b391d3e

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

<<<<<<< HEAD
            var response = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.UserRole.ToString()
            };

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, response);
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
=======
            return CreatedAtAction(nameof(Details), new { id = user.Id }, user);
        }



    }
}
>>>>>>> dca66342fd571c4779676ca6b18eb7081b391d3e
