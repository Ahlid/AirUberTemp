using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    public class Reserva
    {
        public int ReservaId { get; set; }
        public DateTime DataPartida { get; set; }
        public DateTime DataChegada { get; set; }

        public decimal Custo { get; set; }

        [Key, Column(Order = 1), ForeignKey("AeroportoPartida")]
        public int AeroportoPartidaId { get; set; }

        [Key, Column(Order = 1), ForeignKey("AeroportoChegada")]    //Before: Column(Order = 1)
        public int AeroportoChegadaId { get; set; }


        [InverseProperty("AeroportoPartida")]
        public virtual Aeroporto AeroportoPartida { get; set; }

        [InverseProperty("AeroportoChegada")]
        public virtual Aeroporto AeroportoChegada { get; set; }

        
    }
}
