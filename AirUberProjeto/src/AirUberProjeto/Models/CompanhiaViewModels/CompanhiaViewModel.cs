using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models.CompanhiaViewModels
{
    public class CompanhiaViewModel
    {

        [Required]
        public string Nome { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar a morada da companhia.
        /// </summary>
        [Required]
        public string Morada { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o contacto da companhia.
        /// </summary>
        
        public string Contact { get; set; }

        /// <summary>
        /// Descrição da companhia
        /// </summary>
        /// [Required]
        public string Descricao { get; set; }

       

        /// <summary>
        /// Identificador único do pais a que pertence a companhia
        /// </summary>
        public string PaisId { get; set; }

        /// <summary>
        /// Número de identificação fiscal da companhia
        /// </summary>
        public string Nif { get; set; }

    }
}
