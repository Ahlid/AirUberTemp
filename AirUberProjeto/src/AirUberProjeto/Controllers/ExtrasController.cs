using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirUberProjeto.Data;
using AirUberProjeto.Models;

namespace AirUberProjeto.Controllers
{
    public class ExtrasController : Controller
    {
        private readonly AirUberDbContext _context;

        public ExtrasController(AirUberDbContext context)
        {
            _context = context;    
        }

        // GET: Extras
        public async Task<IActionResult> Index()
        {
            var airUberDbContext = _context.Extra.Include(e => e.Companhia).Include(e => e.TipoExtra);
            return View(await airUberDbContext.ToListAsync());
        }

        // GET: Extras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extra = await _context.Extra.SingleOrDefaultAsync(m => m.ExtraId == id);
            if (extra == null)
            {
                return NotFound();
            }

            return View(extra);
        }

        // GET: Extras/Create
        public IActionResult Create()
        {
            ViewData["CompanhiaId"] = new SelectList(_context.Companhia, "CompanhiaId", "Contact");
            ViewData["TipoExtraId"] = new SelectList(_context.TipoExtra, "TipoExtraId", "Nome");
            return View();
        }

        // POST: Extras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExtraId,CompanhiaId,TipoExtraId,Valor")] Extra extra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(extra);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CompanhiaId"] = new SelectList(_context.Companhia, "CompanhiaId", "Contact", extra.CompanhiaId);
            ViewData["TipoExtraId"] = new SelectList(_context.TipoExtra, "TipoExtraId", "Nome", extra.TipoExtraId);
            return View(extra);
        }

        // GET: Extras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extra = await _context.Extra.SingleOrDefaultAsync(m => m.ExtraId == id);
            if (extra == null)
            {
                return NotFound();
            }
            ViewData["CompanhiaId"] = new SelectList(_context.Companhia, "CompanhiaId", "Contact", extra.CompanhiaId);
            ViewData["TipoExtraId"] = new SelectList(_context.TipoExtra, "TipoExtraId", "Nome", extra.TipoExtraId);
            return View(extra);
        }

        // POST: Extras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExtraId,CompanhiaId,TipoExtraId,Valor")] Extra extra)
        {
            if (id != extra.ExtraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(extra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExtraExists(extra.ExtraId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["CompanhiaId"] = new SelectList(_context.Companhia, "CompanhiaId", "Contact", extra.CompanhiaId);
            ViewData["TipoExtraId"] = new SelectList(_context.TipoExtra, "TipoExtraId", "Nome", extra.TipoExtraId);
            return View(extra);
        }

        // GET: Extras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extra = await _context.Extra.SingleOrDefaultAsync(m => m.ExtraId == id);
            if (extra == null)
            {
                return NotFound();
            }

            return View(extra);
        }

        // POST: Extras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var extra = await _context.Extra.SingleOrDefaultAsync(m => m.ExtraId == id);
            _context.Extra.Remove(extra);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ExtraExists(int id)
        {
            return _context.Extra.Any(e => e.ExtraId == id);
        }
    }
}
