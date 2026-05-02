namespace TaskFlowAPI.DTOs
{
    public class ProjectCreateDto
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}