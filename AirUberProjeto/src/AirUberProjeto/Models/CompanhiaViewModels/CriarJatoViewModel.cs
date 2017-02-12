using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models.CompanhiaViewModels
{
    /// <summary>
    /// View Model Responsavel por criar um jato
    /// </summary>
    public class CriarJatoViewModel
    {

        /// <summary>
        /// Nome do jato
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Identificador único do modelo do jato
        /// </summary>
        public int ModeloId { get; set; }
        /// <summary>
        /// Identificador único da companhia a que pertence o jato
        /// </summary>
        public int CompanhiaId { get; set; }
        /// <summary>
        /// Indica se um jato está disponível para viajar
        /// </summary>
        public bool EmFuncionamento { get; set; }       /// ESTA PROPRIEDADE DEVE SAIR, PORQUE NÃO É PARA A COMPANHIA INDICAR, MAS SIM O HELPDESK
        /// <summary>
        /// Identificador único do aeroporto onde o jato se encontra
        /// </summary>
        public int AeroportoId { get; set; }

        /// <summary>
        /// Preço em crédidos por Kilometro
        /// </summary>
        [Display(Name = "Preço em crédidos por Kilometro")]
        public double CreditosPorKilometro { get; set; }

        /// <summary>
        /// Preço base em créditos
        /// </summary>
        [Display(Name = "Preço base em créditos")]
        public double CreditosBase { get; set; }

        /// <summary>
        /// Distância máxima de deslocação em kilometros
        /// </summary>
        [Display(Name = "Distância máxima")]
        public double DistanciaMaxima { get; set; }

        /// <summary>
        /// Velocidade média deslocação em metros/segundo
        /// </summary>
        [Display(Name = "Velocidade média")]
        public double VelocidadeMedia { get; set; }

        /// <summary>
        /// Distância máxima de deslocação em kilometros
        /// </summary>
        [Display(Name = "Tempo de preparação")]
        public double TempoPreparacao { get; set; }


    }
}
