using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AirUberProjeto.Data;

namespace AirUberProjeto.Migrations
{
    [DbContext(typeof(AirUberDbContext))]
    [Migration("20170116165857_sadas")]
    partial class sadas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AirUberProjeto.Models.Aeroporto", b =>
                {
                    b.Property<int>("AeroportoId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CidadeId");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("AeroportoId");

                    b.HasIndex("CidadeId");

                    b.ToTable("Aeroporto");
                });

            modelBuilder.Entity("AirUberProjeto.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Apelido");

                    b.Property<bool>("Ativo");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Nome");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ApplicationUser");
                });

            modelBuilder.Entity("AirUberProjeto.Models.Cidade", b =>
                {
                    b.Property<int>("CidadeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<int>("PaisId");

                    b.HasKey("CidadeId");

                    b.HasIndex("PaisId");

                    b.ToTable("Cidade");
                });

            modelBuilder.Entity("AirUberProjeto.Models.Companhia", b =>
                {
                    b.Property<int>("CompanhiaId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContaDeCreditosId");

                    b.Property<string>("Contact")
                        .IsRequired();

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("EstadoId");

                    b.Property<string>("Morada")
                        .IsRequired();

                    b.Property<string>("Nif")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<int>("PaisId");

                    b.HasKey("CompanhiaId");

                    b.HasIndex("ContaDeCreditosId");

                    b.HasIndex("EstadoId");

                    b.HasIndex("PaisId");

                    b.ToTable("Companhia");
                });

            modelBuilder.Entity("AirUberProjeto.Models.ContaDeCreditos", b =>
                {
                    b.Property<int>("ContaDeCreditosId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("JetCashActual");

                    b.HasKey("ContaDeCreditosId");

                    b.ToTable("ContaDeCreditoses");
                });

            modelBuilder.Entity("AirUberProjeto.Models.Estado", b =>
                {
                    b.Property<int>("EstadoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("EstadoId");

                    b.ToTable("Estado");
                });

            modelBuilder.Entity("AirUberProjeto.Models.Extra", b =>
                {
                    b.Property<int>("ExtraId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanhiaId");

                    b.Property<int?>("ReservaId");

                    b.Property<int>("TipoExtraId");

                    b.Property<decimal>("Valor");

                    b.HasKey("ExtraId");

                    b.HasIndex("CompanhiaId");

                    b.HasIndex("ReservaId");

                    b.HasIndex("TipoExtraId");

                    b.ToTable("Extra");
                });

            modelBuilder.Entity("AirUberProjeto.Models.Jato", b =>
                {
                    b.Property<int>("JatoId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanhiaId");

                    b.Property<bool>("EmFuncionamento");

                    b.Property<int>("ModeloId");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("JatoId");

                    b.HasIndex("CompanhiaId");

                    b.HasIndex("ModeloId");

                    b.ToTable("Jato");
                });

            modelBuilder.Entity("AirUberProjeto.Models.Modelo", b =>
                {
                    b.Property<int>("ModeloId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Alcance");

                    b.Property<decimal>("AltitudeIdeal");

                    b.Property<decimal>("AlturaCabine");

                    b.Property<int>("Capacidade");

                    b.Property<decimal>("ComprimentoCabine");

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<decimal>("LarguraCabine");

                    b.Property<int>("NumeroMotores");

                    b.Property<decimal>("PesoMaximaBagagens");

                    b.Property<int>("TipoJatoId");

                    b.Property<decimal>("VelocidadeMaxima");

                    b.HasKey("ModeloId");

                    b.HasIndex("TipoJatoId");

                    b.ToTable("Modelo");
                });

            modelBuilder.Entity("AirUberProjeto.Models.Notificacao", b =>
                {
                    b.Property<int>("NotificacaoId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Lida");

                    b.Property<string>("Mensagem")
                        .IsRequired();

                    b.Property<string>("Tipo")
                        .IsRequired();

                    b.Property<string>("UtilizadorId")
                        .IsRequired();

                    b.HasKey("NotificacaoId");

                    b.HasIndex("UtilizadorId");

                    b.ToTable("Notificacao");
                });

            modelBuilder.Entity("AirUberProjeto.Models.Pais", b =>
                {
                    b.Property<int>("PaisId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("PaisId");

                    b.ToTable("Pais");
                });

            modelBuilder.Entity("AirUberProjeto.Models.Reserva", b =>
                {
                    b.Property<int>("ReservaId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AeroportoDestinoId");

                    b.Property<int>("AeroportoPartidaId");

                    b.Property<string>("ApplicationUserId")
                        .IsRequired();

                    b.Property<int>("Avaliacao");

                    b.Property<int?>("CompanhiaId");

                    b.Property<decimal>("Custo");

                    b.Property<DateTime>("DataChegada");

                    b.Property<DateTime>("DataPartida");

                    b.Property<int>("JatoId");

                    b.HasKey("ReservaId");

                    b.HasIndex("AeroportoDestinoId");

                    b.HasIndex("AeroportoPartidaId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("CompanhiaId");

                    b.HasIndex("JatoId");

                    b.ToTable("Reserva");
                });

            modelBuilder.Entity("AirUberProjeto.Models.TipoExtra", b =>
                {
                    b.Property<int>("TipoExtraId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("TipoExtraId");

                    b.ToTable("TipoExtra");
                });

            modelBuilder.Entity("AirUberProjeto.Models.TipoJato", b =>
                {
                    b.Property<int>("TipoJatoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("TipoJatoId");

                    b.ToTable("TipoJato");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AirUberProjeto.Models.Cliente", b =>
                {
                    b.HasBaseType("AirUberProjeto.Models.ApplicationUser");

                    b.Property<int>("ContaDeCreditosId");

                    b.Property<string>("Contacto");

                    b.Property<string>("RelativePathImagemPerfil");

                    b.HasIndex("ContaDeCreditosId");

                    b.ToTable("Cliente");

                    b.HasDiscriminator().HasValue("Cliente");
                });

            modelBuilder.Entity("AirUberProjeto.Models.Colaborador", b =>
                {
                    b.HasBaseType("AirUberProjeto.Models.ApplicationUser");

                    b.Property<int>("CompanhiaId");

                    b.Property<bool>("IsAdministrador");

                    b.HasIndex("CompanhiaId");

                    b.ToTable("Colaborador");

                    b.HasDiscriminator().HasValue("Colaborador");
                });

            modelBuilder.Entity("AirUberProjeto.Models.Helpdesk", b =>
                {
                    b.HasBaseType("AirUberProjeto.Models.ApplicationUser");


                    b.ToTable("Helpdesk");

                    b.HasDiscriminator().HasValue("Helpdesk");
                });

            modelBuilder.Entity("AirUberProjeto.Models.Aeroporto", b =>
                {
                    b.HasOne("AirUberProjeto.Models.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AirUberProjeto.Models.Cidade", b =>
                {
                    b.HasOne("AirUberProjeto.Models.Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("PaisId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AirUberProjeto.Models.Companhia", b =>
                {
                    b.HasOne("AirUberProjeto.Models.ContaDeCreditos", "ContaDeCreditos")
                        .WithMany()
                        .HasForeignKey("ContaDeCreditosId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AirUberProjeto.Models.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AirUberProjeto.Models.Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("PaisId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AirUberProjeto.Models.Extra", b =>
                {
                    b.HasOne("AirUberProjeto.Models.Companhia", "Companhia")
                        .WithMany("ListaExtras")
                        .HasForeignKey("CompanhiaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AirUberProjeto.Models.Reserva")
                        .WithMany("ListaExtras")
                        .HasForeignKey("ReservaId");

                    b.HasOne("AirUberProjeto.Models.TipoExtra", "TipoExtra")
                        .WithMany()
                        .HasForeignKey("TipoExtraId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AirUberProjeto.Models.Jato", b =>
                {
                    b.HasOne("AirUberProjeto.Models.Companhia", "Companhia")
                        .WithMany("ListaJatos")
                        .HasForeignKey("CompanhiaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AirUberProjeto.Models.Modelo", "Modelo")
                        .WithMany()
                        .HasForeignKey("ModeloId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AirUberProjeto.Models.Modelo", b =>
                {
                    b.HasOne("AirUberProjeto.Models.TipoJato", "TipoJato")
                        .WithMany()
                        .HasForeignKey("TipoJatoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AirUberProjeto.Models.Notificacao", b =>
                {
                    b.HasOne("AirUberProjeto.Models.ApplicationUser", "Utilizador")
                        .WithMany()
                        .HasForeignKey("UtilizadorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AirUberProjeto.Models.Reserva", b =>
                {
                    b.HasOne("AirUberProjeto.Models.Aeroporto", "AeroportoDestino")
                        .WithMany("Chegadas")
                        .HasForeignKey("AeroportoDestinoId");

                    b.HasOne("AirUberProjeto.Models.Aeroporto", "AeroportoPartida")
                        .WithMany("Partidas")
                        .HasForeignKey("AeroportoPartidaId");

                    b.HasOne("AirUberProjeto.Models.Cliente", "Cliente")
                        .WithMany("ListaReservas")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("AirUberProjeto.Models.Companhia")
                        .WithMany("ListaReservas")
                        .HasForeignKey("CompanhiaId");

                    b.HasOne("AirUberProjeto.Models.Jato", "Jato")
                        .WithMany()
                        .HasForeignKey("JatoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AirUberProjeto.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AirUberProjeto.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AirUberProjeto.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AirUberProjeto.Models.Cliente", b =>
                {
                    b.HasOne("AirUberProjeto.Models.ContaDeCreditos", "ContaDeCreditos")
                        .WithMany()
                        .HasForeignKey("ContaDeCreditosId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AirUberProjeto.Models.Colaborador", b =>
                {
                    b.HasOne("AirUberProjeto.Models.Companhia", "Companhia")
                        .WithMany("ListaColaboradores")
                        .HasForeignKey("CompanhiaId");
                });
        }
    }
}
