using Bolsa.DAL;
using Bolsa.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bolsa.MVC.Controllers
{
    public class HojaDeVidaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HojaDeVidaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HojaDeVida
        public async Task<IActionResult> Index()
        {
            var lista = await _context.HojasDeVida.ToListAsync();
            return View(lista);
        }

        // GET: HojaDeVida/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var cv = await _context.HojasDeVida.FindAsync(id);
            if (cv == null) return NotFound();
            return View(cv);
        }

        // GET: HojaDeVida/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HojaDeVida/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HojaDeVida hojaDeVida)
        {
            if (ModelState.IsValid)
            {
                _context.HojasDeVida.Add(hojaDeVida);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Hoja de vida creada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(hojaDeVida);
        }

        // GET: HojaDeVida/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cv = await _context.HojasDeVida.FindAsync(id);
            if (cv == null) return NotFound();
            return View(cv);
        }

        // POST: HojaDeVida/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HojaDeVida hojaDeVida)
        {
            if (id != hojaDeVida.IdCV) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hojaDeVida);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Hoja de vida actualizada correctamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HojaDeVidaExists(hojaDeVida.IdCV)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hojaDeVida);
        }

        // GET: HojaDeVida/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cv = await _context.HojasDeVida.FindAsync(id);
            if (cv == null) return NotFound();
            return View(cv);
        }

        // POST: HojaDeVida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cv = await _context.HojasDeVida.FindAsync(id);
            if (cv != null)
            {
                _context.HojasDeVida.Remove(cv);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Hoja de vida eliminada correctamente.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool HojaDeVidaExists(int id)
        {
            return _context.HojasDeVida.Any(e => e.IdCV == id);
        }
    }
}