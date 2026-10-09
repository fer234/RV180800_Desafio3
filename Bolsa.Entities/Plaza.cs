using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bolsa.Entities
{
    public class Plaza
    {
        [Key]
        public int IdPlaza { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public DateTime FechaPublicacion { get; set; } = DateTime.Now;
        public DateTime FechaCierre { get; set; }
        [Required]
        public string Institucion { get; set; }
        [Required]
        public string Estado { get; set; } = "Abierta";
        [Required]
        public string IdAgente { get; set; }
    }
}
