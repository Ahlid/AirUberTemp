using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirUberProjeto.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AirUberProjeto.Models
{
    public class DbInitializer
    {

        public static void Initialize(AirUberDbContext context)
        {
            //comentado
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            InicializarPaises(context);
            InicializarEstados(context);
            InicializarClientes(context);

            //Comentar daqui para baixo na 1ª vez

            //A 1ª vez que executo o projecto depois de eliminar a bd não posso ter aqui código
            //descomentado em que algum objecto use uma FK
            //Para isso só posso descomentar à 2ª vez
            //E 1º criar os valores para uma tabela e só depois é que volto a correr com código que utiliza os valores já criados
/*
            InicializarCompanhias(context);
            InicializarCidades(context);
            InicializarAeroportos(context);
            InicializarViagens(context);
// */
            context.SaveChanges();
        }

        public static async Task AddRoles(IServiceProvider serviceProvider, AirUberDbContext context)
        {

            context.Database.EnsureCreated();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();


            if (!await roleManager.RoleExistsAsync(Roles.ROLE_CLIENTE))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.ROLE_CLIENTE));
            }

            if (!await roleManager.RoleExistsAsync(Roles.ROLE_COLABORADOR))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.ROLE_COLABORADOR));
            }
            if (!await roleManager.RoleExistsAsync(Roles.ROLE_COLABORADOR_ADMIN))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.ROLE_COLABORADOR_ADMIN));
            }

            if (!await roleManager.RoleExistsAsync(Roles.ROLE_HELPDESK))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.ROLE_HELPDESK));
            }



        }

        private static void InicializarPaises(AirUberDbContext context)
        {
            if (!context.Pais.Any())
            {

                context.Pais.Add(new Pais { Nome = "Portugal" });
                context.Pais.Add(new Pais { Nome = "Espanha" });
                context.Pais.Add(new Pais { Nome = "França" });
                context.Pais.Add(new Pais { Nome = "Estados Unidos da América" });
                context.Pais.Add(new Pais { Nome = "República da Irelanda" });
                context.Pais.Add(new Pais { Nome = "Reino Unido" });
            }
        }

        private static void InicializarEstados(AirUberDbContext context)
        {
            if (!context.Estado.Any())
            {
                context.Add(new Estado() { Nome = "Aceite" });
                context.Add(new Estado() { Nome = "Pendente" });
                context.Add(new Estado() { Nome = "Rejeitada" });
            }
        }

        private static void InicializarClientes(AirUberDbContext context)
        {
            if (!context.Cliente.Any())
            {
                context.Cliente.Add(new Cliente
                {
                    Nome = "Artur",
                    Apelido = "Esteves",
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    Contacto = "+351...",
                    Email = "artur@airuber.com"
                });

                context.Cliente.Add(new Cliente
                {
                    Nome = "João",
                    Apelido = "Rafael",
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    Contacto = "+351...",
                    Email = "joao@airuber.com"
                });
                //var reserva = context.Cliente.Select(p => p.Email == "joao@airuber.com");
                
            }
        }

        private static void InicializarCompanhias(AirUberDbContext context)
        {
            if (!context.Companhia.Any())
            {
                context.Companhia.Add(new Companhia
                {
                    Nome = "Transportes Aéreos Portugueses - TAP",
                    Contact = "+351 707 205 700",
                    PaisId = 1,     // Portugal
                    Nif = "506623602",
                    JetCashAtual = 1000000,
                    DataCriacao = DateTime.Now,
                    //Activada = true,
                    EstadoId = 1,
                    Email = "tap@airuber.com"
                });

                context.Companhia.Add(new Companhia
                {
                    Nome = "Ryanair",
                    Contact = "+353 1 945 12 12",
                    PaisId = 5,     // República da Irelanda
                    Nif = "980489806",
                    JetCashAtual = 2000000,
                    DataCriacao = DateTime.Now,
                    //Activada = false,
                    EstadoId = 2,
                    Email = "ryanair@airuber.com"
                });

                context.Companhia.Add(new Companhia
                {
                    Nome = "EasyJet Airline Company Limited",
                    Contact = "+351 707 500 176",
                    PaisId = 6, // Reino Unido
                    Nif = "980467101",
                    JetCashAtual = 3000000,
                    DataCriacao = DateTime.Now,
                    //Activada = false,
                    EstadoId = 3,
                    Email = "easyJet@airuber.com"
                });
            }
        }

        private static void InicializarCidades(AirUberDbContext context)
        {
            if (!context.Cidade.Any())
            {
                context.Cidade.Add(new Cidade()
                {
                    Nome = "Lisboa",
                    PaisId = 1  // Portugal
                });

                context.Cidade.Add(new Cidade()
                {
                    Nome = "Porto",
                    PaisId = 1  // Portugal
                });

                context.Cidade.Add(new Cidade()
                {
                    Nome = "Madrid",
                    PaisId = 2  // Espanha
                });

                context.Cidade.Add(new Cidade()
                {
                    Nome = "Lyon",
                    PaisId = 3  // França
                });

                context.Cidade.Add(new Cidade()
                {
                    Nome = "Nice",
                    PaisId = 3  // França
                });

                context.Cidade.Add(new Cidade()
                {
                    Nome = "Toulouse",
                    PaisId = 3  // França
                });

                context.Cidade.Add(new Cidade()
                {
                    Nome = "Marselha",
                    PaisId = 3  // França
                });

                context.Cidade.Add(new Cidade()
                {
                    Nome = "Nova York",
                    PaisId = 4  // Estados Unidos da América
                });

                context.Cidade.Add(new Cidade()
                {
                    Nome = "Miami",
                    PaisId = 4  // Estados Unidos da América
                });

                context.Cidade.Add(new Cidade()
                {
                    Nome = "Los Angeles",
                    PaisId = 4  // Estados Unidos da América
                });
            }
        }

        private static void InicializarAeroportos(AirUberDbContext context)
        {
            if (!context.Aeroporto.Any())
            {
                context.Aeroporto.Add(new Aeroporto()
                {
                    CidadeId = 1, // Lisboa
                    Nome = "Aeroporto Humberto Delgado",
                    Latitude = 38.779444,
                    Longitude = -9.136111
                });

                context.Aeroporto.Add(new Aeroporto()
                {
                    CidadeId = 3, // Madrid
                    Nome = "Aeroporto Adolfo Suárez, Madrid-Barajas",
                    Latitude = 40.472222,
                    Longitude = -3.560833
                });
            }
        }

        private static void InicializarViagens(AirUberDbContext context)
        {
            if (!context.Reserva.Any())
            {
                context.Reserva.Add(new Reserva()
                {
                    DataPartida = DateTime.Now,
                    DataChegada = new DateTime(2016, 12, 31),
                    Custo = 3500.5m,
                    //AeroportoPartidaId = 1,
                    //AeroportoChegadaId = 2 
                });
            }
        }
    }
}



//falta avaliação, extras e jatos -> para isto vai ser preciso gerar a view de novo