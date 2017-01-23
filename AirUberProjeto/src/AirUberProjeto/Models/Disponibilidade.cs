using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    
    public class Disponibilidade
    {
        /// <summary>
        /// Identificador único de uma disponibilidade
        /// </summary>
        public int DisponibilidadeId { get; set; }
        /// <summary>
        /// Data de início
        /// </summary>
        /// <remarks>
        /// Disponível a partir desta data
        /// </remarks>
        public string Inicio { get; set; }
        /// <summary>
        /// Data de fim
        /// </summary>
        /// <remarks>
        /// Indisponível a partir desta data
        /// </remarks>
        public string Fim { get; set; }
        
    }
}
