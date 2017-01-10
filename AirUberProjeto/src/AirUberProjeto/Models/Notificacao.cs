using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AirUberProjeto.Models
{
    public class Notificacao
    {
        public const string TYPE_PRIMARY = "primary";
        public const string TYPE_SUCCESS = "success";
        public const string TYPE_INFO = "info";
        public const string TYPE_WARNING = "warning";
        public const string TYPE_DANGER = "danger";

        [Required]
        [Display(Name = "ID da Notificação")]
        public int NotificacaoId { get; set; }
        [Required]
        [ForeignKey("Utilizador")]
        public string UtilizadorId { get; set; }
        [Required]
        public string Mensagem { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public bool Lida { get; set; }

   
        public virtual ApplicationUser Utilizador { get; set; }
    }
}
