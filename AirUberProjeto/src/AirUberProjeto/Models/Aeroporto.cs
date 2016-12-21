using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    public class Aeroporto
    {
        public int AeroportoId { get; set; }
        [Display (Name = "Aeroporto")]
        public string Nome { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int CidadeId { get; set; }
        public virtual Cidade Cidade { get; set; }

    }
}
