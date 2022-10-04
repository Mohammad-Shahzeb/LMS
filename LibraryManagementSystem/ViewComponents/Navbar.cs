using LibraryManagementSystem.EF_Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.ViewComponents
{
    public class Navbar : ViewComponent
    {
        private readonly LMS_Context _Context = new LMS_Context();

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
