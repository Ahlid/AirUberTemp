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
        public string Nome { get; set; }
        public string Morada { get; set; }
        public string Contact { get; set; } // Mudar para contacto, tava contact

        public int PaisId { get; set; }
        public string Nif { get; set; }
        [Display (Name ="Crédito")]
        public decimal JetCashAtual { get; set; }
        public string Email { get; set; }
        [Display(Name = "Data Registo")]
        public DateTime DataCriacao { get; set; }

        // to be changed? Substituido por Estado
        //public bool Activada { get; set; }
        public int EstadoId { get; set; }

       

        //Testar com isto
        //public virtual ICollection<Reserva> Reservas { get; set; }

        public virtual Pais Pais { get; set; }
        public virtual Estado Estado { get; set; }

        
    }
}
