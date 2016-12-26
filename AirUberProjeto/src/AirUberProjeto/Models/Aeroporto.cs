using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    public class Aeroporto
    {

        public int AeroportoId { get; set; }
        [Required (ErrorMessage = "É necessário introduzir a cidade do aeroporto")]
        [Display(Name = "Cidade")]
        public int CidadeId { get; set; }
        [Required(ErrorMessage = "É necessário introduzir o nome do aeroporto")]
        [Display (Name = "Aeroporto")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "É necessário introduzir a latitude do aeroporto")]
        public double Latitude { get; set; }
        [Required(ErrorMessage = "É necessário introduzir a longitude do aeroporto")]
        public double Longitude { get; set; }
        
        // Atributos Virtuais
        public virtual Cidade Cidade { get; set; }
        

        // Necessário para conseguir referenciar a partir da classe Viagens o aeroporto de partida e chegada
        [InverseProperty("AeroportoPartida")]
        public ICollection<Reserva> Partidas { get; set; }

        [InverseProperty("AeroportoDestino")]
        public ICollection<Reserva> Chegadas { get; set; }

        public Aeroporto()
        {
            Partidas = new List<Reserva>();
            Chegadas = new List<Reserva>();
        }
        
    }
}
