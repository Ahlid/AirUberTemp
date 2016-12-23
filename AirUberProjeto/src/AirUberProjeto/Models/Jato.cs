using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    public class Jato
    {
        public int JatoId { get; set; }

        public int TipoJatoId { get; set; }
        /*
         * ModeloId
         * CompanhiaId
         * Ativo            -> Permissão dada pelo helpdesk
         * EmFuncionamento  ->  
         * 
         */

        public virtual TipoJato TipoJato { get; set; }
    }
}
