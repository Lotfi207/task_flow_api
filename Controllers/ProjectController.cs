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
    [Route("api/projects")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly ApiContext _context;

        public ProjectController(ApiContext context)
        {
            _context = context;
        }

        // GET ALL PROJECTS (user connecté seulement)
        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetAll()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var projects = await _context.Projects
                .Where(p => p.UserId == userId)
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CreationDate = p.CreationDate
                })
                .ToListAsync();

            return Ok(projects);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetById(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (project == null)
                return NotFound();

            var dto = new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreationDate = project.CreationDate
            };

            return Ok(dto);
        }

        // CREATE PROJECT
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> Create(ProjectCreateDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                CreationDate = DateTime.Now,
                UserId = userId
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Ok(new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreationDate = project.CreationDate
            });
        }

        // UPDATE PROJECT
        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectDto>> Update(int id, ProjectDto dto)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
                return NotFound();

            project.Name = dto.Name;
            project.Description = dto.Description;

            await _context.SaveChangesAsync();

            var result = new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreationDate = project.CreationDate,
               
            };

            return Ok(result);
        }

        // DELETE PROJECT
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (project == null)
                return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}