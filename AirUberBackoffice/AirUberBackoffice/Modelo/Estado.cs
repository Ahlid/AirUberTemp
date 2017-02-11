using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirUberBackoffice
{
    public class Estado
    {

        /// <summary>
        /// Identificador unívoco de um estado, sendo chave primária na base de dados.
        /// </summary>
        public int EstadoId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o nome do estado.
        /// </summary>
//[Required]
        public string Nome { get; set; }
    }
}
