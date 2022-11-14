using LibraryManagementSystem.EF_Models;
using LibraryManagementSystem.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    [StaffUserAuth]
    public class FineCalculationController : Controller
    {
        private readonly LMS_Context _context = new LMS_Context();
        public IActionResult Index()
        {
            var list = _context.LmsFineCalculations
                 .Include(a => a.InventoryRequestHistory).ThenInclude(a => a.Student)
                 .Include(a => a.InventoryRequestHistory).ThenInclude(a => a.Inventory);
            return View(list.ToList());
        }


    }
}
