using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AirUberProjeto.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display (Name = "Data Registo")]
        public DateTime DataCriacao { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public bool Ativo { get; set; }



        public ApplicationUser() : base()
        {
            DataCriacao = DateTime.Now;
        }
    }
}
