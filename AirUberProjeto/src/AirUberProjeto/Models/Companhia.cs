using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    public class Companhia
    {

        public int CompanhiaId { get; set; }
        public string Nome { get; set; }
        public string Morada { get; set; }
        public string Contact { get; set; }
        public int PaisId { get; set; }
        public virtual Pais Pais { get; set; }
        public string Nif { get; set; }

    }
}
