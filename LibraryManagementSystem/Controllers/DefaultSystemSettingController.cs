using LibraryManagementSystem.EF_Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class DefaultSystemSettingController : Controller
    {
        private readonly LMS_Context _context = new LMS_Context();
        public IActionResult Index()
        {
            var list = _context.LmsDefaultSystemSettings.ToList();
            return View(list);
        }
        [HttpGet]
        public IActionResult SystemDefault()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SystemDefault(LmsDefaultSystemSetting SystemDefault)
        {
            try
            {
                _context.Add(SystemDefault);
                _context.SaveChanges();
            }

            catch(Exception ex)
            {
                return View();
            }
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int ss)
        {
            var Edit = _context.LmsDefaultSystemSettings.FirstOrDefault(a=> a.Id == ss);
            if(Edit is not null)
            {
                return View(Edit);
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult Edit(LmsDefaultSystemSetting ss)
        {
            _context.Update(ss);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
