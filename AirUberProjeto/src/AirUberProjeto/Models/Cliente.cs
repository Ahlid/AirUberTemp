using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    public class Cliente : ApplicationUser
    {

        [Display (Name="Créditos")]
        [DataType (DataType.Currency)]
        public decimal JetCashAtual { get; set; }
        public string Contacto { get; set; } 
        
        // Atributos Virtuais
        [Display (Name = "Viagens")]    
        public virtual ICollection<Reserva> ListaReservas { get; set; }



        public Cliente()
        {
            ListaReservas = new List<Reserva>();
        }
    }
}
