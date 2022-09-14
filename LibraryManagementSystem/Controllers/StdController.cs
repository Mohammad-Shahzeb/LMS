using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class StdController : Controller
    {
        // GET: StdController
        public ActionResult Index()
        {
            return View();
        }

        // GET: StdController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StdController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StdController/Create
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

        // GET: StdController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StdController/Edit/5
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

        // GET: StdController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StdController/Delete/5
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
