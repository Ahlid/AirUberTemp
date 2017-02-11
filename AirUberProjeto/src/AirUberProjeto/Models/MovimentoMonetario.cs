using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe que representao um movimento monetario
    /// </summary>
    public class MovimentoMonetario
    {
        /// <summary>
        /// Id
        /// </summary>
        public int MovimentoMonetarioId { get; set; }
        /// <summary>
        /// Tipo do movimento feito
        /// </summary>
        public TipoMovimento TipoMovimento { get; set; }
        /// <summary>
        /// Montante do movimento
        /// </summary>
        public  double Montante { get; set; }


        //historico -> adicionar numa fase posterior
        /// <summary>
        /// Historico de transações desta conta de creditos
        /// </summary>

        public int HistoricoTransacoeMonetariasId { get; set; }

        /// <summary>
        /// classe virtual do historico de transacoes
        /// </summary>
        public virtual HistoricoTransacoeMonetarias HistoricoTransacoeMonetarias { get; set; }

    }
}
