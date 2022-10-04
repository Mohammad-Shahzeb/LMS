using LibraryManagementSystem.EF_Models;
using LibraryManagementSystem.Filters;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagementSystem.Controllers
{
    [StaffUserAuth]
    public class InventoryController : Controller
    {
        private readonly LMS_Context _Context = new LMS_Context();
        private string? inventory;

        public IActionResult Index()
        {

            IEnumerable<IGrouping<int,LmsInventory>> inventory = _Context.LmsInventories.ToList().GroupBy(a => a.BookCode);
            return View(inventory);
        }
        [HttpPost]
        public IActionResult InventoryCodeDetail(InventorySearchModel searchModel)
        {
            IQueryable<LmsInventory> query = _Context.LmsInventories;

            if (!string.IsNullOrEmpty(searchModel.BookTitle))
            {
                query = query.Where(a=> a.BookTitle == searchModel.BookTitle);  
            }

            if (searchModel.BookCode is not null)
            {
                query = query.Where(a => a.BookCode == searchModel.BookCode);
            }

            if (searchModel.IsIssued is not null)
            {
                query = query.Where(a => a.IsIssued == searchModel.IsIssued);
            }

            if(searchModel.FromDate is not null)
            {
                var date = Convert.ToDateTime(searchModel.FromDate).Date;
                query = query.Where(a => a.CreatedDate >= date);
            }

            if(searchModel.ToDate is not null)
            {
                var date = Convert.ToDateTime(searchModel.ToDate).Date.AddDays(1).AddSeconds(-1);
                query = query.Where(a => a.CreatedDate <= date);
            }


            var list = query.ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create_Inventory()
        {
            ViewData["BookCode"] = _Context.LmsInventoryCodes.Select(a =>
                             new SelectListItem { Value = a.Id.ToString(), Text = a.Code }).ToList();
            return View(new LmsInventory());
        }

        public string SaveFileToFolder(IFormFile file, string filepath)
        {
            using (Stream filestream = new FileStream(filepath, FileMode.Create))
            {
                file.CopyToAsync(filestream);
            }
            return string.Empty;
        }

        [HttpPost]
        public IActionResult Create_Inventory(LmsInventory inventory)
        {

            string path = "wwwroot/InventoryImages";
            
            var files = Request.Form.Files;

            if(files.Count > 0)
            {
                var file = files["ImagePath"];

                if(file.Length > 0)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    inventory.ImagePath = DateTime.Now.Ticks.ToString() + file.FileName;
                    path = Path.Combine(path, inventory.ImagePath);

                    using (Stream filestream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                }
            }

            try
            {
                inventory.CreatedDate = DateTime.Now;
                inventory.IsIssued = false;
                _Context.Add(inventory);
                _Context.SaveChanges();
            }

            catch(Exception ex)
            {
                return View();
            }
            
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int lm)
        {
            var inventory = _Context.LmsInventories.FirstOrDefault(a=> a.Id == lm);
            if (inventory is not null)
            {
                _Context.Remove(inventory);
                _Context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int lm)
        {
            ViewData["BookCode"] = _Context.LmsInventoryCodes.Select(a =>
                             new SelectListItem { Value = a.Id.ToString(), Text = a.Code }).ToList();
            var Edit = _Context.LmsInventories.FirstOrDefault(a=> a.Id == lm);

            if(Edit is not null)
            {
                return View(Edit);
            }
            return View();
        }
        public string UpdateFileToFolder(IFormFile file, string filepath)
        {
            using (Stream filestream = new FileStream(filepath, FileMode.Create))
            {
                file.CopyToAsync(filestream);
            }
            return string.Empty;
        }

        [HttpPost]
        public IActionResult Edit(LmsInventory lm)
        {
            string path = "wwwroot/InventoryImages";

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
                    lm.ImagePath = DateTime.Now.Ticks.ToString() + file.FileName;
                    path = Path.Combine(path, lm.ImagePath);

                    using (Stream filestream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                }
            }
            lm.IsIssued = false;
            _Context.Update(lm);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Copy(int InventoryId)
        {

            var inventory = _Context.LmsInventories.Find(InventoryId);

            if (inventory is not null)
            {
                try
                {
                    _Context.LmsInventories.Add(new LmsInventory
                    {
                        BookAuthor = inventory.BookAuthor,
                        BookCode = inventory.BookCode,
                        BookTitle = inventory.BookTitle,
                        BookGenre = inventory.BookGenre,
                        BookVersion = inventory.BookVersion,
                        ImagePath = inventory.ImagePath,
                        IsIssued = false,
                        CreatedDate = DateTime.Now
                    });

                    _Context.SaveChanges();
                }
                catch (Exception ex)
                {

                    TempData["key"] = $"There was an error Copying this Inventory{InventoryId}{ex.Message}";
                    
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult InventoryCodeDetail(int Code)
        {
            var list = _Context.LmsInventories.Where(a => a.BookCode == Code);
            return View(list.ToList());
        }

    }
}
