
using LibraryManagementSystem.EF_Models;
using LibraryManagementSystem.Filters;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
//using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{

    public class StudentController : Controller
    {
        private readonly LMS_Context _context = new LMS_Context();

        //private readonly IWebHostEnvironment _webHostEnvironment;

        //public StudentController(IWebHostEnvironment webHostEnvironment)
        //{
        //    _webHostEnvironment = webHostEnvironment;
        //}
        [StaffUserAuth]
        public IActionResult Get_Student_list()
        {
            var list = _context.LmsStudents.OrderByDescending(a => a.CreatedDate);
            return View(list.ToList());
        }

        [StaffUserAuth]
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

        [StaffUserAuth]
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

        [StaffUserAuth]
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
        [StaffUserAuth]
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
        [StaffUserAuth]
        public IActionResult Edit(int mm)
        {
            var Edit = _context.LmsStudents.FirstOrDefault(a => a.Id == mm);
            if (Edit is not null)
            {
                return View(Edit);
            }
            return View();
        }
        public string UpdateFiletoFolder(IFormFile file, string filePath)
        {
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(fileStream);
            }

            return string.Empty;
        }

        [HttpPost]
        [StaffUserAuth]
        public IActionResult Edit(LmsStudent mm)
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
                    mm.ImagePath = DateTime.Now.Ticks.ToString() + file.FileName;
                    path = Path.Combine(path, mm.ImagePath);
                    // SaveFiletoFolder(file, path);


                    using (Stream fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }

            _context.Update(mm);
            _context.SaveChanges();
            return RedirectToAction("Get_Student_List");
        }

        [StudentUserAuth]
        public IActionResult Profile()
        {
            var StudentId = HttpContext.Session.GetInt32(SessionKeys.StudentId);
            if (StudentId is not null)
            {
                var Result = _context.LmsStudents.FirstOrDefault(a => a.Id == StudentId);
                return View(Result);
            }
            return RedirectToAction("StudentLogin", "Account");
        }

        [StudentUserAuth]
        public IActionResult _IssuedInventory()
        {
            var StudentId = HttpContext.Session.GetInt32(SessionKeys.StudentId);

            var IssuedInventoryIds = _context.LmsInventoryRequests.Where(a => a.StudentId == StudentId && a.RequestStatusId == 2).Select(a => a.InventoryId).ToList();//.Include(a=>a.Inventory)

            List<LmsInventory> list = new List<LmsInventory>();

            foreach (var id in IssuedInventoryIds)
            {
                var book = _context.LmsInventories.Find(id);
                if (book is not null)
                {
                    list.Add(book);
                }

            }
            return View();
        }

        [StudentUserAuth]
        [HttpGet]
        public IActionResult EditProfile()
        {
            var StudentId = HttpContext.Session.GetInt32(SessionKeys.StudentId);

            var student = _context.LmsStudents.Find(StudentId);
            if (student is not null)
            {
                return View(student);
            }

            return View();
        }
        public string UpdateProfileImage(IFormFile file, string filePath)
        {
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(fileStream);
            }

            return string.Empty;
        }

        [StudentUserAuth]
        [HttpPost]
        public IActionResult EditProfile(LmsStudent edit)
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
                    edit.ImagePath = DateTime.Now.Ticks.ToString() + file.FileName;
                    path = Path.Combine(path, edit.ImagePath);
                    // SaveFiletoFolder(file, path);


                    using (Stream fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }

            var StudentId = HttpContext.Session.GetInt32(SessionKeys.StudentId);
            try
            {
                if (StudentId is not null && StudentId == edit.Id)
                {

                    var student = _context.LmsStudents.Find(edit.Id);

                    student.FirstName = edit.FirstName;
                    student.LastName = edit.LastName;
                    student.Email = edit.Email;
                    student.Batch = edit.Batch;
                    student.RollNo = edit.RollNo;
                    student.ImagePath = edit.ImagePath;

                    _context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                return View();
            }
            return RedirectToAction("profile");
        }

        [StudentUserAuth]
  
        public IActionResult AvailbleInventory()
        {
            var StudentId = HttpContext.Session.GetInt32(SessionKeys.StudentId);

            var list = _context.LmsInventories.Where(a => a.IsIssued == false).Include(a => a.BookCodeNavigation).Include(a => a.BookGenre).AsNoTracking().ToList();
            List<AvailableInventoryItem> availableInventory = new List<AvailableInventoryItem>();
            list.ForEach(a =>
            {

                availableInventory.Add(new AvailableInventoryItem
                {

                    BookGenre = a.BookGenre.BookGenre,
                    BookAuthor = a.BookAuthor,
                    BookCode = a.BookCode,
                    BookTitle = a.BookTitle,
                    BookVersion = a.BookVersion,
                    CreatedDate = a.CreatedDate,
                    Id = a.Id,
                    ImageBase64 = a.ImageBase64,
                    ImagePath = a.ImagePath,
                    IsIssued = a.IsIssued,
                    CodeDescription = a.BookCodeNavigation.CodeDescription,
                    alreadyRequested = _context.LmsInventoryRequests.Any(p => p.InventoryId == a.Id && p.StudentId == StudentId && (p.RequestStatusId < 3)), /// 
                });



            });


            //foreach (var item in list)
            //{

            //    //AvailableInventoryItem index = new AvailableInventoryItem();
            //    //index.BookGenre = item.BookGenre;
            //    //index.BookGenre = item.BookGenre;
            //    //availableInventory.Add(index);



            //    availableInventory.Add(new AvailableInventoryItem {

            //        BookGenre = item.BookGenre,
            //        BookAuthor = item.BookAuthor,
            //        BookCode  = item.BookCode,
            //        BookTitle =item.BookTitle,
            //        BookVersion = item.BookVersion,
            //        CreatedDate = item.CreatedDate,
            //        Id = item.Id ,
            //        ImageBase64 = item.ImageBase64,
            //        ImagePath = item.ImagePath,
            //        IsIssued = item.IsIssued,                    
            //        alreadyRequested = _context.LmsInventoryRequests.Any(a => a.InventoryId == item.Id && a.StudentId == StudentId), /// 
            //    });


            //}

            return View(availableInventory);
        }

        [StudentUserAuth]
        public IActionResult RequestInventory(int inventoryid)
        {
            var StudentId = HttpContext.Session.GetInt32(SessionKeys.StudentId);

            LmsInventoryRequest request = new LmsInventoryRequest
            {
                InventoryId = inventoryid,
                StudentId = StudentId ?? 0,
                RequestDate = DateTime.Now,
                RequestStatusId = 1 // 1 for pending,

            };

            _context.Add(request);
            _context.SaveChanges();

            return RedirectToAction("MyRequests");
        }

        [StudentUserAuth]
        public IActionResult MyRequests(int inventoryid)
        {
            var StudentId = HttpContext.Session.GetInt32(SessionKeys.StudentId);


            var requestlist = _context.LmsInventoryRequests.Where(a => a.StudentId == StudentId).Include(a => a.RequestStatus).Include(a => a.Student).Include(a => a.Staff).Include(a => a.Inventory);

            return View(requestlist.ToList());
        }

        [StudentUserAuth]
        public IActionResult IssuedInventory()
        {
            var StudentId = HttpContext.Session.GetInt32(SessionKeys.StudentId);



            var IssuedInventoryIds = _context.LmsInventoryRequests.Where(a => a.StudentId == StudentId && a.RequestStatusId == 2).Select(a => a.InventoryId).ToList();//.Include(a=>a.Inventory)

            List<LmsInventory> list = new List<LmsInventory>();

            foreach (var id in IssuedInventoryIds)
            {
                var book = _context.LmsInventories.Where(a => a.Id == id).Include(a => a.BookCodeNavigation).Include(a => a.BookGenre).FirstOrDefault();
                if (book is not null)
                {
                    list.Add(book);
                }

            }

            return View(list);
        }

        [StudentUserAuth]
        public IActionResult ReturnInventory(int Id)
        {

            var StudentId = HttpContext.Session.GetInt32(SessionKeys.StudentId);

            var system_Settings = _context.LmsDefaultSystemSettings.Find(1);


            //// request Completed

            try
            {
                decimal fineCalculated = 0;


                /// 
                var book = _context.LmsInventories.FirstOrDefault(a => a.Id == Id);
                var EntryWhereBookwasissued = _context.LmsInventoryHistories.
                    OrderByDescending(a => a.CreatedDate).
                    FirstOrDefault(a => a.EntryTypeId == 1 &&
                    a.InventoryId == book.Id);


                var timetoToday_bookwasIssued = DateTime.Now - EntryWhereBookwasissued.CreatedDate;

                if (timetoToday_bookwasIssued.Days > system_Settings.IssuedPeriod)
                {
                    var daysOfLateReturn = timetoToday_bookwasIssued.Days - system_Settings.IssuedPeriod;

                    fineCalculated = daysOfLateReturn * system_Settings.FinePerDay;

                }

                var request = _context.LmsInventoryRequests.FirstOrDefault(a => a.RequestStatusId == 2 && a.InventoryId == Id && a.StudentId == StudentId);

                request.RequestStatusId = 4; /// 4 in case of completed the life cycle of request.

                book.IsIssued = false;

                var obj = new LmsInventoryHistory
                {

                    CreatedDate = DateTime.Now,
                    EntryTypeId = 0,
                    InventoryId = book.Id,
                    StaffId = 1018,
                    StudentId = StudentId ?? 0

                };

                _context.LmsInventoryHistories.Add(obj);

                _context.SaveChanges();


                if (obj is not null && obj.Id > 0 && fineCalculated > 0)
                {
                    _context.LmsFineCalculations.Add(new LmsFineCalculation
                    {

                        DueFine = (int)fineCalculated,
                        IsPaid = false,
                        InventoryRequestHistoryId = obj.Id

                    }); ;

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {


            }

            return RedirectToAction("IssuedInventory");
        }

    }
}
