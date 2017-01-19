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

       
        // GET: Colaboradors/Delete/5
        public async Task<IActionResult> Delete(string id)
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
