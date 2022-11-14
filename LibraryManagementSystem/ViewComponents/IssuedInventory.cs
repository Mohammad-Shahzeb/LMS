using LibraryManagementSystem.EF_Models;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LibraryManagementSystem.ViewComponents
{
    public class IssuedInventory : ViewComponent
    {
       private readonly LMS_Context _context = new LMS_Context();

        public IViewComponentResult Invoke()
        {
            var StudentId = HttpContext.Session.GetInt32(SessionKeys.StudentId);

            var IssuedInventoryIds = _context.LmsInventoryRequests.Where(a => a.StudentId == StudentId && a.RequestStatusId == 2).Select(a => a.InventoryId).ToList();//.Include(a=>a.Inventory)

            List<LmsInventory> list = new List<LmsInventory>();

            foreach (var id in IssuedInventoryIds)
            {
                var book = _context.LmsInventories.Where(a=>a.Id == id).Include(a=>a.BookCodeNavigation).Include(a=>a.BookGenre).FirstOrDefault();
                if (book is not null)
                {
                    list.Add(book);
                }

            }
            return View(list);
        }

    }
}
