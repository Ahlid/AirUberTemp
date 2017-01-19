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
    public class ColaboradorsController : Controller
    {
        private readonly AirUberDbContext _context;

        public ColaboradorsController(AirUberDbContext context)
        {
            _context = context;    
        }

        // GET: Colaboradors
        public async Task<IActionResult> Index()
        {
            var airUberDbContext = _context.Colaborador.Include(c => c.Companhia);
            return View(await airUberDbContext.ToListAsync());
        }

        // GET: Colaboradors/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colaborador = await _context.Colaborador.SingleOrDefaultAsync(m => m.Id == id);
            if (colaborador == null)
            {
                return NotFound();
            }

            return View(colaborador);
        }

        // GET: Colaboradors/Create
        public IActionResult Create()
        {
            ViewData["CompanhiaId"] = new SelectList(_context.Companhia, "CompanhiaId", "Contact");
            return View();
        }

        // POST: Colaboradors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccessFailedCount,Apelido,Ativo,ConcurrencyStamp,DataCriacao,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,Nome,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,CompanhiaId,IsAdministrador")] Colaborador colaborador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(colaborador);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CompanhiaId"] = new SelectList(_context.Companhia, "CompanhiaId", "Contact", colaborador.CompanhiaId);
            return View(colaborador);
        }

        // GET: Colaboradors/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colaborador = await _context.Colaborador.SingleOrDefaultAsync(m => m.Id == id);
            if (colaborador == null)
            {
                return NotFound();
            }
            ViewData["CompanhiaId"] = new SelectList(_context.Companhia, "CompanhiaId", "Contact", colaborador.CompanhiaId);
            return View(colaborador);
        }

        // POST: Colaboradors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,AccessFailedCount,Apelido,Ativo,ConcurrencyStamp,DataCriacao,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,Nome,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,CompanhiaId,IsAdministrador")] Colaborador colaborador)
        {
            if (id != colaborador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(colaborador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColaboradorExists(colaborador.Id))
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
            ViewData["CompanhiaId"] = new SelectList(_context.Companhia, "CompanhiaId", "Contact", colaborador.CompanhiaId);
            return View(colaborador);
        }

        // GET: Colaboradors/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colaborador = await _context.Colaborador.SingleOrDefaultAsync(m => m.Id == id);
            if (colaborador == null)
            {
                return NotFound();
            }

            return View(colaborador);
        }

        // POST: Colaboradors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var colaborador = await _context.Colaborador.SingleOrDefaultAsync(m => m.Id == id);
            _context.Colaborador.Remove(colaborador);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ColaboradorExists(string id)
        {
            return _context.Colaborador.Any(e => e.Id == id);
        }
    }
}
