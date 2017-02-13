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
    /// <summary>
    /// Classe responsável por popular a base de dados numa fase inicial.
    /// </summary>
    public class DbInitializer
    {

        /// <summary>
        /// Método reponsável por inserir em todas as tabelas da base de dados caso não haja já valores na tabela.
        /// </summary>
        /// <param name="context">Contexto da aplicação.</param>
        public static void Initialize(AirUberDbContext context, UserManager<ApplicationUser> userManager)
        {
            context.Database.EnsureCreated();
            //context.SaveChanges();

            inicializarTiposAcoes(context);
            context.SaveChanges();

            inicializarPaises(context);
            context.SaveChanges();

            inicializarEstados(context);
            context.SaveChanges();

            inicializarTipoJatos(context);
            context.SaveChanges();

            inicializarTipoExtras(context);
            context.SaveChanges();

            inicializarClientes(context);   // apagar - apenas testes
                                            // não foram atributos roles
            context.SaveChanges();

            inicializarModelos(context);
            context.SaveChanges();

            inicializarCidades(context);
            context.SaveChanges();

            inicializarAeroportos(context);
            context.SaveChanges();

            inicializarCompanhias(context); // apagar - apenas testes
            context.SaveChanges();

            inicializarJatos(context);
            context.SaveChanges();

            inicializarExtras(context);
            context.SaveChanges();

            inicializarColaboradores(context);  // apagar - apenas testes
            context.SaveChanges();

          //  inicializarViagens(context, userManager);    // apagar - apenas testes
          //  context.SaveChanges();

        }

        /// <summary>
        /// Responsável por adicionar à base de dados todos os papéis que um utilizador pode adquirir.
        /// </summary>
        /// <param name="serviceProvider">serviço</param>
        /// <param name="context">Contexto da aplicação</param>
        /// <returns>retornar um objecto da classe Task</returns>
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


        private static void inicializarTiposAcoes(AirUberDbContext context)
        {

            if (!context.TipoAcao.Any())
            {
                context.TipoAcao.Add(new TipoAcao { Nome = "CREATE" });
                context.TipoAcao.Add(new TipoAcao { Nome = "UPDATE" });
                context.TipoAcao.Add(new TipoAcao { Nome = "INSERT" });
                context.TipoAcao.Add(new TipoAcao { Nome = "DELETE" });
                context.TipoAcao.Add(new TipoAcao { Nome = "...." });
                context.TipoAcao.Add(new TipoAcao { Nome = "++++" });
            }
        }

        /// <summary>
        /// Responsável por inserir na base de dados vários pases caso não exista nenhum país na base de dados.
        /// </summary>
        /// <param name="context">Contexto da aplicação</param>
        private static void inicializarPaises(AirUberDbContext context)
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

        /// <summary>
        /// Responsável por inserir na base de dados vários estados caso não exista nenhum estado na base de dados.
        /// </summary>
        /// <param name="context">Contexto da aplicação</param>
        private static void inicializarEstados(AirUberDbContext context)
        {
            if (!context.Estado.Any())
            {
                context.Add(new Estado() { Nome = "Aceite" });
                context.Add(new Estado() { Nome = "Pendente" });
                context.Add(new Estado() { Nome = "Rejeitada" });
            }
        }

        /// <summary>
        /// Responsável por inserir na base de dados vários tipo de jatos caso não exista nenhum tipo de jatos na base de dados.
        /// </summary>
        /// <param name="context">Contexto da aplicação</param>
        private static void inicializarTipoJatos(AirUberDbContext context)
        {
            if (!context.TipoJato.Any())
            {
                // http://www.jets.com/virtual-catalog/

                context.Add(new TipoJato() { Nome = "Turbos" });
                context.Add(new TipoJato() { Nome = "VLJs" });
                context.Add(new TipoJato() { Nome = "Light" });
                context.Add(new TipoJato() { Nome = "Mid-Size" });
                context.Add(new TipoJato() { Nome = "Super" });
                context.Add(new TipoJato() { Nome = "Heavy" });
            }
        }

        /// <summary>
        /// Responsável por inserir na base de dados vários tipo de extras caso não exista nenhum tipo de extras na base de dados.
        /// </summary>
        /// <param name="context">Contexto da aplicação</param>
        private static void inicializarTipoExtras(AirUberDbContext context)
        {
            if (!context.TipoExtra.Any())
            {
                context.Add(new TipoExtra() { Nome = "Bar" });
                context.Add(new TipoExtra() { Nome = "Refeição" });
                context.Add(new TipoExtra() { Nome = "Uber" });
                context.Add(new TipoExtra() { Nome = "Concierge" });
            }
        }

        /// <summary>
        /// Responsável por inserir na base de dados vários clientes caso não exista nenhum cliente na base de dados
        /// </summary>
        /// <param name="context">Contexto da aplicação</param>
        private static void inicializarClientes(AirUberDbContext context)
        {
            if (!context.Cliente.Any())
            {
                HistoricoTransacoeMonetarias h1 = new HistoricoTransacoeMonetarias();
                ContaDeCreditos conta1 = new ContaDeCreditos()
                {
                    JetCashActual = 10000,
                    HistoricoTransacoeMonetarias = h1
                };


                HistoricoTransacoeMonetarias h2 = new HistoricoTransacoeMonetarias();
                ContaDeCreditos conta2 = new ContaDeCreditos()
                {
                    JetCashActual = 15000,
                    HistoricoTransacoeMonetarias = h2
                };

                context.Cliente.Add(new Cliente
                {
                    Nome = "Artur",
                    Apelido = "Esteves",
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    ContaDeCreditos = conta1,
                    Contacto = "+351...",
                    Email = "artur@airuber.com"
                });
                

                context.Cliente.Add(new Cliente
                {
                    Nome = "João",
                    Apelido = "Rafael",
                    Ativo = true,
                    ContaDeCreditos = conta2,
                    DataCriacao = DateTime.Now,
                    Contacto = "+351...",
                    Email = "joao@airuber.com"
                });
            }
        }

        /// <summary>
        /// Responsável por inserir na base de dados vários modelos caso não exista nenhum modelo na base de dados
        /// </summary>
        /// <param name="context">Contexto da aplicação</param>
        private static void inicializarModelos(AirUberDbContext context)
        {
            if (!context.Modelo.Any())
            {
                context.Add(new Modelo()
                {
                    Capacidade = 8,
                    Alcance = 10000,
                    VelocidadeMaxima = 300.30m,
                    PesoMaximaBagagens = 25,
                    NumeroMotores = 2,
                    AltitudeIdeal = 10000,
                    AlturaCabine = 2.20m,
                    LarguraCabine = 1.80m,
                    ComprimentoCabine = 20.0m,
                    Descricao = "1º Modelo",
                    TipoJatoId = 1
                });

            }
        }

        /// <summary>
        /// Responsável por inserir na base de dados várias cidades caso não exista nenhuma cidade na base de dados
        /// </summary>
        /// <param name="context">Contexto da aplicação</param>
        private static void inicializarCidades(AirUberDbContext context)
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

        /// <summary>
        /// Responsável por inserir na base de dados vários aeroportos caso não exista nenhum aeroporto na base de dados
        /// </summary>
        /// <param name="context">Contexto da aplicação</param>
        private static void inicializarAeroportos(AirUberDbContext context)
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

                context.Aeroporto.Add(new Aeroporto()
                {
                    CidadeId = 4, // Lyon
                    Nome = "Aeroporto de Lyon-Saint-Exupéry",
                    Latitude = 45.725556,
                    Longitude = 5.081111
                });
            }
        }

        /// <summary>
        /// Responsável por inserir na base de dados várias companhias caso não exista nenhuma companhia na base de dados
        /// </summary>
        /// <param name="context">Contexto da aplicação</param>
        private static void inicializarCompanhias(AirUberDbContext context)
        {
            if (!context.Companhia.Any())
            {

                HistoricoTransacoeMonetarias h1 = new HistoricoTransacoeMonetarias();
                ContaDeCreditos conta1 = new ContaDeCreditos()
                {
                    
                    HistoricoTransacoeMonetarias = h1,
                    JetCashActual = 1000000
                };
                HistoricoTransacoeMonetarias h2 = new HistoricoTransacoeMonetarias();
                ContaDeCreditos conta2 = new ContaDeCreditos()
                {
                    HistoricoTransacoeMonetarias =  h2,
                    JetCashActual = 2000000
                };

                ContaDeCreditos conta3 = new ContaDeCreditos()
                {
                    JetCashActual = 3000000
                };
                context.Companhia.Add(new Companhia
                {
                    Nome = "Transportes Aéreos Portugueses - TAP",
                    Contact = "+351 707 205 700",
                    PaisId = 1,     // Portugal
                    Nif = "506623602",
                    ContaDeCreditos = conta1,
                    DataCriacao = DateTime.Now,
                    //Activada = true,
                    EstadoId = 1,
                    Email = "tap@airuber.com",
                    Morada = "Edificio 25, Aeroporto de Lisboa 1750 - 364 Lisboa"
                });

                context.Companhia.Add(new Companhia
                {
                    Nome = "Ryanair",
                    Contact = "+353 1 945 12 12",
                    PaisId = 5,     // República da Irelanda
                    Nif = "980489806",
                    ContaDeCreditos = conta2,
                    DataCriacao = DateTime.Now,
                    //Activada = false,
                    EstadoId = 2,
                    Email = "ryanair@airuber.com",
                    Morada = "8, R. Alexandre Herculano 50, 1250 Lisboa"
                });

                context.Companhia.Add(new Companhia
                {
                    Nome = "EasyJet Airline Company Limited",
                    Contact = "+351 707 500 176",
                    PaisId = 6, // Reino Unido
                    Nif = "980467101",
                    ContaDeCreditos = conta2,
                    DataCriacao = DateTime.Now,
                    //Activada = false,
                    EstadoId = 2,
                    Email = "easyJet@airuber.com",
                    Morada = "Hangar 89, London Luton Airport, Luton, Bedfordshire LU2 9PF"
                });
            }
        }

        /// <summary>
        /// Responsável por inserir na base de dados vários jatos caso não exista nenhum jato na base de dados
        /// </summary>
        /// <param name="context">Contexto da aplicação</param>
        private static void inicializarJatos(AirUberDbContext context)
        {
            if (!context.Jato.Any())
            {
                Jato jato1 = new Jato() { Nome = "King Air", ModeloId = 1, CompanhiaId = 1, AeroportoId = 1, };
                Jato jato2 = new Jato() { Nome = "Pilatus", ModeloId = 1, CompanhiaId = 1, AeroportoId = 2, };
                List<Jato> listaJatos = new List<Jato>()
                {
                    jato1, jato2
                };

                context.Jato.Add(jato1);
                context.Jato.Add(jato2);

                context.SaveChanges();

                foreach (Companhia c in context.Companhia)
                {
                    if (c.CompanhiaId == 1)
                    {
                        c.ListaJatos = listaJatos;
                        context.Update(c);
                    }
                }

            }
        }

        /// <summary>
        /// Responsável por inserir na base de dados vários extras caso não exista nenhum extra na base de dados
        /// </summary>
        /// <param name="context">Contexto da aplicação</param>
        private static void inicializarExtras(AirUberDbContext context)
        {
            if (!context.Extra.Any())
            {
                Extra extra1 = new Models.Extra() { TipoExtraId = 1, CompanhiaId = 1, Nome = "Cervejinha", Valor = 50.00m };
                Extra extra2 = new Models.Extra() { TipoExtraId = 2, CompanhiaId = 1, Nome = "Massagem", Valor = 100.00m };
                Extra extra3 = new Models.Extra() { TipoExtraId = 3, CompanhiaId = 2, Nome = "Massagem", Valor = 150.00m };
                Extra extra4 = new Models.Extra() { TipoExtraId = 4, CompanhiaId = 1, Nome = "Champagne", Valor = 500.00m };
                Extra extra5 = new Models.Extra() { TipoExtraId = 4, CompanhiaId = 2, Nome = "Lapdance", Valor = 700.00m };

                List<Extra> extrasTAP = new List<Extra>() { extra1, extra2, extra4 };
                List<Extra> extrasRyanair = new List<Extra>() { extra3, extra5 };

                context.Extra.Add(extra1);
                context.Extra.Add(extra2);
                context.Extra.Add(extra3);
                context.Extra.Add(extra4);
                context.Extra.Add(extra5);

                context.SaveChanges();

                foreach (Companhia c in context.Companhia)
                {
                    if (c.CompanhiaId == 1)
                    {
                        c.ListaExtras = extrasTAP;
                        context.Update(c);
                    }
                    else if(c.CompanhiaId == 2)
                    {
                        c.ListaExtras = extrasRyanair;
                        context.Update(c);
                    }
                }
            }
        }

        /// <summary>
        /// Responsável por inserir na base de dados vários colaboradores caso não exista nenhum colaborador na base de dados
        /// </summary>
        /// <param name="context">Contexto da aplicação</param>
        private static void inicializarColaboradores(AirUberDbContext context)
        {
            if (!context.Colaborador.Any())
            {
                // Colaboradores Companhia 1 - TAP  -> Total: 4 Colaboradores
                Colaborador admin1TAP = new Colaborador() { Nome = "Admin1TAP", CompanhiaId = 1, IsAdministrador = true };
                Colaborador Colaborador1TAP = new Colaborador() { Nome = "Colaborador1TAP", CompanhiaId = 1 };
                Colaborador Colaborador2TAP = new Colaborador() { Nome = "Colaborador2TAP", CompanhiaId = 1 };
                Colaborador Colaborador3TAP = new Colaborador() { Nome = "Colaborador3TAP", CompanhiaId = 1 };

                List<Colaborador> ColaboradoresTap = new List<Colaborador>();
                ColaboradoresTap.Add(admin1TAP);
                ColaboradoresTap.Add(Colaborador1TAP);
                ColaboradoresTap.Add(Colaborador2TAP);
                ColaboradoresTap.Add(Colaborador3TAP);

                // Colaboradores Companhia 2 - Ryanair  -> Total: 2 Colaboradores
                Colaborador Admin1Ryanair = new Colaborador() { Nome = "Admin1Ryanair", CompanhiaId = 2, IsAdministrador = true };
                Colaborador Colaborador1Ryanair = new Colaborador() { Nome = "Colaborador1Ryanair", CompanhiaId = 2 };

                List<Colaborador> ColaboradoresRyanair = new List<Colaborador>();
                ColaboradoresRyanair.Add(Admin1Ryanair);
                ColaboradoresRyanair.Add(Colaborador1Ryanair);

                // Colaboradores Companhia 3 - EasyJet  -> Total: 9 Colaboradores
                Colaborador Admin1EasyJet = new Colaborador() { Nome = "Admin1EasyJet", CompanhiaId = 3, IsAdministrador = true };
                Colaborador Admin2EasyJet = new Colaborador() { Nome = "Admin2EasyJet", CompanhiaId = 3, IsAdministrador = true };
                Colaborador Colaborador1EasyJet = new Colaborador() { Nome = "Colaborador1EasyJet", CompanhiaId = 3 };
                Colaborador Colaborador2EasyJet = new Colaborador() { Nome = "Colaborador2EasyJet", CompanhiaId = 3 };
                Colaborador Colaborador3EasyJet = new Colaborador() { Nome = "Colaborador3EasyJet", CompanhiaId = 3 };
                Colaborador Colaborador4EasyJet = new Colaborador() { Nome = "Colaborador4EasyJet", CompanhiaId = 3 };
                Colaborador Colaborador5EasyJet = new Colaborador() { Nome = "Colaborador5EasyJet", CompanhiaId = 3 };
                Colaborador Colaborador6EasyJet = new Colaborador() { Nome = "Colaborador6EasyJet", CompanhiaId = 3 };
                Colaborador Colaborador7EasyJet = new Colaborador() { Nome = "Colaborador7EasyJet", CompanhiaId = 3 };

                List<Colaborador> ColaboradoresEasyJet = new List<Colaborador>();
                ColaboradoresEasyJet.Add(Admin1EasyJet);
                ColaboradoresEasyJet.Add(Admin2EasyJet);
                ColaboradoresEasyJet.Add(Colaborador1EasyJet);
                ColaboradoresEasyJet.Add(Colaborador2EasyJet);
                ColaboradoresEasyJet.Add(Colaborador3EasyJet);
                ColaboradoresEasyJet.Add(Colaborador4EasyJet);
                ColaboradoresEasyJet.Add(Colaborador5EasyJet);
                ColaboradoresEasyJet.Add(Colaborador6EasyJet);
                ColaboradoresEasyJet.Add(Colaborador7EasyJet);


                context.SaveChanges();


                foreach (Companhia c in context.Companhia)
                {
                    switch (c.CompanhiaId)
                    {
                        case 1:
                            c.ListaColaboradores = ColaboradoresTap;
                            break;
                        case 2:
                            c.ListaColaboradores = ColaboradoresRyanair;
                            break;
                        case 3:
                            c.ListaColaboradores = ColaboradoresEasyJet;
                            break;
                    }

                    //Se der!!
                    context.Update(c);
                }
            }
        }

        /// <summary>
        /// Responsável por inserir na base de dados várias viagens caso não exista nenhuma viagem na base de dados
        /// </summary>
        /// <param name="context">Contexto da aplicação</param>
        private static void inicializarViagens(AirUberDbContext context, UserManager<ApplicationUser> userManager)
        {

            //aqui associar uma lista de extras ao jato
            if (!context.Reserva.Any())
            {
                ContaDeCreditos conta1 = new ContaDeCreditos()
                {
                    JetCashActual = 4542
                };

                //Novo cliente
                Cliente Miguel = new Cliente() {
                    Nome = "Miguel",
                    Apelido = "Esteves",
                    Ativo = true,
                    ContaDeCreditos = conta1,
                    DataCriacao = DateTime.Now,
                    Contacto = "2222",
                    Email ="miguel@teste.pt",
                    UserName = "miguel@teste.pt"
                };
                
                //var result = userManager.CreateAsync(Miguel, "ost:43636/Acc").Result;
                /*if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(Miguel, Roles.ROLE_CLIENTE);
                }*/
                context.Cliente.Add(Miguel);
                context.SaveChanges();

                 Reserva reserva1 = new Reserva()
                 {
                     DataPartida = DateTime.Now,
                     DataChegada = new DateTime(2016, 12, 31),
                     AeroportoPartidaId = 1,
                     AeroportoDestinoId = 2,
                     JatoId = 1,
                     Cliente = Miguel,
                     Custo = 3500.5m
                 };
                Reserva reserva2 = new Reserva()
                {
                    DataPartida = DateTime.Now,
                    DataChegada = new DateTime(2017, 1, 1),
                    AeroportoPartidaId = 2,
                    AeroportoDestinoId = 3,
                    JatoId = 1,
                    Cliente = Miguel,
                    Custo = 7483.23m,
                    Avaliacao = 5
                };
                context.Reserva.Add(reserva1);
                context.Reserva.Add(reserva2);

                foreach (Companhia c in context.Companhia)
                {
                    if (c.CompanhiaId == 1) //pq o Jato 1 está ligado à companhia 1 -> está hardcoded aqui pq é apenas para testar
                    {
                        c.ListaReservas.Add(reserva1);
                        c.ListaReservas.Add(reserva2);
                        context.Update(c);
                    }
                }

                Miguel.ListaReservas.Add(reserva1);
                context.Update(Miguel);
            }
        }
    }
}