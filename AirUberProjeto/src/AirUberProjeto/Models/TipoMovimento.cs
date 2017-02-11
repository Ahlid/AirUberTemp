using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Enumerado para o tipo de movimento
    /// </summary>
    public enum TipoMovimento
    {
        /// <summary>
        /// Se forr um carregamento
        /// </summary>
        Carregamento,
        /// <summary>
        /// Se for uma transferencia
        /// </summary>
        Transferencia,
        /// <summary>
        /// Se for o pagamento de uma reserva
        /// </summary>
        PagamentoServico,
        /// <summary>
        /// Se for reembolso
        /// </summary>
        Reembolso
    }
}
