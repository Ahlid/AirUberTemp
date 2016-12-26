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
        [Required]
        public string Nome { get; set; }
        [Display(Name = "País")]
        [Required]
        public int PaisId { get; set; } 

        // Atributos Virtuais
        public virtual Pais Pais { get; set; }

    }
}
