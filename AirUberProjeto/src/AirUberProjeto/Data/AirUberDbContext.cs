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
    public class AirUberDbContext : IdentityDbContext<ApplicationUser>
    {
        public AirUberDbContext(DbContextOptions<AirUberDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<ApplicationUser>().HasKey(p => p.Id);
            builder.Entity<Cliente>().ToTable("Cliente");
            builder.Entity<Helpdesk>().ToTable("Helpdesk");
            builder.Entity<Colaborador>().ToTable("Colaborador");



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

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Aeroporto> Aeroporto { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Colaborador> Colaborador { get; set; }
        public DbSet<Companhia> Companhia { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Extra> Extra { get; set; }
        public DbSet<Jato> Jato { get; set; }
        public DbSet<Modelo> Modelo { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<TipoExtra> TipoExtra { get; set; }
        public DbSet<TipoJato> TipoJato { get; set; }

        //public DbSet<Helpdesk> Helpdesk { get; set; }
        
    }
}
