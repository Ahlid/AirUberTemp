using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models.CompanhiaViewModels
{
    /// <summary>
    /// View Model Responsavel por editar um extra
    /// </summary>
    public class EditarExtraViewModel
    {
        /// <summary>
        /// Identificador de um extra
        /// </summary>
        public int ExtraId { get; set; }
        /// <summary>
        /// Identificador do tipo de extra
        /// </summary>
        public int TipoExtraId { get; set; }
        /// <summary>
        /// Identificador da companhia a que pertence o extra
        /// </summary>
        public int CompanhiaId { get; set; }
        /// <summary>
        /// Nome extra
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Valor extra
        /// </summary>
        public decimal Valor { get; set; }
    }
}
