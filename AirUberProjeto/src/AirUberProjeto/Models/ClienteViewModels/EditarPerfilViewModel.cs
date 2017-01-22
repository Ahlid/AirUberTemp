using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models.ClienteViewModels
{
    /// <summary>
    /// View Model Responsavel por editar o perfil do cliente
    /// </summary>
    public class EditarPerfilViewModel
    {
        /// <summary>
        /// Nome do cliente
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Apelido do cliente
        /// </summary>
        public string Apelido { get; set; }
        /// <summary>
        /// Contacto do cliente
        /// </summary>
        public string Contacto { get; set; }
        
    }
}
