using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    public class LibrarySystemRunsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibrarySystemRunsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LibrarySystemRuns
        public async Task<IActionResult> Index()
        {
            var libraryDBContext = _context.LibrarySystemRuns.Include(l => l.Bookis).Include(l => l.Persons);
            return View(await libraryDBContext.ToListAsync());
        }

        // GET: LibrarySystemRuns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librarySystemRun = await _context.LibrarySystemRuns
                .Include(l => l.Bookis)
                .Include(l => l.Persons)
                .FirstOrDefaultAsync(m => m.LibraryID == id);
            if (librarySystemRun == null)
            {
                return NotFound();
            }

            return View(librarySystemRun);
        }

        // GET: LibrarySystemRuns/Create
        public IActionResult Create()
        {
            ViewData["BooksID"] = new SelectList(_context.Books, "BooksID", "Title");
            ViewData["PersonID"] = new SelectList(_context.Persons, "PersonID", "PersonName");
            return View();
        }

        // POST: LibrarySystemRuns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LibraryID,PersonID,BooksID,BorrowedDate,DateBack,LengthBorrowed,Descriptions")] LibrarySystemRun librarySystemRun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(librarySystemRun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BooksID"] = new SelectList(_context.Books, "BooksID", "Title", librarySystemRun.BooksID);
            ViewData["PersonID"] = new SelectList(_context.Persons, "PersonID", "PersonName", librarySystemRun.PersonID);
            return View(librarySystemRun);
        }

        // GET: LibrarySystemRuns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librarySystemRun = await _context.LibrarySystemRuns.FindAsync(id);
            if (librarySystemRun == null)
            {
                return NotFound();
            }
            ViewData["BooksID"] = new SelectList(_context.Books, "BooksID", "Title", librarySystemRun.BooksID);
            ViewData["PersonID"] = new SelectList(_context.Persons, "PersonID", "PersonName", librarySystemRun.PersonID);
            return View(librarySystemRun);
        }

        // POST: LibrarySystemRuns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LibraryID,PersonID,BooksID,BorrowedDate,DateBack,LengthBorrowed,Descriptions")] LibrarySystemRun librarySystemRun)
        {
            if (id != librarySystemRun.LibraryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(librarySystemRun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibrarySystemRunExists(librarySystemRun.LibraryID))
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
            ViewData["BooksID"] = new SelectList(_context.Books, "BooksID", "Title", librarySystemRun.BooksID);
            ViewData["PersonID"] = new SelectList(_context.Persons, "PersonID", "PersonName", librarySystemRun.PersonID);
            return View(librarySystemRun);
        }

        // GET: LibrarySystemRuns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librarySystemRun = await _context.LibrarySystemRuns
                .Include(l => l.Bookis)
                .Include(l => l.Persons)
                .FirstOrDefaultAsync(m => m.LibraryID == id);
            if (librarySystemRun == null)
            {
                return NotFound();
            }

            return View(librarySystemRun);
        }

        // POST: LibrarySystemRuns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var librarySystemRun = await _context.LibrarySystemRuns.FindAsync(id);
            _context.LibrarySystemRuns.Remove(librarySystemRun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibrarySystemRunExists(int id)
        {
            return _context.LibrarySystemRuns.Any(e => e.LibraryID == id);
        }
    }
}
