using LibraryManagementSystem.EF_Models;
using LibraryManagementSystem.Filters;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [StaffUserAuth]
    public class StudentController : Controller
    {
        private readonly LMS_Context _context = new LMS_Context();

        //private readonly IWebHostEnvironment _webHostEnvironment;

        //public StudentController(IWebHostEnvironment webHostEnvironment)
        //{
        //    _webHostEnvironment = webHostEnvironment;
        //}
        public IActionResult Get_Student_list()
        {
            var list = _context.LmsStudents.OrderByDescending(a => a.CreatedDate).ToList();
            return View(list);
        }

        [HttpPost]
        public IActionResult Get_Student_list(StudentSearchModel searchModel)
        {

            IQueryable<LmsStudent> query = _context.LmsStudents;

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

            var list = query.ToList();
            return View(list);
        }


        [HttpGet]
        public IActionResult Create_Student()
        {
            return View();
        }


        public string SaveFiletoFolder(IFormFile file, string filePath)
        {
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(fileStream);
            }

            return string.Empty;
        }


        [HttpPost]
        public IActionResult Create_Student(LmsStudent student)
        {

            string path = "wwwroot/studentImages";

            ///string path = "wwwroot/StudentImages/";

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
                    student.ImagePath = DateTime.Now.Ticks.ToString() + file.FileName;
                    path = Path.Combine(path, student.ImagePath);
                    // SaveFiletoFolder(file, path);


                    using (Stream fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }

            try
            {
                student.CreatedDate = DateTime.Now;
                _context.Add(student);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                return View();
            }

            return RedirectToAction("Get_Student_list");
        }

        public IActionResult Delete(int mm)
        {
            var Delete = _context.LmsStudents.FirstOrDefault(a => a.Id == mm);
            if (Delete is not null)
            {
                _context.Remove(Delete);
                _context.SaveChanges();
            }
            return RedirectToAction("Get_Student_List");
        }
        [HttpGet]
        public IActionResult Edit(int mm)
        {
            var Edit = _context.LmsStudents.FirstOrDefault(a => a.Id == mm);
            if (Edit is not null)
            {
                return View(Edit);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(LmsStudent mm)
        {
            _context.Update(mm);
            _context.SaveChanges();
            return RedirectToAction("Get_Student_List");
        }


    }
}
