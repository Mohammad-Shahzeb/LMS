using LibraryManagementSystem.EF_Models;
using LibraryManagementSystem.Filters;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
            var list = _context.LmsStaffs.OrderByDescending(a => a.CreatedDate);
            return View(list.ToList());
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
        public string UpdateFileToFolder(IFormFile file, string filepath)
        {
            using (Stream filestream = new FileStream(filepath, FileMode.Create))
            {
                file.CopyToAsync(filestream);
            }

            return String.Empty;
        }

        [HttpPost]
        public IActionResult Edit(LmsStaff cr)
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
                    cr.ImagePath = DateTime.Now.Ticks.ToString() + file.FileName;
                    path = Path.Combine(path, cr.ImagePath);

                    using (Stream filestream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                }

            }

            _context.Update(cr);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult allRequests()
        {
            var list = _context.LmsInventoryRequests.Include(a => a.Inventory).Include(a => a.Student).Include(a => a.Staff);
            return View(list.ToList());
        }

        public IActionResult updateRequest(int requestId, int ActionId)
        {
            var StaffId = HttpContext.Session.GetInt32(SessionKeys.StaffId);

            var request = _context.LmsInventoryRequests.FirstOrDefault(a => a.Id == requestId);





            if (ActionId == 2)
            {
                try
                {
                    request.RequestStatusId = 2;
                    request.StaffId = StaffId;
                    request.ModifiedDate = DateTime.Now;


                    var inventory = _context.LmsInventories.FirstOrDefault(a => a.Id == request.InventoryId);

                    if (inventory is not null)
                    {
                        inventory.IsIssued = true;


                        LmsInventoryHistory history = new LmsInventoryHistory()
                        {

                            CreatedDate = DateTime.Now,
                            EntryTypeId = 1, // 1 for issue

                            InventoryId = inventory.Id,
                            StudentId = request.StudentId,
                            StaffId = StaffId ?? 0,

                        };
                        _context.Add(history);
                    }


                    _context.SaveChanges();


                    if (request is not null)
                    {
                        var all_remainingRequest_against_same_inventory = _context.LmsInventoryRequests.
                                                                                                                    Where(a => a.Id != requestId && a.InventoryId == request.InventoryId).AsQueryable();



                        foreach (var item in all_remainingRequest_against_same_inventory)
                        {
                            item.RequestStatusId = 3;
                            item.ModifiedDate = DateTime.Now;
                            item.StaffId = StaffId;
                        }


                        

                        _context.SaveChanges();
                    }



                }
                catch (Exception ex)
                {


                }

            }
            else if (ActionId == 3)
            {
                request.RequestStatusId = 3;
                request.StaffId = StaffId;
                request.ModifiedDate = DateTime.Now;

            }
            _context.SaveChanges();



            return RedirectToAction("allRequests");
        }

        //public IActionResult CancelRequests(int requestId, int ActionId)
        //{
        //    var StaffId = HttpContext.Session.GetInt32(SessionKeys.StaffId);

        //    var request = _context.LmsInventoryRequests.FirstOrDefault(a => a.Id == requestId);

        //    if (ActionId == 1)
        //    {
        //        try
        //        {
        //            request.Status = 3;
        //            request.StaffId = StaffId;
        //            request.ModifiedDate = DateTime.Now;
        //        }
        //        catch (Exception ex)
        //        {
        //        }


        //    }

        //    return View();
        //}

    }
}
