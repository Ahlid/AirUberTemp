using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por toda a informação de um Jato.
    /// </summary>
    public class Jato
    {
        /// <summary>
        /// Identificador unívoco de um jato, sendo chave primária na base de dados.
        /// </summary>
        public int JatoId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o nome de um jato.
        /// </summary>
        [Display (Name = "Jato")]
        [Required]  
        public string Nome { get; set; }
        /// <summary>
        /// Identificador unívoco do modelo que o jato é.
        /// </summary>
        [Display (Name = "Modelo")]
        [Required]
        public int ModeloId { get; set; }
        /// <summary>
        /// Identificador unívoco da companhia a que o jato pertence.
        /// </summary>
        [Display(Name = "Companhia")]
        [Required]
        public int CompanhiaId { get; set; }
        /// <summary>
        /// Propriedade responsável por indicar se uma companhia está em funcionamento ou não.
        /// </summary>
        /// <remarks>
        /// Caso uma companhia não esteja em funcionamento os seus jatos não serão apresentados
        /// nas listagem de voos possíveis para os dados introduzidos pelo cliente.
        /// </remarks>
        public bool EmFuncionamento { get; set; }

        // Propriedades Virtuais
        /// <summary>
        /// Propriedade navegacional responsável por referenciar o modelo que o jato é.
        /// </summary>
        public virtual Modelo Modelo { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referenciar a companhia a que o jato pertence.
        /// </summary>
        public virtual Companhia Companhia { get; set; }



        /// <summary>
        /// Constructor da classe Jato, é responsável por indicar, que por omissão, um jato
        /// não está em funcionamento.
        /// </summary>
        public Jato()
        {
            //valor inicial a false, porque a companhia inicialmente nao esta activa
            EmFuncionamento = false;
        }
    }
}
