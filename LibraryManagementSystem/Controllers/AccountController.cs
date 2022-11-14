using LibraryManagementSystem.EF_Models;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            HttpContext.Session.SetInt32(SessionKeys.StaffId,result.Id);
            // 
            return RedirectToAction("Index","InventoryHistory");

        }
      
        public IActionResult staffLogout()  
        {
            HttpContext.Session.Clear();
            return RedirectToAction("StaffLogin");
        }

        public IActionResult StudentLogin()
        {

            if (HttpContext.Session.GetInt32(SessionKeys.StudentId) is not null)
            {

               

               return RedirectToAction("Profile", "Student");
            }


            return View(new LoginModel());
        }

        [HttpPost]
        public IActionResult StudentLogin(LoginModel model)
        {
            var result = _context.LmsStudents.FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password);

            //var eee = _context.Database.ExecuteSqlRaw($"exec GetStudentIdByEmailPass_SP", [model.Email, model.Password]);

            if (result is null)
            {
                return View(model);
            }

            HttpContext.Session.SetInt32(SessionKeys.StudentId, result.Id);

            return RedirectToAction("Profile", "Student");
        }

        public IActionResult StudentLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("StudentLogin");
        }
    }
}
