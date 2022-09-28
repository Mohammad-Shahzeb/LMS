using LibraryManagementSystem.EF_Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class InventoryRequestController : Controller
    {
        private readonly LMS_Context _context = new LMS_Context();
        
        public IActionResult Index()
        {
            var list = _context.LmsInventoryRequests.OrderByDescending(a => a.RequestDate);
            return View(list.ToList());
        }

    }
}
