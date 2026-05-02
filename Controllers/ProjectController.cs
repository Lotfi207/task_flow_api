using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Data;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : Controller
    {
        private readonly ApiContext _context;

        public ProjectController(ApiContext context)
        {
            _context = context;
        }

        // GET: ProjectController
        [HttpGet]
        public async Task<ActionResult<List<Project>>> Index()
        {
            List<Project> projects = await _context.Projects.ToListAsync();
            return Ok(projects);
        }

        // GET: ProjectController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> Details(int id)
        {
            Project? project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // POST: ProjectController/Create
        [HttpPost]
        public async Task<ActionResult<Project>> Create(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            project.CreationDate = DateTime.Now;

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Details), new { id = project.Id }, project);
        }

        // PUT: ProjectController/Update/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Project>> Update(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest("Id does not match");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool exists = await _context.Projects.AnyAsync(p => p.Id == id);
                if (!exists)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: ProjectController/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Project? project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}