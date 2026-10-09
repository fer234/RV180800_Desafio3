using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Bolsa.Entities
{
    public class HojaDeVida
    {
        [Key]
        public int IdCV { get; set; }

        [Required]
        public string IdUsuario { get; set; }

        public string FormacionAcademica { get; set; }

        public string ExperienciaLaboral { get; set; }

        public string Competencias { get; set; }

        public string ArchivoCV { get; set; }
    }
}
