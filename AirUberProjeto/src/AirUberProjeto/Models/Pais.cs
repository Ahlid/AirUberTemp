﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    public class Pais
    {
        public int PaisId { get; set; }
        [Display (Name = "País")]
        [Required]
        public string Nome { get; set; }

    }
}
