using Bolsa.DAL;
using Bolsa.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bolsa.MVC.Controllers
{
    public class PostulacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostulacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Postulaciones
        public async Task<IActionResult> Index()
        {
            var lista = await _context.Postulaciones.ToListAsync();
            return View(lista);
        }

        // GET: Postulaciones/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var postulacion = await _context.Postulaciones.FindAsync(id);
            if (postulacion == null) return NotFound();
            return View(postulacion);
        }

        // GET: Postulaciones/Create
        public async Task<IActionResult> Create()
        {
            var plazas = await _context.Plazas.ToListAsync();
            ViewBag.Plazas = new SelectList(plazas, "IdPlaza", "Titulo");
            return View();
        }

        // POST: Postulaciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Postulacion postulacion)
        {
            postulacion.FechaPostulacion = DateTime.Now;
            if (string.IsNullOrEmpty(postulacion.Estado))
            {
                postulacion.Estado = "En revisión";
            }

            _context.Postulaciones.Add(postulacion);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "¡Postulación creada correctamente!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Postulaciones/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var postulacion = await _context.Postulaciones.FindAsync(id);
            if (postulacion == null) return NotFound();

            var plazas = await _context.Plazas.ToListAsync();
            ViewBag.Plazas = new SelectList(plazas, "IdPlaza", "Titulo", postulacion.IdPlaza);
            return View(postulacion);
        }

        // POST: Postulaciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Postulacion postulacion)
        {
            if (id != postulacion.IdPostulacion) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postulacion);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "¡Postulación actualizada correctamente!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostulacionExists(postulacion.IdPostulacion)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var plazas = await _context.Plazas.ToListAsync();
            ViewBag.Plazas = new SelectList(plazas, "IdPlaza", "Titulo", postulacion.IdPlaza);
            return View(postulacion);
        }

        // GET: Postulaciones/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var postulacion = await _context.Postulaciones.FindAsync(id);
            if (postulacion == null) return NotFound();
            return View(postulacion);
        }

        // POST: Postulaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postulacion = await _context.Postulaciones.FindAsync(id);
            if (postulacion != null)
            {
                _context.Postulaciones.Remove(postulacion);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "¡Postulación eliminada correctamente!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PostulacionExists(int id)
        {
            return _context.Postulaciones.Any(e => e.IdPostulacion == id);
        }
    }
}