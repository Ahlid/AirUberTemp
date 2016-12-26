using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    public class Colaborador : ApplicationUser
    {

        [Display(Name = "Companhia")]
        [Required]
        public int CompanhiaId { get; set; }    
        [Display (Name = "Administrador")]
        public bool IsAdministrador { get; set; }

        // Atributos virtuais
        public virtual Companhia Companhia { get; set; }



        public Colaborador()
        {
            // por omissão um colaborador não é admin
            IsAdministrador = false; 
        }
    }
}
