using TaskFlowAPI.DTOs;

namespace TaskFlowAPI.Services
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto?> GetByIdAsync(int id);
        Task<UserResponseDto> CreateAsync(UserCreateDto dto);
    }
}
