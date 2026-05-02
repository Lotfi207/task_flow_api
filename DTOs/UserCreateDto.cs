using TaskFlowAPI.Models;

namespace TaskFlowAPI.DTOs
{
    public class UserCreateDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public User.Role Role { get; set; }

    }
}
