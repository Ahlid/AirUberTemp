using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models.AutenticacaoViewModels
{
    /// <summary>
    /// View Model para efetuar um login.
    /// </summary>
    public class LoginViewModel
    {

        /// <summary>
        /// Email do utilizador a fazer login
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Password do utilizador a fazer login
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Para saber se o utilizador deseja que a sua conta continue logada
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
