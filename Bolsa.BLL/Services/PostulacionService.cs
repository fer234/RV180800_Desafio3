using Bolsa.DAL;
using Bolsa.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolsa.BLL.Services
{
    public class PostulacionService
    {
        private readonly ApplicationDbContext _context;

        public PostulacionService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Crear una postulación validando restricciones de forma asíncrona
        public async Task<bool> CreatePostulacionAsync(Postulacion postulacion)
        {
            // Validar si la plaza existe y está abierta
            var plaza = await _context.Plazas.FindAsync(postulacion.IdPlaza);
            if (plaza == null || plaza.Estado != "Abierta")
            {
                throw new InvalidOperationException("La plaza no existe o ya no se encuentra abierta.");
            }

            // Validar que el candidato no se postule dos veces a la misma plaza (Requisito de la rúbrica)
            bool yaPostulado = await _context.Postulaciones
                .AnyAsync(p => p.IdPlaza == postulacion.IdPlaza && p.IdCandidato == postulacion.IdCandidato);

            if (yaPostulado)
            {
                throw new InvalidOperationException("El candidato ya se ha postulado a esta plaza anteriormente.");
            }

            postulacion.FechaPostulacion = DateTime.Now;
            postulacion.Estado = "En revisión";

            _context.Postulaciones.Add(postulacion);
            return await _context.SaveChangesAsync() > 0;
        }

        // Obtener las postulaciones de un candidato de forma asíncrona
        public async Task<IEnumerable<Postulacion>> GetPostulacionesByCandidatoAsync(string idCandidato)
        {
            return await _context.Postulaciones
                .Where(p => p.IdCandidato == idCandidato)
                .ToListAsync();
        }
    }
}
