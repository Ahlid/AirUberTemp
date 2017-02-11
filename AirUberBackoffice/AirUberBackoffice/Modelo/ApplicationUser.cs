using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirUberBackoffice
{
    public class ApplicationUser
    {

        public string ApplicationUserId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar a data de criação de um utilizador no sistema.
        /// </summary>
        public DateTime DataCriacao { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o nome do utilizador
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o apelido do utilizador
        /// </summary>
        public string Apelido { get; set; }
        /// <summary>
        /// Propriedade responsável por indicar se um utilizador está activo ou desactivado.
        /// </summary>
        /// <remarks>
        /// Caso um utilizador esteja desactivado este não conseguirá fazer login no sistema.
        /// </remarks>
        public bool Ativo { get; set; }

        public string Email { get; set; }


        /// <summary>
        /// Constructor da classe ApplicationUser, é responsável por inicializar a data criação para a data do momento
        /// de criação.
        /// </summary>
        public ApplicationUser() : base()
        {
            DataCriacao = DateTime.Now;
        }
    }
}
