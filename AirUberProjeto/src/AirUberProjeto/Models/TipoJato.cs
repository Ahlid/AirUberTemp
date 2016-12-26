using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models
{
    public class TipoJato
    {
        public int TipoJatoId { get; set; }
        [Display (Name = "Tipo Jato")]
        [Required]
        public string Nome { get; set; }

    }
}
