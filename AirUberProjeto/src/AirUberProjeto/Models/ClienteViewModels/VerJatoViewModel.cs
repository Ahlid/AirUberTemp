using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models.ClienteViewModels
{
    public class VerJatoViewModel
    {
        public IEnumerable<Jato> JatodDisponiveis { get; set; }
        public int AeroportoId { get; set; }
        public DateTime DataPartida { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
