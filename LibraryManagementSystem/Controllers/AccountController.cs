using LibraryManagementSystem.EF_Models;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly LMS_Context _context = new LMS_Context();

        public IActionResult StaffLogin()
        {
            return View(new LoginModel());
        }


        [HttpPost]
        public IActionResult StaffLogin(LoginModel model)
        {

            var result = _context.LmsStaffs.FirstOrDefault(a=> a.Email == model.Email && a.Password == model.Password);

            if(result is null)
            {
                ViewBag.message = "Entered Email or Password was incorrect";
                return View(model);
            }
            //
            // store date to session here!!!
            HttpContext.Session.SetInt32("StaffId",result.Id);
            // 
            return RedirectToAction("Index","InventoryHistory");

        }

        public IActionResult StudentLogin()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public IActionResult StudentLogin(LoginModel model)
        {
            var result = _context.LmsStudents.FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password);

            if (result is null)
            {
                return View(model);
            }

            HttpContext.Session.SetInt32("StudentID", result.Id);

            return RedirectToAction("Index","InventoryHistory");
        }
    }
}
