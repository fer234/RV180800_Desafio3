using Bolsa.BLL.Services;
using Bolsa.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bolsa.MVC.Controllers
{
    public class PlazasController : Controller
    {
        private readonly PlazaService _plazaService;

        public PlazasController(PlazaService plazaService)
        {
            _plazaService = plazaService;
        }

        // GET: Plazas
        public async Task<IActionResult> Index()
        {
            var plazas = await _plazaService.GetAllPlazasAsync();
            return View(plazas);
        }

        // GET: Plazas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var plaza = await _plazaService.GetPlazaByIdAsync(id);
            if (plaza == null)
            {
                return NotFound();
            }
            return View(plaza);
        }

        // GET: Plazas/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Plaza plaza)
        {
 
            plaza.IdAgente = "Admin-General";
            plaza.Estado = string.IsNullOrEmpty(plaza.Estado) ? "Abierta" : plaza.Estado;
            plaza.FechaPublicacion = DateTime.Now;

            ModelState.Remove("IdAgente");
            ModelState.Remove("Estado");

            if (ModelState.IsValid)
            {
                try
                {
                    await _plazaService.CreatePlazaAsync(plaza);
                    TempData["SuccessMessage"] = "Plaza creada exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar en la base de datos: " + ex.Message);
                }
            }

            return View(plaza);
        }

        // GET: Plazas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var plaza = await _plazaService.GetPlazaByIdAsync(id);
            if (plaza == null)
            {
                return NotFound();
            }
            return View(plaza);
        }

        // POST: Plazas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Plaza plaza)
        {
            if (id != plaza.IdPlaza)
            {
                return NotFound();
            }

            ModelState.Remove("IdAgente");

            if (ModelState.IsValid)
            {
                try
                {
                    await _plazaService.UpdatePlazaAsync(plaza);
                    TempData["SuccessMessage"] = "Plaza actualizada exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar: " + ex.Message);
                }
            }
            return View(plaza);
        }

        // POST: Plazas/CambiarEstado/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarEstado(int id, string nuevoEstado)
        {
            await _plazaService.ChangePlazaStatusAsync(id, nuevoEstado);
            TempData["SuccessMessage"] = "El estado de la plaza ha sido actualizado.";
            return RedirectToAction(nameof(Index));
        }
    }
}