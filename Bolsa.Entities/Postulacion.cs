using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bolsa.Entities
{
    public class Postulacion
    {
        [Key]
        public int IdPostulacion { get; set; }
        [Required]
        public int IdPlaza { get; set; }
        [Required]
        public string IdCandidato { get; set; }
        public DateTime FechaPostulacion { get; set; } = DateTime.Now;
        [Required]
        public string Estado { get; set; } = "En revisión";
    }
}
