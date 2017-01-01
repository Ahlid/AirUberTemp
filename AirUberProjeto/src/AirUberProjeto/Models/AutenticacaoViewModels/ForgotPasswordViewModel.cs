using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models.AutenticacaoViewModels
{
    /// <summary>
    /// ViewModel para o esquecimento de uma password.
    /// </summary>
    public class ForgotPasswordViewModel
    {

        /// <summary>
        /// O email do utilizador que se esqueceu da password.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
