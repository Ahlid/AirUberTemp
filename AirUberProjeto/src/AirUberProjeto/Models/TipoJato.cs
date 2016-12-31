using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por toda a informação de um tipo de jato.
    /// </summary>
    public class TipoJato
    {
        /// <summary>
        /// Identificador unívoco de um tipo de jato, sendo chave primária na base de dados.
        /// </summary>
        public int TipoJatoId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o nome do tipo de jato.
        /// </summary>
        [Display (Name = "Tipo Jato")]
        [Required]
        public string Nome { get; set; }

    }
}
