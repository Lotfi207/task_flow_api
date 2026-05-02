using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Data;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/tasks")]
    public class TaskItemController : ControllerBase
    {
        private readonly ApiContext _context;

        public TaskItemController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<List<TaskItem>>> GetAll()
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            var tasks = await _context.Tasks
                .Include(t => t.Project)
                .Where(t => t.Project.UserId == userId)
                .ToListAsync();

            return Ok(tasks);
        }

        // GET: api/tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetById(int id)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(
                    t => t.Id == id && t.Project.UserId == userId
                );

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskItem>> Create(TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            var project = await _context.Projects
                .FirstOrDefaultAsync(
                    p => p.Id == task.ProjectId && p.UserId == userId
                );

            if (project == null)
            {
                return BadRequest("Invalid project");
            }

            _context.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = task.Id },
                task
            );
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, TaskItem updatedTask)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(
                    t => t.Id == id && t.Project.UserId == userId
                );

            if (task == null)
            {
                return NotFound();
            }

            task.Title = updatedTask.Title;
            task.Status = updatedTask.Status;
            task.DueDate = updatedTask.DueDate;
            task.Comments = updatedTask.Comments;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            );

            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(
                    t => t.Id == id && t.Project.UserId == userId
                );

            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}