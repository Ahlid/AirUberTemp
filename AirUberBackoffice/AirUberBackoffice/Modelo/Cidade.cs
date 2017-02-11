using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirUberBackoffice
{
    public class Cidade
    {

        /// <summary>
        /// Identificador unívoco de uma cidade, sendo chave primária na base de dados.
        /// </summary>
        public int CidadeId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o nome da cidade.
        /// </summary>
//[Required]
        public string Nome { get; set; }
        /// <summary>
        /// Identificador unívoco do país a que a cidade pertence.
        /// </summary>
//[Required]
        public int PaisId { get; set; }


        public Pais Pais { get; set; }
    }
}
