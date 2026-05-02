<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Data;
using TaskFlowAPI.Models;

namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskItemController : ControllerBase
    {
        private readonly ApiContext _context;

        public TaskItemController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskItem>>> GetAll()
        {
            List<TaskItem> tasks = await _context.Tasks.ToListAsync();

            return Ok(tasks);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetById(int id)
        {
            TaskItem? task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        
        [HttpPost]
        public async Task<ActionResult<TaskItem>> Create(TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

       
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, TaskItem task)
        {
            if (id != task.Id)
            {
                return BadRequest("Id does not match");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool exists = await _context.Tasks.AnyAsync(t => t.Id == id);

                if (!exists)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            TaskItem? task = await _context.Tasks.FindAsync(id);

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
=======
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskFlowAPI.Controllers
{
    public class TaskItemController : Controller
    {
        // GET: TaskItemController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TaskItemController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TaskItemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TaskItemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TaskItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TaskItemController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TaskItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
>>>>>>> fe1176d (add ProjectController and TaskItemController)
