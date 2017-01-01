using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por toda a informação de uma reserva.
    /// </summary>
    public class Reserva
    {
        /// <summary>
        /// Identificador unívoco de um reserva, sendo chave primária na base de dados.
        /// </summary>
        public int ReservaId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar a data de partida da reserva.
        /// </summary>
        [Display (Name = "Data Partida")]
        [Required]
        public DateTime DataPartida { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar a data de chegada da reserva.
        /// </summary>
        [Display(Name = "Data Chegada")]
        [Required]
        public DateTime DataChegada { get; set; }
        /// <summary>
        /// Identificador unívoco do aeroporto de partida da reserva.
        /// </summary>
        [Display (Name = "Aeroporto Partida")]
        [Required]
        public int AeroportoPartidaId { get; set; }
        /// <summary>
        /// Identificador unívoco do aeroporto de destino da reserva.
        /// </summary>
        [Display (Name = "Aeroporto Destino")]
        [Required]
        public int AeroportoDestinoId { get; set; }
        /// <summary>
        /// Identificador unívoco do jato que pertence à reserva.
        /// </summary>
        [Display (Name = "Jato")]
        [Required]
        public int JatoId { get; set; }
        /// <summary>
        /// Identificador unívoco do cliente que fez a reserva.
        /// </summary>       
        [Display (Name = "Cliente")]
        [Required]
        public string ApplicationUserId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o valor do custo da reserva.
        /// </summary>
        [Required]
        public decimal Custo { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o valor da avaliação.
        /// </summary>
        /// <remarks>
        /// Esta avaliação é feita depois da viagem terminar. 
        /// Quem pode fazer a avaliação é o cliente que fez a reserva.
        /// </remarks>
        [Display(Name = "Avaliação")]
        [Range(0, 5, ErrorMessage = "A avaliação terá que pertencer ao intervalo de 0 a 5 ")]
        public int Avaliacao { get; set; }

        // Propriedades Virtuais
        /// <summary>
        /// Propriedade navegacional responsável por referenciar o jato que pertence à reserva.
        /// </summary>
        public virtual Jato Jato { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referenciar o cliente que fez a reserva.
        /// </summary>
        public virtual Cliente Cliente { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referenciar o aeroporto de partida da reserva.
        /// </summary>
        public virtual Aeroporto AeroportoPartida { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referenciar o aeroporto de destino da reserva.
        /// </summary>
        public virtual Aeroporto AeroportoDestino { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referenciar a lista de extras que foram requisitados para a viagem.
        /// </summary>
        [Display (Name = "Extras")]
        public virtual ICollection<Extra> ListaExtras { get; set; }



        /// <summary>
        /// Constructor da classe Reserva, é responsável por inicializar a lista de extras.
        /// </summary>
        public Reserva()
        {
            ListaExtras = new List<Extra>();
            Avaliacao = -1; //not introduced
        }
    }
}
