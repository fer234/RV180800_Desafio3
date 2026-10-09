using Bolsa.DAL;
using Bolsa.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolsa.BLL.Services
{
    public class PlazaService
    {
        private readonly ApplicationDbContext _context;

        public PlazaService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todas las plazas abiertas
        public async Task<IEnumerable<Plaza>> GetAllPlazasAsync()
        {
            return await _context.Plazas.Where(p => p.Estado == "Abierta").ToListAsync();
        }

        // Obtener plaza por ID
        public async Task<Plaza> GetPlazaByIdAsync(int id)
        {
            return await _context.Plazas.FindAsync(id);
        }

        // Crear una plaza (Validando reglas de negocio)
        public async Task<bool> CreatePlazaAsync(Plaza plaza)
        {
            if (plaza.FechaCierre <= plaza.FechaPublicacion)
            {
                throw new ArgumentException("La fecha de cierre debe ser posterior a la fecha de publicación.");
            }

            plaza.Estado = "Abierta";
            _context.Plazas.Add(plaza);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdatePlazaAsync(Plaza plaza)
        {
            _context.Plazas.Update(plaza);
            return await _context.SaveChangesAsync() > 0;
        }

        // Actualizar estado de la plaza (Abierta, En evaluación, Cerrada)
        public async Task<bool> ChangePlazaStatusAsync(int idPlaza, string nuevoEstado)
        {
            var plaza = await _context.Plazas.FindAsync(idPlaza);
            if (plaza == null) return false;

            plaza.Estado = nuevoEstado;
            _context.Plazas.Update(plaza);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}