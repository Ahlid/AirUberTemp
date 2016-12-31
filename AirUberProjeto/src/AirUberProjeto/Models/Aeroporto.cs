using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por toda a informação de um aeroporto.
    /// </summary>
    public class Aeroporto
    {
        /// <summary>
        /// Identificador unívoco de um aeroporto, sendo chave primária na base de dados.
        /// </summary>
        public int AeroportoId { get; set; }
        /// <summary>
        /// Identificador unívoco da cidade a que o aeroporto pertence.
        /// </summary>
        [Required (ErrorMessage = "É necessário introduzir a cidade do aeroporto")]
        [Display(Name = "Cidade")]
        public int CidadeId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o nome do aeroporto.
        /// </summary>
        [Required(ErrorMessage = "É necessário introduzir o nome do aeroporto")]
        [Display (Name = "Aeroporto")]
        public string Nome { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar a latitude do aeroporto.
        /// </summary>
        [Required(ErrorMessage = "É necessário introduzir a latitude do aeroporto")]
        public double Latitude { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar a longitude do aeroporto.
        /// </summary>
        [Required(ErrorMessage = "É necessário introduzir a longitude do aeroporto")]
        public double Longitude { get; set; }


        // Propriedades Virtuais

        /// <summary>
        /// Propriedade navegacional responsável por referenciar a cidade do aeroporto.
        /// </summary>
        public virtual Cidade Cidade { get; set; }
        // Necessário para conseguir referenciar a partir da classe Viagens o aeroporto de partida e chegada


        [InverseProperty("AeroportoPartida")]
        public ICollection<Reserva> Partidas { get; set; }

        [InverseProperty("AeroportoDestino")]
        public ICollection<Reserva> Chegadas { get; set; }



        /// <summary>
        /// Constructor da classe Aeroporto, é responsável por inicializar as listas de partidas
        /// e chegadas.
        /// </summary>
        public Aeroporto()
        {
            Partidas = new List<Reserva>();
            Chegadas = new List<Reserva>();
        }
        
    }
}
