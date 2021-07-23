using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warm_Up_Challenge.Data;
using Warm_Up_Challenge.Models;

namespace Warm_Up_Challenge.Controllers
{
    public class FormsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Forms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Form.ToListAsync());
        }
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }
        public async Task<IActionResult> ShowSearchResults(string searchTitle)
        {
            return View("Index", await _context.Form.Where(f => f.Title == searchTitle).ToListAsync());
        }

        // GET: Forms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Form
                .FirstOrDefaultAsync(m => m.FormId == id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // GET: Forms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Forms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormId,Title,Content,ImageUrl,Category,CreationDate")] Form form)
        {
            if (ModelState.IsValid)
            {
                _context.Add(form);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(form);
        }

        // GET: Forms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Form.FindAsync(id);
            if (form == null)
            {
                return NotFound();
            }
            return View(form);
        }

        // POST: Forms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormId,Title,Content,ImageUrl,Category,CreationDate")] Form form)
        {
            if (id != form.FormId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(form);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormExists(form.FormId))
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
            return View(form);
        }

        // GET: Forms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Form
                .FirstOrDefaultAsync(m => m.FormId == id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // POST: Forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var form = await _context.Form.FindAsync(id);
            _context.Form.Remove(form);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormExists(int id)
        {
            return _context.Form.Any(e => e.FormId == id);
        }
    }
}
