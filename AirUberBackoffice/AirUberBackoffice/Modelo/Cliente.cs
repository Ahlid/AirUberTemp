using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirUberBackoffice
{
    public class Cliente : ApplicationUser

    {
        /// <summary>
        /// Identificador unívoco da conta de créditos que pertence ao utilizador.
        /// </summary>
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
        /// <remarks>
        /// Histórico de viagens
        /// </remarks>
        public ICollection<Reserva> ListaReservas { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referenciar a conta de créditos de um cliente.
        /// </summary>
        public ContaDeCreditos ContaDeCreditos { get; set; }



        /// <summary>
        /// Constructor da classe Cliente, é responsável por inicializar a lista de reservas de um cliente.
        /// </summary>
        public Cliente()
        {
            ListaReservas = new List<Reserva>();
            RelativePathImagemPerfil = System.IO.Path.Combine("images", "perfil-default.jpg");
        }
    }
}
