using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Bolsa.DAL
{
    public class ApplicationUser : IdentityUser
    {
        public string NombreCompleto { get; set; }
    }
}
