using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por toda a informação de um tipo de acão.
    /// </summary>
    public class TipoAcao
    {

        /// <summary>
        /// Identificador unívoco de um tipo de extra
        /// </summary>
        public int TipoAcaoId { get; set; }
        /// <summary>
        /// Nome de um tipo de extra
        /// </summary>
        public string Nome { get; set; }
    }
}
