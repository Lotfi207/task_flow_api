using TaskFlowAPI.Models;
using TaskStatus = TaskFlowAPI.Models.TaskStatus;


namespace TaskFlowAPI.DTOs
{
    public class TaskItemCreateDto
    {
        public string Title { get; set; } = string.Empty;

        public TaskStatus Status { get; set; }

        public int ProjectId { get; set; }

        public DateTime? DueDate { get; set; }
        public List<string> Comments { get; set; } = new();

    }
}