using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models.AutenticacaoViewModels
{
    public class RegisterCompanhiaViewModel

    {


        [Required]
        [Display(Name = "Nome Companhia")]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "Morada Escritório")]
        public string Morada { get; set; }
        [Required]
        [Display(Name = "Contacto")]
        public string Contacto { get; set; }
        [Required]
        [Display(Name = "Pais")]
        public int PaisId { get; set; }
        [Required]
        [Display(Name = "NIF/NIC Empresa")]
        public string Nif { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}