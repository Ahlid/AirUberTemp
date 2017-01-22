using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models.CompanhiaViewModels
{
    public class EditarColaboradorViewModel
    {
        public string Id { get; set; }
        /// <summary>
        /// Identificador único do colaborador a ser editado
        /// </summary>
        public string ColaboradorId { get; set; }
        /// <summary>
        /// Nome do cliente
        /// </summary>
        [Required]
        [Display(Name = "Primeiro Nome")]
        public string Nome { get; set; }
        /// <summary>
        /// Apelido do cliente
        /// </summary>
        [Required]
        [Display(Name = "Apelido")]
        public string Apelido { get; set; }
        /// <summary>
        /// Email do cliente
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //Companhia
        [Display(Name = "Companhia")]
        public int CompanhiaId { get; set; }

        [Display(Name = "É administrador?")]
        public bool IsAdministrador { get; set; }

        //public bool isAtivo { get; set; }     ???
    }
}
