using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirUberBackoffice
{
    public class Jato
    {

        /// <summary>
        /// Identificador unívoco de um jato, sendo chave primária na base de dados.
        /// </summary>
        public int JatoId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o nome de um jato.
        /// </summary>
//[Required]
        public string Nome { get; set; }
        /// <summary>
        /// Identificador unívoco do modelo que o jato é.
        /// </summary>
//[Required]
        public int ModeloId { get; set; }
        /// <summary>
        /// Identificador unívoco da companhia a que o jato pertence.
        /// </summary>
//[Required]
        public int CompanhiaId { get; set; }
        /// <summary>
        /// Propriedade responsável por indicar se uma companhia está em funcionamento ou não.
        /// </summary>
        /// <remarks>
        /// Caso uma companhia não esteja em funcionamento os seus jatos não serão apresentados
        /// nas listagem de voos possíveis para os dados introduzidos pelo cliente.
        /// </remarks>
        public bool EmFuncionamento { get; set; }
        /// <summary>
        /// Identificador único do aeroporto onde o jato se encontra
        /// </summary>
        public int AeroportoId { get; set; }
        /// <summary>
        /// Caminho de onde a imagem do jato está localizada no projecto
        /// </summary>
        public string RelativePathImagemPerfil { get; set; }

        // Propriedades Virtuais
        /// <summary>
        /// Propriedade navegacional responsável por referenciar o modelo que o jato é.
        /// </summary>
        public Modelo Modelo { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referenciar a companhia a que o jato pertence.
        /// </summary>
        public Companhia Companhia { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referencer o aeroporto a que se encontra o jato
        /// </summary>
        public Aeroporto Aeroporto { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por guardar todos os intervalos de tempo que um jato está ou não disponível
        /// </summary>
        public ICollection<Disponibilidade> ListaDisponibilidade { get; set; }



        /// <summary>
        /// Constructor da classe Jato, é responsável por indicar, que por omissão, um jato
        /// não está em funcionamento.
        /// </summary>
        public Jato()
        {
            //valor inicial a false, porque a companhia inicialmente nao esta activa
            EmFuncionamento = false;
            ListaDisponibilidade = new List<Disponibilidade>();
            RelativePathImagemPerfil = System.IO.Path.Combine("images", "aviao-default.svg");
        }
    }
}
