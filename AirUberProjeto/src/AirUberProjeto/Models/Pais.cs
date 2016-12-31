using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por toda a informação de um país.
    /// </summary>
    public class Pais
    {
        /// <summary>
        /// Identificador unívoco de um país, sendo chave primária na base de dados.
        /// </summary>
        public int PaisId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o nome de um país.
        /// </summary>
        [Display (Name = "País")]
        [Required]
        public string Nome { get; set; }

    }
}
