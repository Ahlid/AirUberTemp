using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models.CompanhiaViewModels
{
    /// <summary>
    /// View Model Responsavel por editar um jato
    /// </summary>
    public class EditarJatoViewModel
    {
        /// <summary>
        /// Identificador único do jato
        /// </summary>
        public int JatoId { get; set; }
        /// <summary>
        /// Nome do jato
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Identificador único do modelo do jati
        /// </summary>
        public int ModeloId { get; set; }
        /// <summary>
        /// Identificador único da companhia a que pertence o jato
        /// </summary>
        public int CompanhiaId { get; set; }
        /// <summary>
        /// Indica se um jato está disponível para viajar
        /// </summary>
        public int EmFuncionamento { get; set; }
        /// <summary>
        /// Identificador único do aeroporto onde o jato se encontra
        /// </summary>
        public int AeroportoId { get; set; }
        /// <summary>
        /// Preço base em créditos
        /// </summary>
        [Display(Name = "Preço base em créditos")]
        public double CreditosBase { get; set; }
        /// <summary>
        /// Preço em crédidos por Kilometro
        /// </summary>
        [Display(Name = "Preço em crédidos por Kilometro")]
        public double CreditosPorKilometro { get; set; }

        

    }
}
