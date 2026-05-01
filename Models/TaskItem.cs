using System;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Models
{
    public enum TaskStatus
    {
        AFaire,
        EnCours,
        Termine
    }

    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public TaskStatus Status { get; set; } = TaskStatus.AFaire;

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public DateTime? DueDate { get; set; }
    }
}