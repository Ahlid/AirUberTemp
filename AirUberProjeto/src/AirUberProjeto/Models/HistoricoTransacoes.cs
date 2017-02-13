using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe que representa um historico de movimentos monetarios
    /// </summary>
    public class HistoricoTransacoeMonetarias
    {
        /// <summary>
        /// Id
        /// </summary>
        
        public int HistoricoTransacoeMonetariasId { get; set; }
        /// <summary>
        /// Lista de movimentos monetarios
        /// </summary>
        public virtual ICollection<MovimentoMonetario> MovimentosMonetarios { get; set; }

        /// <summary>
        /// Metodo construtor
        /// </summary>
        public HistoricoTransacoeMonetarias()
        {
            this.MovimentosMonetarios = new List<MovimentoMonetario>();
        }

    }
}
