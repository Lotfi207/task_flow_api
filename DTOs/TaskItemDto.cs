using TaskFlowAPI.Models;

namespace TaskFlowAPI.DTOs
{
    public class TaskItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public Models.TaskStatus Status { get; set; }

        public int ProjectId { get; set; }

        public DateTime? DueDate { get; set; }

        public List<string> Comments { get; set; } = new();
    }
}