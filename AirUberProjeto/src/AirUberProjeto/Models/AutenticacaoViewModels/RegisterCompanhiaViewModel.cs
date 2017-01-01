using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models.AutenticacaoViewModels
{
    /// <summary>
    /// View Model responsavel pelo registo da companhia.
    /// </summary>
    public class RegisterCompanhiaViewModel

    {

        /// <summary>
        /// Nome da companhia.
        /// </summary>
        [Required]
        [Display(Name = "Nome Companhia")]
        public string Nome { get; set; }
        /// <summary>
        /// Morada da base da companhia.
        /// </summary>
        [Required]
        [Display(Name = "Morada Escritório")]
        public string Morada { get; set; }
        /// <summary>
        /// Contacto da companhia.
        /// </summary>
        [Required]
        [Display(Name = "Contact")]
        public string Contact { get; set; }
        /// <summary>
        /// Id do pais da companhia
        /// </summary>
        [Required]
        [Display(Name = "Pais")]
        public int PaisId { get; set; }
        /// <summary>
        /// NIf da companhia
        /// </summary>
        [Required]
        [Display(Name = "NIF/NIC Empresa")]
        public string Nif { get; set; }

        /// <summary>
        /// Email do colaborador admin.
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Password para o colaborador admin.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Repetição da password para o colaborador admin.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}