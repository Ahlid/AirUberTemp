using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por toda a informação de um tipo de extra.
    /// </summary>
    public class TipoExtra
    {
        /// <summary>
        /// Identificador unívoco de um tipo de extra, sendo chave primária na base de dados.
        /// </summary>
        public int TipoExtraId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o nome do tipo de extra.
        /// </summary>
        [Display(Name = "Tipo Extra")]
        [Required]
        public string Nome { get; set; }
    }
}
