using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    public class TipoExtra
    {
        public int TipoExtraId { get; set; }
        [Display(Name = "Tipo Extra")]
        [Required]
        public string Nome { get; set; }
    }
}
