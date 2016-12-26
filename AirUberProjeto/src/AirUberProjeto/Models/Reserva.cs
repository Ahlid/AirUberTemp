using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AirUberProjeto.Models
{
    public class Reserva
    {
        public int ReservaId { get; set; }
        [Display (Name = "Data Partida")]
        [Required]
        public DateTime DataPartida { get; set; }
        [Display(Name = "Data Chegada")]
        [Required]
        public DateTime DataChegada { get; set; }
        [Display (Name = "Aeroporto Partida")]
        [Required]
        public int AeroportoPartidaId { get; set; }
        [Display (Name = "Aeroporto Destino")]
        [Required]
        public int AeroportoDestinoId { get; set; }
        [Display (Name = "Jato")]
        [Required]
        public int JatoId { get; set; }         // Existe um erro de FK em relação a este ID -> correcção está na classe do Contexto -> mas confirmar se é o pretendido
                                                //aparentemente resolveu o problema
        [Display (Name = "Cliente")]
        [Required]
        public string ApplicationUserId { get; set; }
        [Required]
        public decimal Custo { get; set; }
        [Display(Name = "Avaliação")]
        [Range(0, 5, ErrorMessage = "A avaliação terá que pertencer ao intervalo de 0 a 5 ")]
        public int Avaliacao { get; set; }

        // Atributos Virtuais
        public virtual Jato Jato { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Aeroporto AeroportoPartida { get; set; }
        public virtual Aeroporto AeroportoDestino { get; set; }
        [Display (Name = "Extras")]
        public virtual ICollection<Extra> ListaExtras { get; set; }
        


        public Reserva()
        {
            ListaExtras = new List<Extra>();
        }
    }
}
