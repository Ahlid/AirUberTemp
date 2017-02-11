using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por toda a informação de uma companhia.
    /// </summary>
    public class Companhia
    {
        /// <summary>
        /// Identificador unívoco de uma companhia, sendo chave primária na base de dados.
        /// </summary>
        public int CompanhiaId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o nome da companhia.
        /// </summary>
        [Display (Name = "Companhia")]
        [Required]
        public string Nome { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar a morada da companhia.
        /// </summary>
        [Required]
        public string Morada { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o contacto da companhia.
        /// </summary>
        [Required]
        public string Contact { get; set; }
        /// <summary>
        /// Identificador unívoco do país a que a companhia pertence.
        /// </summary>
        [Display (Name = "País")]
        [Required]
        public int PaisId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o NIF da companhia.
        /// </summary>
        [Display(Name = "NIF")]
        [Required]
        public string Nif { get; set; }
        /// <summary>
        /// Identificador unívoco da conta de créditos que pertence à companhia.
        /// </summary>
        [Display (Name ="Crédito")]
        public int ContaDeCreditosId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o email da companhia.
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar a data de criação da companhia.
        /// </summary>
        [Display(Name = "Data Registo")]
        public DateTime DataCriacao { get; set; }
        /// <summary>
        /// Identificador unívoco do estado que representa a situação actual da companhia no sistema.
        /// </summary>
        [Display (Name = "Estado")]
        public int EstadoId { get; set; }
        /// <summary>
        /// Caminho de onde a imagem de perfil da companhia está localizada no projecto
        /// </summary>
        public string RelativePathImagemPerfil { get; set; }

        /// <summary>
        /// Caminho de onde a imagem de perfil da companhia está localizada no projecto
        /// </summary>
        public string Descricao { get; set; }

        // Propriedades Virtuais
        /// <summary>
        /// Propriedade navegacional responsável por referenciar o país a que uma companhia pertence.
        /// </summary>
        [Display(Name = "País")]
        public virtual Pais Pais { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referenciar o estado de uma companhia.
        /// </summary>
        public virtual Estado Estado { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referenciar a conta de créditos de uma companhia.
        /// </summary>
        public virtual ContaDeCreditos ContaDeCreditos { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referenciar a lista de reservas de uma companhia.
        /// </summary>
        /// <remarks>
        /// Histórico de viagens
        /// </remarks>
        [Display(Name = "Viagens")]
        public virtual ICollection<Reserva> ListaReservas { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referenciar a lista de colaboradores que pertence a uma companhia.
        /// </summary>
        [Display(Name = "Número de Colaboradores")]
        public virtual ICollection<Colaborador> ListaColaboradores { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referenciar a lista de jatos que pertencem a uma companhia.
        /// </summary>
        [Display(Name = "Número de Jatos")]
        public virtual ICollection<Jato> ListaJatos { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referenciar a lista de extras que pertencem a uma companhia.
        /// </summary>
        [Display(Name = "Extras")]
        public virtual ICollection<Extra> ListaExtras { get; set; }



        /// <summary>
        /// Constructor da classe Companhia, é responsável por inicializar a lista de reservas,
        /// de colaboradores, de jatos e extras. É também responsável por inicializar a data de criação para a 
        /// data do momento da criação da companhia. Por omissão também inicializa o EstadoId a 2, que é o 
        /// estado que indica que a companhia está pendente.
        /// </summary>
        public Companhia()
        {
            ListaReservas = new List<Reserva>();
            ListaColaboradores = new List<Colaborador>();
            ListaJatos = new List<Jato>();
            ListaExtras = new List<Extra>();
            DataCriacao = DateTime.Now;
            EstadoId = 2;   // Pendente
            RelativePathImagemPerfil = Path.Combine("images", "perfil-default.jpg");
        }
    }
}