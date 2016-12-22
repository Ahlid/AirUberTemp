using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    public class Cidade
    {
        public int CidadeId { get; set; }
        [Display (Name = "Cidade")]
        public string Nome { get; set; }
        public int PaisId { get; set; } 

        public virtual Pais Pais { get; set; }

    }
}
