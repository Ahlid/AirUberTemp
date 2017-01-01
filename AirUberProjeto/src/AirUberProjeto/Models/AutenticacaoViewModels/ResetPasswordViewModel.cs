using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models.AutenticacaoViewModels
{
    /// <summary>
    /// View Model Responsavel para a mudança da password quando o utilizador se esquece da mesma.
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Email do utilizador.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Nova password do utilizador.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Repetição da nova password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Codigo enviado pra que o utilizador possa alterar a password.
        /// </summary>
        public string Code { get; set; }
    }
}
