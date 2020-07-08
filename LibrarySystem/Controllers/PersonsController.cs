﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    public class PersonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Persons
        public async Task<IActionResult> Index()
        {
            var libraryDBContext = _context.Persons.Include(p => p.Maritals).Include(p => p.Religions).Include(p => p.Sexs);
            return View(await libraryDBContext.ToListAsync());
        }

        // GET: Persons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persons = await _context.Persons
                .Include(p => p.Maritals)
                .Include(p => p.Religions)
                .Include(p => p.Sexs)
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (persons == null)
            {
                return NotFound();
            }

            return View(persons);
        }

        // GET: Persons/Create
        public IActionResult Create()
        {
            ViewData["MaritalID"] = new SelectList(_context.Set<Maritals>(), "MaritalsName", "MaritalsName");
            ViewData["ReligionID"] = new SelectList(_context.Set<Religions>(), "ReligionName", "ReligionName");
            ViewData["SexID"] = new SelectList(_context.Set<Sexs>(), "SexName", "SexName");
            return View();
        }

        // POST: Persons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonID,PersonName,SexID,MaritalID,ReligionID,PhoneNumber,Address,City,Country")] Persons persons)
        {
            if (ModelState.IsValid)
            {
                _context.Add(persons);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaritalID"] = new SelectList(_context.Set<Maritals>(), "MaritalID", "MaritalsName", persons.MaritalID);
            ViewData["ReligionID"] = new SelectList(_context.Set<Religions>(), "ReligionID", "ReligionName", persons.ReligionID);
            ViewData["SexID"] = new SelectList(_context.Set<Sexs>(), "SexID", "SexName", persons.SexID);
            return View(persons);
        }

        // GET: Persons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persons = await _context.Persons.FindAsync(id);
            if (persons == null)
            {
                return NotFound();
            }
            ViewData["MaritalID"] = new SelectList(_context.Set<Maritals>(), "MaritalID", "MaritalsName", persons.MaritalID);
            ViewData["ReligionID"] = new SelectList(_context.Set<Religions>(), "ReligionID", "ReligionName", persons.ReligionID);
            ViewData["SexID"] = new SelectList(_context.Set<Sexs>(), "SexID", "SexName", persons.SexID);
            return View(persons);
        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonID,PersonName,SexID,MaritalID,ReligionID,PhoneNumber,Address,City,Country")] Persons persons)
        {
            if (id != persons.PersonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persons);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonsExists(persons.PersonID))
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
            ViewData["MaritalID"] = new SelectList(_context.Set<Maritals>(), "MaritalsName", "MaritalsName", persons.MaritalID);
            ViewData["ReligionID"] = new SelectList(_context.Set<Religions>(), "ReligionName", "ReligionName", persons.ReligionID);
            ViewData["SexID"] = new SelectList(_context.Set<Sexs>(), "SexName", "SexName", persons.SexID);
            return View(persons);
        }

        // GET: Persons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persons = await _context.Persons
                .Include(p => p.Maritals)
                .Include(p => p.Religions)
                .Include(p => p.Sexs)
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (persons == null)
            {
                return NotFound();
            }

            return View(persons);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persons = await _context.Persons.FindAsync(id);
            _context.Persons.Remove(persons);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonsExists(int id)
        {
            return _context.Persons.Any(e => e.PersonID == id);
        }
    }
}
