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
        public DateTime DataPartida { get; set; }
        [Display(Name = "Data Chegada")]
        public DateTime DataChegada { get; set; }


        //falta avaliação, extras e jatos -> para isto vai ser preciso gerar a view de novo
        // falta o id do Utilizador
        //guardar id do user que criou a reserva
        //public string AspNetUsersId { get; set; }
       // public string ApplicationUserId { get; set; }

        public int CompanhiaId { get; set; }
        public virtual Companhia Companhia { get; set; }

        public string ApplicationUserId { get; set; }
        //public string Cliente { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Cliente Cliente { get; set; }

        public decimal Custo { get; set; }

        [Display (Name = "Avaliação")]
        [Range (0, 5, ErrorMessage = "A avaliação terá que pertencer ao intervalo de 0 a 5 ")]
        public int Avaliacao { get; set; }


        //getting errrors
        //public virtual AspNetUsers ApplicationUser { get; set; }
        ///public virtual IdentityUser ApplicationUser { get; set; }

        ////////       [Display(Name = "Partida")]
        //[Key, Column(Order = 1), ForeignKey("AeroportoPartida")]
        //[ForeignKey ("Aeroporto")]
        public int AeroportoPartidaId { get; set; }

        ////////       [Display(Name = "Chegada")]
        //[Key, Column(Order = 2), ForeignKey("AeroportoChegada")]    //Before: Column(Order = 1)
        public int AeroportoDestinoId { get; set; }





        //[InverseProperty("AeroportoPartida")]
        /*     [ForeignKey("AeroportoPartidaId")]
             [InverseProperty("AeroportoPartida")]*/
        public virtual Aeroporto AeroportoPartida { get; set; }
        //[InverseProperty("AeroportoChegada")]
        /*[ForeignKey("AeroportoChegadaId")]
        [InverseProperty("AeroportoChegada")]*/
        public virtual Aeroporto AeroportoDestino { get; set; }


    }
}
