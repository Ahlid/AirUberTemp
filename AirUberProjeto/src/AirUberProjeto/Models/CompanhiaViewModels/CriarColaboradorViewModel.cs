using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirUberProjeto.Models.CompanhiaViewModels
{
    /// <summary>
    /// View Model Responsavel por criar um colaborador
    /// </summary>
    public class CriarColaboradorViewModel
    {
        /// <summary>
        /// Nome do cliente
        /// </summary>
        [Required]
        [Display(Name = "Primeiro Nome")]
        public string PrimeiroNome { get; set; }
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
        /// <summary>
        /// Companhia
        /// </summary>
        [Display(Name = "Companhia")]
        public int CompanhiaId { get; set; }
        /// <summary>
        /// Indica se um colaborador é administrador ou não
        /// </summary>
        [Display(Name = "É administrador?")]
        public bool IsAdministrador { get; set; }

        /// <summary>
        /// Password do cliente
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Repetição da password do cliente
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
