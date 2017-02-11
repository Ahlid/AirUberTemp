using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirUberBackoffice
{
    public class ContaDeCreditos
    {


        /// <summary>
        /// Identificador unívoco de uma conta de créditos, sendo chave primária na base de dados.
        /// </summary>
        public int ContaDeCreditosId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o número de créditos existentes numa conta.
        /// </summary>
        public decimal JetCashActual { get; set; }

        /// <summary>
        /// Constructor da classe ContaDeCreditos, é responsável por inicializar o número de jetcash a 0.
        /// </summary>
        public ContaDeCreditos()
        {
            JetCashActual = 0.00m;
        }
    }
}
