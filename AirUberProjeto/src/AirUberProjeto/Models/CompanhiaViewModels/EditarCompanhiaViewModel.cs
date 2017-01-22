using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models.CompanhiaViewModels
{
    /// <summary>
    /// View Model Responsavel por editar o perfil da companhia
    /// </summary>
    public class EditarCompanhiaViewModel
    {
        /// <summary>
        /// Nome da companhia
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Morada da companhia
        /// </summary>
        public string Morada { get; set; }

        /// <summary>
        /// Contacto da companhia
        /// </summary>
        public string Contact { get; set; }

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
