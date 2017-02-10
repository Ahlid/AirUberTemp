using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

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
        [Display (Name = "Nome do Jato")]
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
        /// <summary>
        /// Identificador único do aeroporto onde o jato se encontra
        /// </summary>
        [Display (Name = "Aeroporto")]
        public int AeroportoId { get; set; }
        /// <summary>
        /// Caminho de onde a imagem do jato está localizada no projecto
        /// </summary>
        public string RelativePathImagemPerfil { get; set; }

        /// <summary>
        /// Preço em crédidos por Kilometro
        /// </summary>
        [Display(Name = "Preço em crédidos por Kilometro")]
        public double CreditosPorKilometro { get; set; }

        /// <summary>
        /// Preço base em créditos
        /// </summary>
        [Display(Name = "Preço base em créditos")]
        public double CreditosBase { get; set; }



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
        /// Propriedade navegacional responsável por referencer o aeroporto a que se encontra o jato
        /// </summary>
        public virtual Aeroporto Aeroporto { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por guardar todos os intervalos de tempo que um jato está ou não disponível
        /// </summary>
        public virtual ICollection<Disponibilidade> ListaDisponibilidade { get; set; }



        /// <summary>
        /// Constructor da classe Jato, é responsável por indicar, que por omissão, um jato
        /// não está em funcionamento.
        /// </summary>
        public Jato()
        {
            //valor inicial a false, porque a companhia inicialmente nao esta activa
            EmFuncionamento = false;
            ListaDisponibilidade = new List<Disponibilidade>();
            RelativePathImagemPerfil = Path.Combine("images", "aviao-default.svg");
        }
    }
}
