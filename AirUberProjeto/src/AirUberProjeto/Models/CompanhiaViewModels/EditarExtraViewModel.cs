using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models.CompanhiaViewModels
{
    public class EditarExtraViewModel
    {

        public int ExtraId { get; set; }

        public int TipoExtraId { get; set; }

        public int CompanhiaId { get; set; }

        public string Nome { get; set; }

        public decimal Valor { get; set; }
    }
}
