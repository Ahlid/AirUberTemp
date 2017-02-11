using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models.ClienteViewModels
{
    public class VerJatoViewModel
    {
        public IEnumerable<Jato> JatodDisponiveis { get; set; }
        public int AeroportoPartidaId { get; set; }
        public int AeroportoDestinoId { get; set; }
        public DateTime DataPartida { get; set; }
    }
}
