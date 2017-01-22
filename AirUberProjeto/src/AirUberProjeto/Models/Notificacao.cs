using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por toda a informação de uma notificação.
    /// </summary>
    public class Notificacao
    {
        /// <summary>
        /// 
        /// </summary>
        public const string TYPE_PRIMARY = "primary";
        /// <summary>
        /// 
        /// </summary>
        public const string TYPE_SUCCESS = "success";
        /// <summary>
        /// 
        /// </summary>
        public const string TYPE_INFO = "info";
        /// <summary>
        /// 
        /// </summary>
        public const string TYPE_WARNING = "warning";
        /// <summary>
        /// 
        /// </summary>
        public const string TYPE_DANGER = "danger";

        /// <summary>
        /// Id da notificação
        /// </summary>
        [Required]
        [Display(Name = "ID da Notificação")]
        public int NotificacaoId { get; set; }
        /// <summary>
        /// Id do utilizador a quem a notificação pertence
        /// </summary>
        [Required]
        [ForeignKey("Utilizador")]
        public string UtilizadorId { get; set; }
        /// <summary>
        /// Mensagem da notificação
        /// </summary>
        [Required]
        public string Mensagem { get; set; }
        /// <summary>
        /// Tipo de notificação
        /// </summary>
        [Required]
        public string Tipo { get; set; }
        /// <summary>
        /// Indica se uma notificação já foi lida
        /// </summary>
        [Required]
        public bool Lida { get; set; }

        /// <summary>
        /// Propriedade navegacional responsável por referenciar o utilizador a quem a notificação pertence
        /// </summary>
        public virtual ApplicationUser Utilizador { get; set; }
    }
}
