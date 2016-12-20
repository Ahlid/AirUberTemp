using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    public class Colaborador : ApplicationUser
    {

        public int CompanhiaId { get; set; }
        public bool IsAdministrador { get; set; }
        public virtual Companhia Companhia { get; set; }

    }
}
