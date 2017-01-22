﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por toda a informação de uma acção realizável no sistema
    /// </summary>
    public class Acao
    {
        /// <summary>
        /// Identificador único da acção realizada por um utilizador do sistema
        /// </summary>
        public int AcaoId { get; set; }
        /// <summary>
        /// Tipo de ação
        /// </summary>
        public int TipoAcaoId { get; set; }
        /// <summary>
        /// Objectivo da acção
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// Detalhes sobre a acção
        /// </summary>
        public string Detalhes { get; set; }

        /// <summary>
        /// Propriedade navegacional de um tipo de acão
        /// </summary>
        public virtual TipoAcao TipoAcao { get; set; }

    }
}
