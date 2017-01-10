using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models.ClienteViewModels
{
    public class PerfilViewModel
    {
        public PerfilViewModel()
        {
            Notificacoes = new List<Notificacao>();
        }

        public ICollection<Notificacao> Notificacoes { get; set; }
        public Cliente Cliente { get; set; }

    }
}
