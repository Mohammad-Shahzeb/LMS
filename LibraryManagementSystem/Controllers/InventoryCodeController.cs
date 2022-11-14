using LibraryManagementSystem.EF_Models;
using LibraryManagementSystem.Filters;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [StaffUserAuth]
    public class InventoryCodeController : Controller
    {
        private readonly LMS_Context _context = new LMS_Context();
        [HttpGet]

        public IActionResult Index()
        {
            var list = _context.LmsInventoryCodes.ToList();
            return View(list);
        }


        [HttpGet]
        public IActionResult CreateInventoryCode()
        {
            return View(new LmsInventoryCode());
        }

        [HttpPost]
        public IActionResult CreateInventoryCode(LmsInventoryCode inventorycodemodel)
        {
            try
            {
                _context.Add(inventorycodemodel);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return View();
            }
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int Id)
        {

            var Delete = _context.LmsInventoryCodes.FirstOrDefault(a => a.Id == Id);
            if (Delete is not null)
            {
                _context.Remove(Delete);
                _context.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var Edit = _context.LmsInventoryCodes.FirstOrDefault(a => a.Id == Id);
            if (Edit is not null)
            {
                return View(Edit);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(LmsInventoryCode ed)
        {
            _context.Update(ed);
            _context.SaveChanges();
            return RedirectToAction("Index");
          

        }


        public bool IsValidCode(string Code , int Id)
        {
            if(Id == 0)
            {
                return !_context.LmsInventoryCodes.Any(a => a.Code == Code);
            }
            else
            {
                return !_context.LmsInventoryCodes.Any(a=>a.Code == Code && a.Id != Id);
            }
        }

    }
}
    