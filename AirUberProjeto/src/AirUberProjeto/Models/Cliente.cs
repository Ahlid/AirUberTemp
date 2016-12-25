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

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int ClienteId { get; set; }       // -- para desaparecer por causa de updates
        [Display (Name="Créditos")]
        [DataType (DataType.Currency)]
        public decimal JetCashAtual { get; set; }
        public string Contacto { get; set; } // Mudar para contacto, adicionar no MR

        //public List<Reserva> ListaReservas { get; set; }



        [Display (Name = "Viagens")]    
        public virtual ICollection<Reserva> ListaReservas { get; set; }


        public Cliente()
        {
            ListaReservas = new List<Reserva>();
        }
    }
}
