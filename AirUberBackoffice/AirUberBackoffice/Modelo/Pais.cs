using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirUberBackoffice
{
    public class Pais
    {

        /// <summary>
        /// Identificador unívoco de um país, sendo chave primária na base de dados.
        /// </summary>
        public int PaisId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o nome de um país.
        /// </summary>
//[Required]
        public string Nome { get; set; }
    }
}
