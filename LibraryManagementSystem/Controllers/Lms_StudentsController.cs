using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.EF_Models;
using LibraryManagementSystem.Filters;

namespace LibraryManagementSystem.Controllers
{

   
    public class Lms_StudentsController : Controller // our class is being inherited from built in base class
    {
        private readonly LMS_Context _context =new LMS_Context();

        //public Lms_StudentsController(LMS_Context context)
        //{
        //    _context = context;
        //}

        // GET: Lms_Students
        public async Task<IActionResult> Index()
        {
              return _context.LmsStudents != null ? 
                          View(await _context.LmsStudents.ToListAsync()) :
                          Problem("Entity set 'LMS_Context.LmsStudents'  is null.");
        }

        // GET: Lms_Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LmsStudents == null)
            {
                return NotFound();
            }

            var lmsStudent = await _context.LmsStudents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lmsStudent == null)
            {
                return NotFound();
            }

            return View(lmsStudent);
        }

        // GET: Lms_Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lms_Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LmsStudent lmsStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lmsStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lmsStudent);
        }

        // GET: Lms_Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LmsStudents == null)
            {
                return NotFound();
            }

            var lmsStudent = await _context.LmsStudents.FindAsync(id);
            if (lmsStudent == null)
            {
                return NotFound();
            }
            return View(lmsStudent);
        }

        // POST: Lms_Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,RollNo,Batch,Email,Password,CreatedDate")] LmsStudent lmsStudent)
        {
            if (id != lmsStudent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lmsStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LmsStudentExists(lmsStudent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lmsStudent);
        }

        // GET: Lms_Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LmsStudents == null)
            {
                return NotFound();
            }

            var lmsStudent = await _context.LmsStudents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lmsStudent == null)
            {
                return NotFound();
            }

            return View(lmsStudent);
        }

        // POST: Lms_Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LmsStudents == null)
            {
                return Problem("Entity set 'LMS_Context.LmsStudents'  is null.");
            }
            var lmsStudent = await _context.LmsStudents.FindAsync(id);
            if (lmsStudent != null)
            {
                _context.LmsStudents.Remove(lmsStudent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LmsStudentExists(int id)
        {
          return (_context.LmsStudents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
