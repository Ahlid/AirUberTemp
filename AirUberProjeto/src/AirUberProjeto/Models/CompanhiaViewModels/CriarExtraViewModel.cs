using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models.CompanhiaViewModels
{
    /// <summary>
    /// View Model Responsavel por criar um extra
    /// </summary>
    public class CriarExtraViewModel
    {
        /// <summary>
        /// Identificador de um tipo de extra 
        /// </summary>
        public int TipoExtraId { get; set; }
        /// <summary>
        /// Identificador de uma companhia
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
