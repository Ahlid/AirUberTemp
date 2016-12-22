using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    public class Cliente : ApplicationUser
    {

        public int ClienteId { get; set; }
        [Display (Name="Créditos")]
        [DataType (DataType.Currency)]
        public decimal JetCashAtual { get; set; }
        public string Contacto { get; set; } // Mudar para contacto, adicionar no MR

    }
}
