using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirUberBackoffice
{
    public class Colaborador : ApplicationUser
    {

        /// <summary>
        /// Identificador unívoco da companhia a que pertence o colaborador
        /// </summary>
//[Required]
        public int CompanhiaId { get; set; }
        /// <summary>
        /// Propriedade responsável por indicar se um colaborador é administrador ou não
        /// </summary>
        /// <remarks>
        /// Um utilizador que seja administrador terá certos privilégios que um colaborador não seja administrador não tem responsabilidade
        /// </remarks>
        public bool IsAdministrador { get; set; }

        // Propriedades virtuais
        /// <summary>
        /// Propriedade navegacional responsável por referenciar a companhia a que o colaborador pertence.
        /// </summary>
        public virtual Companhia Companhia { get; set; }
        /// <summary>
        /// Propriedade navegacional responável por guardar todas as informações que um utilziador realizou
        /// </summary>
        /// <remarks>
        /// Histórico de acções
        /// </remarks>
        public ICollection<Acao> ListaAcoes { get; set; }


        /// <summary>
        /// Constructor da classe Colaborador, é responsável por indicar, que por omissão, um 
        /// colaborador não é um administrador.
        /// </summary>
        public Colaborador()
        {
            // por omissão um colaborador não é admin
            IsAdministrador = false;
            ListaAcoes = new List<Acao>();
        }
    }
}
