using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por toda a informação de um extra.
    /// </summary>
    public class Extra
    {
        /// <summary>
        /// Identificador unívoco de um extra, sendo chave primária na base de dados.
        /// </summary>
        public int ExtraId { get; set; }
        /// <summary>
        /// Nome do extra
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Identificador unívoco do tipo de extra que o extra é.
        /// </summary>
        [Display (Name = "Tipo Extra")]
        [Required]
        public int TipoExtraId { get; set; }
        /// <summary>
        /// Identificador unívoco da companhia a que o extra pertence.
        /// </summary>
        [Display (Name = "Companhia")]
        [Required]
        public int CompanhiaId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o valor de um extra.
        /// </summary>
        [Display (Name = "Custo")]
        [Required(ErrorMessage = "Price is required")]
        public decimal Valor { get; set; }

        // Propriedades Virtuais
        /// <summary>
        /// Propriedade navegacional responsável por referenciar o tipo de extra que o extra é.
        /// </summary>
        public virtual TipoExtra TipoExtra { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referenciar a companhia a que o extra pertence.
        /// </summary>
        public virtual Companhia Companhia { get; set; }

        
    }
}
