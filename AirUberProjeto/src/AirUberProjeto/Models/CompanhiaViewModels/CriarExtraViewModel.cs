using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models.CompanhiaViewModels
{
    public class CriarExtraViewModel
    {

        public int TipoExtraId { get; set; }

        public int CompanhiaId { get; set; }

        public string Nome { get; set; }
        
        public decimal Valor { get; set; }
    }
}
