using LibraryManagementSystem.EF_Models;
using LibraryManagementSystem.Filters;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    [StaffUserAuth]
    public class InventoryHistoryController : Controller
    {
        private readonly LMS_Context _context = new LMS_Context();


        [Exceptionhandler]
        public IActionResult Index()
        {


         

            HttpContext.Session.SetString("MyKey", "shaab");
            var list = _context.LmsInventoryHistories.Include(a=> a.Staff).Include(a=> a.Student).Include(a=> a.Inventory)
                .ToList();
            return View(list);
        }

        [HttpPost]
        public IActionResult Index(InventoryHistorySearchModel searchModel)
        {
            var query = _context.LmsInventoryHistories.Include(a => a.Staff).Include(a => a.Student).Include(a => a.Inventory);
               
            if(!string.IsNullOrEmpty(searchModel.StudentName))
            {
                query = query.Where(a => a.Student.FirstName == searchModel.StudentName).Include(a => a.Staff).Include(a => a.Student).Include(a => a.Inventory);
            }

            if (!string.IsNullOrEmpty(searchModel.StaffName))
            {
                query = query.Where(a => a.Staff.FirstName == searchModel.StaffName).Include(a => a.Staff).Include(a => a.Student).Include(a => a.Inventory);

            }

            if (!string.IsNullOrEmpty(searchModel.InventoryTitle))
            {
                query = query.Where(a => a.Inventory.BookTitle == searchModel.InventoryTitle).Include(a => a.Staff).Include(a => a.Student).Include(a => a.Inventory);

            }

            if (!string.IsNullOrEmpty(searchModel.EntryType))
            {
                query = query.Where(a => a.EntryType == searchModel.EntryType).Include(a => a.Staff).Include(a => a.Student).Include(a => a.Inventory);

            }

            if (searchModel.FromDate is not null)
            {
                var date = Convert.ToDateTime(searchModel.FromDate).Date;
                query = query.Where(a => a.CreatedDate >= date).Include(a => a.Staff).Include(a => a.Student).Include(a => a.Inventory);

            }

            if (searchModel.ToDate is not null)
            {
                var date = Convert.ToDateTime(searchModel.ToDate).Date.AddDays(1).AddSeconds(-1);
                query = query.Where(a => a.CreatedDate <= date).Include(a => a.Staff).Include(a => a.Student).Include(a => a.Inventory);


            }

            var list = query.ToList();

            return View(list);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Create_InventoryHistory()
        {
            ViewData["StudentId"] = _context.LmsStudents.Select(a =>
                        new SelectListItem { Value = a.Id.ToString(), Text = a.FirstName + " " + a.LastName }).ToList();
            ViewData["StaffId"] = _context.LmsStaffs.Select(a =>
                        new SelectListItem { Value = a.Id.ToString(), Text = a.FirstName + " " + a.LastName }).ToList();
            ViewData["InventoryId"] = _context.LmsInventories.Select(a =>
                        new SelectListItem { Value = a.Id.ToString(), Text = a.BookTitle + " " + a.BookCode }).ToList();
            return View(new LmsInventoryHistory());
        }

        [HttpPost]
        public IActionResult Create_InventoryHistory(LmsInventoryHistory data)
        {
            try
            {
                data.CreatedDate = DateTime.Now;
                _context.Add(data);
               
                _context.SaveChanges();
            }

            catch (Exception ex)
            {
                var s = ex.Message;
                return View();
            }
            return RedirectToAction("Index");
        }
        public IActionResult InventoryTimeline(int inventoryId) {
            var list = _context.LmsInventoryHistories.Where(a=> a.InventoryId == inventoryId).Include(a => a.Staff).Include(a => a.Student).Include(a => a.Inventory)
                .ToList();
            return View(list);
        }
    }
}
