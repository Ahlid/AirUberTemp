using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models.ClienteViewModels
{
    public class VerOfertaViewModel
    {
        public Jato Jato { get; set; }

        public string Partida { get; set; }

        public Aeroporto AeroportoPartida { get; set; }

        public Aeroporto AeroportoChegada { get; set; }

        public int Estrelas { get; set; }

        public DateTime DataPartida { get; set; }

        public double PrecoBase { get; set; }

        public double Kilometros { get; set; }

    }
}
