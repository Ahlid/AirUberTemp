using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AirUberProjeto.Migrations
{
    public partial class nomeQualquer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContaDeCreditoses",
                columns: table => new
                {
                    ContaDeCreditosId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JetCashActual = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaDeCreditoses", x => x.ContaDeCreditosId);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    EstadoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.EstadoId);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    PaisId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.PaisId);
                });

            migrationBuilder.CreateTable(
                name: "TipoAcao",
                columns: table => new
                {
                    TipoAcaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAcao", x => x.TipoAcaoId);
                });

            migrationBuilder.CreateTable(
                name: "TipoExtra",
                columns: table => new
                {
                    TipoExtraId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoExtra", x => x.TipoExtraId);
                });

            migrationBuilder.CreateTable(
                name: "TipoJato",
                columns: table => new
                {
                    TipoJatoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoJato", x => x.TipoJatoId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Cidade",
                columns: table => new
                {
                    CidadeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    PaisId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidade", x => x.CidadeId);
                    table.ForeignKey(
                        name: "FK_Cidade_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "PaisId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Companhia",
                columns: table => new
                {
                    CompanhiaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContaDeCreditosId = table.Column<int>(nullable: false),
                    Contact = table.Column<string>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    EstadoId = table.Column<int>(nullable: false),
                    Morada = table.Column<string>(nullable: false),
                    Nif = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    PaisId = table.Column<int>(nullable: false),
                    RelativePathImagemPerfil = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companhia", x => x.CompanhiaId);
                    table.ForeignKey(
                        name: "FK_Companhia_ContaDeCreditoses_ContaDeCreditosId",
                        column: x => x.ContaDeCreditosId,
                        principalTable: "ContaDeCreditoses",
                        principalColumn: "ContaDeCreditosId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companhia_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "EstadoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companhia_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "PaisId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Modelo",
                columns: table => new
                {
                    ModeloId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Alcance = table.Column<decimal>(nullable: false),
                    AltitudeIdeal = table.Column<decimal>(nullable: false),
                    AlturaCabine = table.Column<decimal>(nullable: false),
                    Capacidade = table.Column<int>(nullable: false),
                    ComprimentoCabine = table.Column<decimal>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    LarguraCabine = table.Column<decimal>(nullable: false),
                    NumeroMotores = table.Column<int>(nullable: false),
                    PesoMaximaBagagens = table.Column<decimal>(nullable: false),
                    TipoJatoId = table.Column<int>(nullable: false),
                    VelocidadeMaxima = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelo", x => x.ModeloId);
                    table.ForeignKey(
                        name: "FK_Modelo_TipoJato_TipoJatoId",
                        column: x => x.TipoJatoId,
                        principalTable: "TipoJato",
                        principalColumn: "TipoJatoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aeroporto",
                columns: table => new
                {
                    AeroportoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CidadeId = table.Column<int>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aeroporto", x => x.AeroportoId);
                    table.ForeignKey(
                        name: "FK_Aeroporto_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "CidadeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Apelido = table.Column<string>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    ContaDeCreditosId = table.Column<int>(nullable: true),
                    Contacto = table.Column<string>(nullable: true),
                    RelativePathImagemPerfil = table.Column<string>(nullable: true),
                    CompanhiaId = table.Column<int>(nullable: true),
                    IsAdministrador = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_ContaDeCreditoses_ContaDeCreditosId",
                        column: x => x.ContaDeCreditosId,
                        principalTable: "ContaDeCreditoses",
                        principalColumn: "ContaDeCreditosId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Companhia_CompanhiaId",
                        column: x => x.CompanhiaId,
                        principalTable: "Companhia",
                        principalColumn: "CompanhiaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jato",
                columns: table => new
                {
                    JatoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AeroportoId = table.Column<int>(nullable: false),
                    CompanhiaId = table.Column<int>(nullable: false),
                    CreditosBase = table.Column<double>(nullable: false),
                    CreditosPorKilometro = table.Column<double>(nullable: false),
                    EmFuncionamento = table.Column<bool>(nullable: false),
                    ModeloId = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    RelativePathImagemPerfil = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jato", x => x.JatoId);
                    table.ForeignKey(
                        name: "FK_Jato_Aeroporto_AeroportoId",
                        column: x => x.AeroportoId,
                        principalTable: "Aeroporto",
                        principalColumn: "AeroportoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jato_Companhia_CompanhiaId",
                        column: x => x.CompanhiaId,
                        principalTable: "Companhia",
                        principalColumn: "CompanhiaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jato_Modelo_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelo",
                        principalColumn: "ModeloId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Acao",
                columns: table => new
                {
                    AcaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColaboradorId = table.Column<string>(nullable: true),
                    Detalhes = table.Column<string>(nullable: true),
                    Target = table.Column<string>(nullable: true),
                    TipoAcaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acao", x => x.AcaoId);
                    table.ForeignKey(
                        name: "FK_Acao_AspNetUsers_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Acao_TipoAcao_TipoAcaoId",
                        column: x => x.TipoAcaoId,
                        principalTable: "TipoAcao",
                        principalColumn: "TipoAcaoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notificacao",
                columns: table => new
                {
                    NotificacaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Lida = table.Column<bool>(nullable: false),
                    Mensagem = table.Column<string>(nullable: false),
                    Tipo = table.Column<string>(nullable: false),
                    UtilizadorId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacao", x => x.NotificacaoId);
                    table.ForeignKey(
                        name: "FK_Notificacao_AspNetUsers_UtilizadorId",
                        column: x => x.UtilizadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Disponibilidade",
                columns: table => new
                {
                    DisponibilidadeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fim = table.Column<string>(nullable: true),
                    Inicio = table.Column<string>(nullable: true),
                    JatoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disponibilidade", x => x.DisponibilidadeId);
                    table.ForeignKey(
                        name: "FK_Disponibilidade_Jato_JatoId",
                        column: x => x.JatoId,
                        principalTable: "Jato",
                        principalColumn: "JatoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    ReservaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AeroportoDestinoId = table.Column<int>(nullable: false),
                    AeroportoPartidaId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: false),
                    Avaliacao = table.Column<int>(nullable: false),
                    CompanhiaId = table.Column<int>(nullable: true),
                    Custo = table.Column<decimal>(nullable: false),
                    DataChegada = table.Column<DateTime>(nullable: false),
                    DataPartida = table.Column<DateTime>(nullable: false),
                    JatoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.ReservaId);
                    table.ForeignKey(
                        name: "FK_Reserva_Aeroporto_AeroportoDestinoId",
                        column: x => x.AeroportoDestinoId,
                        principalTable: "Aeroporto",
                        principalColumn: "AeroportoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reserva_Aeroporto_AeroportoPartidaId",
                        column: x => x.AeroportoPartidaId,
                        principalTable: "Aeroporto",
                        principalColumn: "AeroportoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reserva_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reserva_Companhia_CompanhiaId",
                        column: x => x.CompanhiaId,
                        principalTable: "Companhia",
                        principalColumn: "CompanhiaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reserva_Jato_JatoId",
                        column: x => x.JatoId,
                        principalTable: "Jato",
                        principalColumn: "JatoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Extra",
                columns: table => new
                {
                    ExtraId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanhiaId = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    ReservaId = table.Column<int>(nullable: true),
                    TipoExtraId = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extra", x => x.ExtraId);
                    table.ForeignKey(
                        name: "FK_Extra_Companhia_CompanhiaId",
                        column: x => x.CompanhiaId,
                        principalTable: "Companhia",
                        principalColumn: "CompanhiaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Extra_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reserva",
                        principalColumn: "ReservaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Extra_TipoExtra_TipoExtraId",
                        column: x => x.TipoExtraId,
                        principalTable: "TipoExtra",
                        principalColumn: "TipoExtraId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acao_ColaboradorId",
                table: "Acao",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Acao_TipoAcaoId",
                table: "Acao",
                column: "TipoAcaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Aeroporto_CidadeId",
                table: "Aeroporto",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ContaDeCreditosId",
                table: "AspNetUsers",
                column: "ContaDeCreditosId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanhiaId",
                table: "AspNetUsers",
                column: "CompanhiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cidade_PaisId",
                table: "Cidade",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Companhia_ContaDeCreditosId",
                table: "Companhia",
                column: "ContaDeCreditosId");

            migrationBuilder.CreateIndex(
                name: "IX_Companhia_EstadoId",
                table: "Companhia",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Companhia_PaisId",
                table: "Companhia",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Disponibilidade_JatoId",
                table: "Disponibilidade",
                column: "JatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Extra_CompanhiaId",
                table: "Extra",
                column: "CompanhiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Extra_ReservaId",
                table: "Extra",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Extra_TipoExtraId",
                table: "Extra",
                column: "TipoExtraId");

            migrationBuilder.CreateIndex(
                name: "IX_Jato_AeroportoId",
                table: "Jato",
                column: "AeroportoId");

            migrationBuilder.CreateIndex(
                name: "IX_Jato_CompanhiaId",
                table: "Jato",
                column: "CompanhiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Jato_ModeloId",
                table: "Jato",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelo_TipoJatoId",
                table: "Modelo",
                column: "TipoJatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacao_UtilizadorId",
                table: "Notificacao",
                column: "UtilizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_AeroportoDestinoId",
                table: "Reserva",
                column: "AeroportoDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_AeroportoPartidaId",
                table: "Reserva",
                column: "AeroportoPartidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_ApplicationUserId",
                table: "Reserva",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_CompanhiaId",
                table: "Reserva",
                column: "CompanhiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_JatoId",
                table: "Reserva",
                column: "JatoId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acao");

            migrationBuilder.DropTable(
                name: "Disponibilidade");

            migrationBuilder.DropTable(
                name: "Extra");

            migrationBuilder.DropTable(
                name: "Notificacao");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TipoAcao");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "TipoExtra");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Jato");

            migrationBuilder.DropTable(
                name: "Aeroporto");

            migrationBuilder.DropTable(
                name: "Companhia");

            migrationBuilder.DropTable(
                name: "Modelo");

            migrationBuilder.DropTable(
                name: "Cidade");

            migrationBuilder.DropTable(
                name: "ContaDeCreditoses");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "TipoJato");

            migrationBuilder.DropTable(
                name: "Pais");
        }
    }
}
