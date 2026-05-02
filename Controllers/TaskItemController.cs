using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Data;
using TaskFlowAPI.DTOs;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    [Authorize]
    public class TaskItemController : ControllerBase
    {
        private readonly ApiContext _context;

        public TaskItemController(ApiContext context)
        {
            _context = context;
        }

        // GET ALL TASKS (only tasks of logged-in user projects)
        [HttpGet]
        public async Task<ActionResult<List<TaskItemDto>>> GetAll()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var tasks = await _context.Tasks
                .Include(t => t.Project)
                .Where(t => t.Project!.UserId == userId)
                .Select(t => new TaskItemDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Status = t.Status,
                    ProjectId = t.ProjectId,
                    DueDate = t.DueDate,
                    Comments = t.Comments
                })
                .ToListAsync();

            return Ok(tasks);
        }

        // GET TASK BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetById(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id && t.Project!.UserId == userId);

            if (task == null)
                return NotFound();

            return Ok(new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                Status = task.Status,
                ProjectId = task.ProjectId,
                DueDate = task.DueDate,
                Comments = task.Comments
            });
        }

        // CREATE TASK
        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> Create(TaskItemCreateDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            // check project belongs to user
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == dto.ProjectId && p.UserId == userId);

            if (project == null)
                return BadRequest("Project not found or not yours");

            var task = new TaskItem
            {
                Title = dto.Title,
                ProjectId = dto.ProjectId,
                DueDate = dto.DueDate,
                Status = Models.TaskStatus.AFaire,
                Comments = dto.Comments ?? new List<string>()
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok(new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                Status = task.Status,
                ProjectId = task.ProjectId,
                DueDate = task.DueDate,
                Comments = task.Comments
            });
        }

        // UPDATE TASK
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskItemDto>> Update(int id, TaskItemDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id && t.Project!.UserId == userId);

            if (task == null)
                return NotFound();

            task.Title = dto.Title;
            task.Status = dto.Status;
            task.DueDate = dto.DueDate;
            task.Comments = dto.Comments;

            await _context.SaveChangesAsync();

            var result = new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                Status = task.Status,
                ProjectId = task.ProjectId,
                DueDate = task.DueDate,
                Comments = task.Comments
            };

            return Ok(result);
        }


        // DELETE TASK
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id && t.Project!.UserId == userId);

            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}