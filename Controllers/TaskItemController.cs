using Microsoft.AspNetCore.Http;
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
