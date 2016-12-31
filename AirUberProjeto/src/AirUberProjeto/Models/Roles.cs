namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por indicar todos os papéis disponíveis que um utilizador pode adquirir.
    /// </summary>
    public class Roles
    {
        /// <summary>
        /// Constante que representa o papel de Cliente
        /// </summary>
        public const string ROLE_CLIENTE = "Cliente";
        /// <summary>
        /// Constante que representa o papel de Helpdesk
        /// </summary>
        public const string ROLE_HELPDESK = "Helpesk";
        /// <summary>
        /// Constante que representa o papel de Colaborador
        /// </summary>
        public const string ROLE_COLABORADOR = "Colaborador";
        /// <summary>
        /// Constante que representa o papel de Colaborador Administrador
        /// </summary>
        public const string ROLE_COLABORADOR_ADMIN = "Colaborador_Admin";
    }
}
