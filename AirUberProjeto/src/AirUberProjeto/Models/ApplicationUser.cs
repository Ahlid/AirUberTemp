using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AirUberProjeto.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DataCriacao { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
    }
}
