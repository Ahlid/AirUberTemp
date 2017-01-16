using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models.CompanhiaViewModels
{
    public class PerfilCompanhiaViewModel
    {
        public Colaborador Colaborador { get; set; }

        public Companhia Companhia { get; set; }

        public ICollection<Notificacao> Notificacoes { get; set; }
        

        public PerfilCompanhiaViewModel()
        {
            Notificacoes = new List<Notificacao>();
        }
    }
}
