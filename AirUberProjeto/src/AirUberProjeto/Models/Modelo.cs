using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    public class Modelo
    {
        public int ModeloId { get; set; }
        [Display (Name = "Número máximo pessoas")]
        [Required]
        public int Capacidade { get; set; }
        [Required]
        public decimal Alcance { get; set; }    // em Km
        [Display(Name = "Velocidade Máxima")]
        [Required]
        public decimal VelocidadeMaxima { get; set; }   // Km/h
        [Display(Name = "Peso Máxima Bagagens")]
        public decimal PesoMaximaBagagens { get; set; } // Kg
        [Display(Name = "Número motores")]
        public int NumeroMotores { get; set; }
        [Display(Name = "Altura ideal")]
        public decimal AltitudeIdeal { get; set; }      // Metros
        [Display(Name = "Altura cabine")]
        [Required]
        public decimal AlturaCabine { get; set; }       // Metros
        [Display(Name = "Largura cabine")]
        [Required]
        public decimal LarguraCabine { get; set; }      // Metros
        [Display(Name = "Comprimento cabine")]
        [Required]
        public decimal ComprimentoCabine { get; set; }      // Metros
        [Display(Name = "Descrição")]
        [Required]
        public string Descricao { get; set; }
        [Display(Name = "Tipo de jato")]
        [Required]
        public int TipoJatoId { get; set; }

        // Atributos Virtuais
        public virtual TipoJato TipoJato { get; set; }
    }
}
