using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models.CompanhiaViewModels
{
    /// <summary>
    /// View Model Responsavel pelo perfil da companhia
    /// </summary>
    public class PerfilCompanhiaViewModel
    {
        /// <summary>
        /// Objecto que representa o colaborador
        /// </summary>
        public Colaborador Colaborador { get; set; }
        /// <summary>
        /// Objecto que representa a companhia
        /// </summary>
        public Companhia Companhia { get; set; }
        /// <summary>
        /// Lista de notificações da companhia
        /// </summary>
        public ICollection<Notificacao> Notificacoes { get; set; }
        
        /// <summary>
        /// Cria uma instância da classe PerfilCompanhiaViewModel
        /// </summary>
        public PerfilCompanhiaViewModel()
        {
            Notificacoes = new List<Notificacao>();
        }
    }
}
