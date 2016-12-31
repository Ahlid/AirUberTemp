using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por toda a informação de um estado
    /// </summary>
    public class Estado
    {
        /// <summary>
        /// Identificador unívoco de um estado, sendo chave primária na base de dados.
        /// </summary>
        public int EstadoId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o nome do estado.
        /// </summary>
        [Display (Name = "Estado")]
        [Required]
        public string Nome { get; set; }
    }
}
