using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    public class Extra
    {
        public int ExtraId { get; set; }
        [Display (Name = "Tipo Extra")]
        [Required]
        public int TipoExtraId { get; set; }
        [Display (Name = "Companhia")]
        [Required]
        public int CompanhiaId { get; set; }
        [Display (Name = "Custo")]
        public decimal Valor { get; set; }

        // Atributos Virtuais
        public virtual TipoExtra TipoExtra { get; set; }
        public virtual Companhia Companhia { get; set; }

        
    }
}
