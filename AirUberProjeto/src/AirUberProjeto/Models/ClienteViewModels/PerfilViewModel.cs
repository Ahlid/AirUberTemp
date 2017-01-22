using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models.ClienteViewModels
{
    /// <summary>
    /// View Model Responsavel pelo perfil do cliente
    /// </summary>
    public class PerfilViewModel
    {

        /// <summary>
        /// Lista de notificações do cliente
        /// </summary>
        public ICollection<Notificacao> Notificacoes { get; set; }
        /// <summary>
        /// Objecto que representa o cliente
        /// </summary>
        public Cliente Cliente { get; set; }
        /// <summary>
        /// Número de viagens de um cliente
        /// </summary>
        public int NumeroViagens;

        /// <summary>
        /// Criar uma instância da classe PerfilViewModel.
        /// </summary>
        public PerfilViewModel()
        {
            Notificacoes = new List<Notificacao>();
        }
    }
}
