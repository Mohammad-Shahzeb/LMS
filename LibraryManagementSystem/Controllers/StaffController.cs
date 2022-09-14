using LibraryManagementSystem.EF_Models;
using LibraryManagementSystem.Filters;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace LibraryManagementSystem.Controllers
{
    public class StaffController : Controller
    {
        
        private readonly LMS_Context _context = new LMS_Context();
        [StaffUserAuth]
        public IActionResult Index()
        {
            var list = _context.LmsStaffs.OrderByDescending(a => a.CreatedDate).ToList();
            return View(list);
        }

        [HttpPost]
        public IActionResult Index(StaffSearchModel searchModel)
        {
            IQueryable<LmsStaff> query = _context.LmsStaffs;

            if (!string.IsNullOrEmpty(searchModel.FirstName))
            {
                query = query.Where(a => a.FirstName == searchModel.FirstName);
            }

            if (!string.IsNullOrEmpty(searchModel.LastName))
            {
                query = query.Where(a => a.LastName == searchModel.LastName);
            }

            if (!string.IsNullOrEmpty(searchModel.Email))
            {
                query = query.Where(a => a.Email == searchModel.Email);
            }

            if (searchModel.FromDate is not null)
            {
                var date = Convert.ToDateTime(searchModel.FromDate).Date;
                query = query.Where(a => a.CreatedDate >= date);
            }

            if (searchModel.ToDate is not null)
            {
                var date = Convert.ToDateTime(searchModel.ToDate).Date.AddDays(1).AddSeconds(-1);
                query = query.Where(a => a.CreatedDate <= date);
            }

            var list = query.OrderByDescending(a => a.CreatedDate).ToList();
            return View(list);
        }
        [HttpGet]
        public IActionResult Create_Staff()
        {
            return View();
        }
        public string SaveFileToFolder(IFormFile file, string filepath)
        {
            using (Stream filestream = new FileStream(filepath, FileMode.Create))
            {
                file.CopyToAsync(filestream);
            }

            return String.Empty;
        }

        [HttpPost]
        public IActionResult Create_Staff(LmsStaff staff)
        {

            string path = "wwwroot/staffImages";


            var files = Request.Form.Files;

            if (files.Count > 0)
            {
                var file = files["ImagePath"];

                if (file.Length > 0)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    staff.ImagePath = DateTime.Now.Ticks.ToString() + file.FileName;
                    path = Path.Combine(path, staff.ImagePath);

                    using (Stream filestream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                }

            }



            try
            {
                staff.CreatedDate = DateTime.Now;
                _context.Add(staff);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return View();
            }

            return RedirectToAction("Index");

        }

        public IActionResult Delete(int shazz)
        {
            var staff = _context.LmsStaffs.FirstOrDefault(a => a.Id == shazz);

            if (staff is not null)
            {
                _context.Remove(staff);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int cr)
        {
            var staff = _context.LmsStaffs.FirstOrDefault(a => a.Id == cr);

            if (staff is not null)
            {
                return View(staff);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(LmsStaff cr)
        {


            _context.Update(cr);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
