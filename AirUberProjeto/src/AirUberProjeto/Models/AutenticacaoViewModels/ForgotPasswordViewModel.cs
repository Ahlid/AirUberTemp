using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models.AutenticacaoViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
