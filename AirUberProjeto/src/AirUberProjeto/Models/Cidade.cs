using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por toda a informação de uma cidade.
    /// </summary>
    public class Cidade
    {
        /// <summary>
        /// Identificador unívoco de uma cidade, sendo chave primária na base de dados.
        /// </summary>
        public int CidadeId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o nome da cidade.
        /// </summary>
        [Display (Name = "Cidade")]
        [Required]
        public string Nome { get; set; }
        /// <summary>
        /// Identificador unívoco do país a que a cidade pertence.
        /// </summary>
        [Display(Name = "País")]
        [Required]
        public int PaisId { get; set; }

        // Propriedades Virtuais
        /// <summary>
        /// Propriedade navegacional responsável por referenciar o país da cidade.
        /// </summary>
        public virtual Pais Pais { get; set; }

    }
}
