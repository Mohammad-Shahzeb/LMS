using LibraryManagementSystem.EF_Models;
using LibraryManagementSystem.Filters;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
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
            var inventory = _Context.LmsInventories.OrderByDescending(a=> a.CreatedDate).ToList();
            return View(inventory);
        }
        [HttpPost]
        public IActionResult Index(InventorySearchModel searchModel)
        {
            IQueryable<LmsInventory> query = _Context.LmsInventories;

            if (!string.IsNullOrEmpty(searchModel.BookTitle))
            {
                query = query.Where(a=> a.BookTitle == searchModel.BookTitle);  
            }

            if (!string.IsNullOrEmpty(searchModel.BookCode))
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
            return View();
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
            var Edit = _Context.LmsInventories.FirstOrDefault(a=> a.Id == lm);

            if(Edit is not null)
            {
                return View(Edit);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(LmsInventory lm)
        {
            _Context.Update(lm);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
