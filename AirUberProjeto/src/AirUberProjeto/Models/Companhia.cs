using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    public class Companhia
    {


        public int CompanhiaId { get; set; }
        [Display (Name = "Companhia")]
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Morada { get; set; }
        [Required]
        public string Contacto { get; set; }
        [Display (Name = "País")]
        [Required]
        public int PaisId { get; set; }
        [Display(Name = "NIF")]
        [Required]
        public string Nif { get; set; }
        [Display (Name ="Crédito")]
        public decimal JetCashAtual { get; set; }
        [Required]
        public string Email { get; set; }
        [Display(Name = "Data Registo")]
        public DateTime DataCriacao { get; set; }
        [Display (Name = "Estado")]
        public int EstadoId { get; set; }
        
        // Atributos Virtuais
        [Display(Name = "País")]
        public virtual Pais Pais { get; set; }
        public virtual Estado Estado { get; set; }
        [Display(Name = "Viagens")]
        public virtual ICollection<Reserva> ListaReservas { get; set; }
        [Display(Name = "Número de Colaboradores")]
        public virtual ICollection<Colaborador> ListaColaboradores { get; set; }
        [Display(Name = "Número de Jatos")]
        public virtual ICollection<Jato> ListaJatos { get; set; }
        [Display(Name = "Extras")]
        public virtual ICollection<Extra> ListaExtras { get; set; }

        public Companhia()
        {
            ListaReservas = new List<Reserva>();
            ListaColaboradores = new List<Colaborador>();
            ListaJatos = new List<Jato>();
            ListaExtras = new List<Extra>();
            JetCashAtual = 0.00m;
            DataCriacao = DateTime.Now;
            EstadoId = 2;   // Pendente
        }
    }
}