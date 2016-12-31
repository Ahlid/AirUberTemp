using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por toda a informação de um Modelo.
    /// </summary>
    public class Modelo
    {
        /// <summary>
        /// Identificador unívoco de um modelo, sendo chave primária na base de dados.
        /// </summary>
        public int ModeloId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o número máximo de pessoas que um modelo de jato suporta.
        /// </summary>
        [Display (Name = "Número máximo pessoas")]
        [Required]
        public int Capacidade { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar valor do alcance máximo, em Km, de um modelo.
        /// </summary>
        [Required]
        public decimal Alcance { get; set; }    // em Km
        /// <summary>
        /// Propriedade responsável por guardar o valor da velocidade máxima, em Km/H, de um modelo.
        /// </summary>
        [Display(Name = "Velocidade Máxima")]
        [Required]
        public decimal VelocidadeMaxima { get; set; }   // Km/h
        /// <summary>
        /// Propriedade responsável por guardar o valor do peso máximo em bagagens, em Kg, de um modelo.
        /// </summary>
        [Display(Name = "Peso Máxima Bagagens")]
        public decimal PesoMaximaBagagens { get; set; } // Kg
        /// <summary>
        /// Propriedade responsável por guardar o número de motores de um modelo.
        /// </summary>
        [Display(Name = "Número motores")]
        public int NumeroMotores { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o valor da altura ideal, em Metros, de um modelo.
        /// </summary>
        [Display(Name = "Altura ideal")]
        public decimal AltitudeIdeal { get; set; }      // Metros
        /// <summary>
        /// Propriedade responsável por guardar o valor da altura da cabine, em Metros, de um modelo.
        /// </summary>
        [Display(Name = "Altura cabine")]
        [Required]
        public decimal AlturaCabine { get; set; }       // Metros
        /// <summary>
        /// Propriedade responsável por guardar o valor da largura da cabine, em Metros, de um modelo.
        /// </summary>
        [Display(Name = "Largura cabine")]
        [Required]
        public decimal LarguraCabine { get; set; }      // Metros
        /// <summary>
        /// Propriedade responsável por guardar o valor do comprimento da cabine, em Metros, de um modelo.
        /// </summary>
        [Display(Name = "Comprimento cabine")]
        [Required]
        public decimal ComprimentoCabine { get; set; }      // Metros
        /// <summary>
        /// Propriedade responsável por guardar a descrição de um modelo.
        /// </summary>
        [Display(Name = "Descrição")]
        [Required]
        public string Descricao { get; set; }
        /// <summary>
        /// Identificador unívoco do tipo de jato que o modelo é.
        /// </summary>
        [Display(Name = "Tipo de jato")]
        [Required]
        public int TipoJatoId { get; set; }

        // Propriedades Virtuais
        /// <summary>
        /// Propriedade navegacional responsável por referenciar o tipo de jato que o modelo é.
        /// </summary>
        public virtual TipoJato TipoJato { get; set; }
    }
}
