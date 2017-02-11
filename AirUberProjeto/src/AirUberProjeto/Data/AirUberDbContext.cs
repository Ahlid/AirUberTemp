using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AirUberProjeto.Models;
using NuGet.Protocol.Core.v3;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AirUberProjeto.Data
{
    /// <summary>
    /// Classe que representa o contexto da aplicação
    /// </summary>
    public class AirUberDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Constructor da classe AirUberDbContext
        /// </summary>
        /// <param name="options"></param>
        public AirUberDbContext(DbContextOptions<AirUberDbContext> options)
            : base(options)
        {
        }
        
        /// <summary>
        /// Create model
        /// </summary>
        /// <param name="builder">builder model</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            base.OnModelCreating(builder);
            
            builder.Entity<ApplicationUser>().HasKey(p => p.Id);
            builder.Entity<Cliente>().ToTable("Cliente");
            builder.Entity<Helpdesk>().ToTable("Helpdesk");
            builder.Entity<Colaborador>().ToTable("Colaborador");

            builder.Entity<Pais>();



            builder.Entity<Reserva>()
                .HasOne(v => v.AeroportoPartida)
                .WithMany(a => a.Partidas)
                .HasForeignKey(v => v.AeroportoPartidaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Reserva>()
               .HasOne(v => v.AeroportoDestino)
               .WithMany(a => a.Chegadas)
               .HasForeignKey(v => v.AeroportoDestinoId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Cliente>()
                .HasMany(p => p.ListaReservas).WithOne(c => c.Cliente)
                .HasForeignKey(c => c.ApplicationUserId);

            builder.Entity<Reserva>()
                .HasOne(c => c.Cliente)
                .WithMany(u => u.ListaReservas)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            //resolve erro de FK 'CompanhiaId' on tabela 'aspnetusers' may cause cycles or multiple cascade paths
            builder.Entity<Colaborador>()
                .HasOne(c => c.Companhia)
                .WithMany(u => u.ListaColaboradores)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            /*

            builder.Entity<Jato>()
                .HasOne(c => c.Companhia)
                .WithMany()
                .WillCascadeOnDelete(false);
                */

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        /// <summary>
        /// Entidade usada para criar, editar e eliminar aeroportos
        /// </summary>
        public DbSet<Aeroporto> Aeroporto { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar application users
        /// </summary>
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar cidades
        /// </summary>
        public DbSet<Cidade> Cidade { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar clientes
        /// </summary>
        public DbSet<Cliente> Cliente { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar colaboradores
        /// </summary>
        public DbSet<Colaborador> Colaborador { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar companhias
        /// </summary>
        public DbSet<Companhia> Companhia { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar estados
        /// </summary>
        public DbSet<Estado> Estado { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar extras
        /// </summary>
        public DbSet<Extra> Extra { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar jatos
        /// </summary>
        public DbSet<Jato> Jato { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar modelos
        /// </summary>
        public DbSet<Modelo> Modelo { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar paises
        /// </summary>
        public DbSet<Pais> Pais { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar reservas
        /// </summary>
        public DbSet<Reserva> Reserva { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar tipos de extra
        /// </summary>
        public DbSet<TipoExtra> TipoExtra { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar tipos de jato
        /// </summary>
        public DbSet<TipoJato> TipoJato { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar notificações
        /// </summary>
        public DbSet<Notificacao> Notificacao { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar conta de créditos
        /// </summary>
        public DbSet<ContaDeCreditos> ContaDeCreditoses { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar informação sobre acções realizáveis no sistema por um utilizador 
        /// </summary>
        public DbSet<Acao> Acao { get; set; }
        /// <summary>
        /// Entidade usada para criar, editar e eliminar informação sobre o tipo de acções que um utilizador por realizar no sistema
        /// </summary>
        public DbSet<TipoAcao> TipoAcao { get; set; }

        /// <summary>
        /// Entidade historico transacoes
        /// </summary>
        public DbSet<HistoricoTransacoeMonetarias> HistoricoTransacoeMonetariases { get; set; }


        /// <summary>
        /// Historico Movimento Monetario
        /// </summary>
        public DbSet<MovimentoMonetario> MovimentoMonetarios { get; set; }
        //public DbSet<Helpdesk> Helpdesk { get; set; }

    }
}
