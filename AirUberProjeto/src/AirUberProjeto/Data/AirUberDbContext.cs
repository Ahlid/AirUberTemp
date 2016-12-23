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


            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Pais> Pais { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; } // nao cria esta tabela pq? -> pq fica tudo na tabela aspnetUsers
        public DbSet<Companhia> Companhia { get; set; }

        //added
        public DbSet<Cliente> Cliente { get; set; } // nao cria esta tabela pq?
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Aeroporto> Aeroporto { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<Estado> Estado { get; set; }

    }



}
