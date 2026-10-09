using Bolsa.DAL;
using Bolsa.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bolsa.BLL.Services
{
    public class HojaDeVidaService
    {
        private readonly ApplicationDbContext _context;

        public HojaDeVidaService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener la hoja de vida por el ID del usuario
        public async Task<HojaDeVida> GetHojaDeVidaByUsuarioAsync(string idUsuario)
        {
            return await _context.HojasDeVida
                .FirstOrDefaultAsync(h => h.IdUsuario == idUsuario);
        }

        // Crear o actualizar la hoja de vida
        public async Task<bool> SaveHojaDeVidaAsync(HojaDeVida hojaDeVida)
        {
            var existing = await _context.HojasDeVida
                .FirstOrDefaultAsync(h => h.IdUsuario == hojaDeVida.IdUsuario);

            if (existing == null)
            {
                _context.HojasDeVida.Add(hojaDeVida);
            }
            else
            {
                existing.FormacionAcademica = hojaDeVida.FormacionAcademica;
                existing.ExperienciaLaboral = hojaDeVida.ExperienciaLaboral;
                existing.Competencias = hojaDeVida.Competencias;
                existing.ArchivoCV = hojaDeVida.ArchivoCV;
                _context.HojasDeVida.Update(existing);
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}