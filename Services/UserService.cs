using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Data;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;

using static TaskFlowAPI.Models.User;

namespace TaskFlowAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApiContext _context;

        public UserService(ApiContext context)
        {
            _context = context;
        }

        public async Task<List<UserResponseDto>> GetAllAsync()
        {
            return await _context.Users
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.UserRole.ToString()
                })
                .ToListAsync();
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.UserRole.ToString()
            };
        }

        public async Task<UserResponseDto> CreateAsync(UserCreateDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new Exception("Email already exists");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password, // hashing later
                UserRole = Role.User
            };
           

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.UserRole.ToString()
            };
        }
    }
}
