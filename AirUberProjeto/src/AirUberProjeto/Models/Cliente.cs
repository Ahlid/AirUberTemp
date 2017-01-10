using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por representar a informação específica de um cliente.
    /// Para a classe adquirir toda a informação de um utilizador esta classe extende 
    /// da classe 'ApplicationUser'.
    /// </summary>
    public class Cliente : ApplicationUser
    {



        /// <summary>
        /// Identificador unívoco da conta de créditos que pertence ao utilizador.
        /// </summary>
        [Display (Name="Créditos")]
        [DataType (DataType.Currency)]
        public int ContaDeCreditosId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o contacto do cliente
        /// </summary>
        public string Contacto { get; set; }


        public string RelativePathImagemPerfil { get; set; }


        // Propriedades Virtuais
        /// <summary>
        /// Propriedade navegacional responsável por referenciar a lista de reservas de um cliente.
        /// </summary>
        [Display (Name = "Viagens")]    
        public virtual ICollection<Reserva> ListaReservas { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referenciar a conta de créditos de um cliente.
        /// </summary>
        public virtual ContaDeCreditos ContaDeCreditos { get; set; }



        /// <summary>
        /// Constructor da classe Cliente, é responsável por inicializar a lista de reservas de um cliente.
        /// </summary>
        public Cliente()
        {
            ListaReservas = new List<Reserva>();
            RelativePathImagemPerfil = Path.Combine("images", "perfil-default.jpg");
        }
    }
}
