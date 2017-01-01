using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models.AutenticacaoViewModels
{
    /// <summary>
    /// View Model responsavel pelo registo de um cliente
    /// </summary>
    public class RegisterClienteViewModel
    {
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
