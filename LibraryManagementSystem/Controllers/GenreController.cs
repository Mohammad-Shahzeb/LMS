using LibraryManagementSystem.EF_Models;
using LibraryManagementSystem.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.Controllers
{
    [StaffUserAuth]
    public class GenreController : Controller
    {
        private readonly LMS_Context _context = new LMS_Context();
        
        public IActionResult Index()
        {
            var BookGenre = _context.LmsBookGenres.ToList();
            return View(BookGenre);
        }

        [HttpGet]
        public IActionResult CreateGenre()
        {
            return View(new LmsBookGenre());
        }

        [HttpPost]
        public IActionResult CreateGenre(LmsBookGenre data)
        {
            try
            {
                _context.LmsBookGenres.Add(data);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditGenre(int id)
        {
            var result = _context.LmsBookGenres.Find(id);
            if (result is not null)
            {
                return View(result);
            }
            return View();
        }

        [HttpPost]
        public IActionResult EditGenre(LmsBookGenre data)
        {
            try
            {
                _context.LmsBookGenres.Update(data);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return View();
            }
            return RedirectToAction("Index");

        }

        public IActionResult DeleteGenre(int Id)
        {
            var result = _context.LmsBookGenres.FirstOrDefault(a => a.BookGenreId == Id);
            try
            {
                if (result is not null)
                {
                    _context.LmsBookGenres.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return View(); 
            }

            return RedirectToAction("Index");
        }


        public bool IsValidGenre(string BookGenre, int BookGenreId = 0)
        {
            if (BookGenreId == 0)
            {
                return !_context.LmsBookGenres.Any(a => a.BookGenre == BookGenre);
            }
            else
            {
                return !_context.LmsBookGenres.Any(a => a.BookGenre == BookGenre && a.BookGenreId != BookGenreId);
            }

        }

    }
}
