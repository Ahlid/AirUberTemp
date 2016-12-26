using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    public class Estado
    {
        public int EstadoId { get; set; }
        [Display (Name = "Estado")]
        [Required]
        public string Nome { get; set; }
    }
}
