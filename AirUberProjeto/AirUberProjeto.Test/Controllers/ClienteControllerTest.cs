using System;
using System.Collections.Generic;
using System.Linq;
using AirUberProjeto.Controllers;
using AirUberProjeto.Data;
using AirUberProjeto.Models;
using AirUberProjeto.Models.ClienteViewModels;
using AirUberProjeto.Test.Mocks;
using MyTested.AspNetCore.Mvc;
using Xunit;


namespace AirUberProjeto.Test.Controllers
{
    public class ClienteControllerTest
    {
        private Cliente clienteTest;

        public ClienteControllerTest()
        {
            clienteTest = (Cliente) UserManagerMock.CLIENTE_TEST;
        }

        [Fact]
        public void IndexTextShouldReturnRedirect()
            => MyController<ClienteController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .Redirect()
                .ToAction("Perfil");

        [Fact]
        public void PerfilTest()
            => MyController<ClienteController>
                .Instance()
                .WithDbContext(dbContext => dbContext // <---
                    .WithEntities(entities => entities.AddRange(
                        clienteTest)))
                .Calling(c => c.Perfil())
                .ShouldReturn()
                .View()
                .WithModelOfType<PerfilViewModel>()
                .Passing(model => model.Cliente == clienteTest && model.NumeroViagens ==2 );

        [Fact]
        public void EditarPerfilGetTest()
            => MyController<ClienteController>
                .Instance()
                .Calling(c => c.EditarPerfil())
                .ShouldReturn()
                .View()
                .WithModelOfType<Cliente>()
                .Passing(model => Assert.Equal(model,clienteTest));

     /*   [Fact]
        public void EditarPerfilPostTest()
            => MyController<ClienteController>
                .Instance()
                 .WithDbContext(dbContext => dbContext // <---
                    .WithEntities(entities => entities.AddRange(
                        clienteTest)))
                .Calling(c => c.EditarPerfil(GetEditarPerfilViewModel()))
                .ShouldReturn()
                .View();

    */
        [Fact]
        public void UploadImageShouldReturnRedirectTest()
            => MyController<ClienteController>
                .Instance()
                .WithDbContext(dbContext => dbContext // <---
                    .WithEntities(entities => entities.AddRange(
                        clienteTest)))
                .Calling(c => c.AlterarImagemPerfil(null))
                .ShouldReturn()
                .Redirect().ToAction("EditarPerfil");


        [Fact]
        public void NotificacaoShouldReturnFalse()
            =>
            MyController<ClienteController>.Instance()
                .Calling(c => c.MarcarNotificacaoLida(0))
                .ShouldReturn()
                .ResultOfType<bool>()
                .Passing(c => c == false);

        [Fact]
        public void NotificacaoShouldReturnTrue()
            =>
            MyController<ClienteController>.Instance()
                .WithDbContext(dbContext => dbContext // <---
                    .WithEntities(entities => entities.AddRange(
                        new Notificacao {Lida = false, Mensagem = "Test",NotificacaoId = 1, Tipo = "teste"})))
                .Calling(c => c.MarcarNotificacaoLida(1))
                .ShouldReturn()
                .ResultOfType<bool>()
                .Passing(c => c == true);

        //------------------

        [Fact]
        public void DisponibilidadePontualTesteDisponivel()
        {
            Aeroporto aeroporto1 = new Aeroporto()
            {
                AeroportoId = 1,
                Nome = "Aeroporto1",
                Latitude = 38.779444,
                Longitude = -9.136111,               
            };

            Companhia companhia = new Companhia()
            {
                Nome = "Companhia1",
                CompanhiaId = 1,
                ListaReservas = new List<Reserva>()
            };

            Jato jato1 = new Jato()
            {
                CompanhiaId = 1,
                AeroportoId = 1,
                Nome = "Jato1",
                EmFuncionamento = true,
                CreditosBase = 2200,
                CreditosPorKilometro = 1.1
            };
            
            Disponibilidade disponibilidade1 = new Disponibilidade()
            {
                DisponibilidadeId = 1,
                Inicio = "22/09/2017",
                Fim = "24/09/2017"
            };
           
            jato1.ListaDisponibilidade.Add(disponibilidade1);

            DateTime dataPartida = new DateTime(2017, 9, 23);


            MyController<ClienteController>.Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        aeroporto1,companhia, jato1, disponibilidade1)))
                .Calling(c => c.AeroportosDisponiveis(dataPartida))
                .ShouldReturn()
                .ResultOfType<IEnumerable<Aeroporto>>()
                .Passing(c => c.Count() > 0);

        }

        [Fact]
        public void DisponibilidadePontualTesteIndisponivel()
        {
            Aeroporto aeroporto1 = new Aeroporto()
            {
                AeroportoId = 1,
                Nome = "Aeroporto1",
                Latitude = 38.779444,
                Longitude = -9.136111,
            };

            Companhia companhia = new Companhia()
            {
                Nome = "Companhia1",
                CompanhiaId = 1,
                ListaReservas = new List<Reserva>()
            };

            Jato jato1 = new Jato()
            {
                CompanhiaId = 1,
                AeroportoId = 1,
                Nome = "Jato1",
                EmFuncionamento = true,
                CreditosBase = 2200,
                CreditosPorKilometro = 1.1
            };

            Disponibilidade disponibilidade1 = new Disponibilidade()
            {
                DisponibilidadeId = 1,
                Inicio = "22/09/2017",
                Fim = "24/09/2017"
            };

            jato1.ListaDisponibilidade.Add(disponibilidade1);

            DateTime dataPartida = new DateTime(2017, 10, 23);


            MyController<ClienteController>.Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        aeroporto1, companhia, jato1, disponibilidade1)))
                .Calling(c => c.AeroportosDisponiveis(dataPartida))
                .ShouldReturn()
                .ResultOfType<IEnumerable<Aeroporto>>()
                .Passing(c => c.Count() == 0);

        }

        [Fact]
        public void DisponibilidadePontualTesteIndisponivelReservado()
        {
            Aeroporto aeroporto1 = new Aeroporto()
            {
                AeroportoId = 1,
                Nome = "Aeroporto1",
                Latitude = 38.779444,
                Longitude = -9.136111,
            };

            Aeroporto aeroporto2 = new Aeroporto()
            {
                AeroportoId = 2,
                Nome = "Aeroporto2",
                Latitude = 40.472222,
                Longitude = -3.560833,
            };

            Companhia companhia = new Companhia()
            {
                Nome = "Companhia1",
                CompanhiaId = 1,
                ListaReservas = new List<Reserva>()
            };

            Jato jato1 = new Jato()
            {
                CompanhiaId = 1,
                AeroportoId = 1,
                Nome = "Jato1",
                EmFuncionamento = true,
                CreditosBase = 2200,
                CreditosPorKilometro = 1.1
            };

            Disponibilidade disponibilidade1 = new Disponibilidade()
            {
                DisponibilidadeId = 1,
                Inicio = "22/09/2017",
                Fim = "24/09/2017"
            };

            
            Reserva reserva = new Reserva()
            {
                DataPartida = new DateTime(2017, 9, 20),
                DataChegada = new DateTime(2017, 9, 25),
                JatoId = 1,
                AeroportoPartidaId = 1,
                AeroportoDestinoId = 2
            };
            companhia.ListaReservas.Add(reserva);

            jato1.ListaDisponibilidade.Add(disponibilidade1);

            DateTime dataPartida = new DateTime(2017, 9, 23);

            MyController<ClienteController>.Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        aeroporto1, 
                        aeroporto2,
                        reserva,  
                        companhia, 
                        jato1, 
                        disponibilidade1)))
                .Calling(c => c.AeroportosDisponiveis(dataPartida))
                .ShouldReturn()
                .ResultOfType<IEnumerable<Aeroporto>>()
                .Passing(c => c.Count() == 0);

        }

        [Fact]
        public void DisponibilidadePontualTesteDeslocacao()
        {
            Aeroporto aeroporto1 = new Aeroporto()
            {
                AeroportoId = 1,
                Nome = "Aeroporto1",
                Latitude = 38.779444,
                Longitude = -9.136111,
            };

            Aeroporto aeroporto2 = new Aeroporto()
            {
                AeroportoId = 2,
                Nome = "Aeroporto2",
                Latitude = 40.472222,
                Longitude = -3.560833,
            };

            Companhia companhia = new Companhia()
            {
                Nome = "Companhia1",
                CompanhiaId = 1,
                ListaReservas = new List<Reserva>()
            };

            Jato jato1 = new Jato()
            {
                CompanhiaId = 1,
                AeroportoId = 1,
                Nome = "Jato1",
                EmFuncionamento = true,
                CreditosBase = 2200,
                CreditosPorKilometro = 1.1
            };

            Disponibilidade disponibilidade1 = new Disponibilidade()
            {
                DisponibilidadeId = 1,
                Inicio = "22/09/2017",
                Fim = "24/09/2017"
            };
            
            jato1.ListaDisponibilidade.Add(disponibilidade1);

            DateTime dataPartida = new DateTime(2017, 9, 23);

            MyController<ClienteController>.Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        aeroporto1,
                        aeroporto2,
                        companhia,
                        jato1,
                        disponibilidade1)))
                .Calling(c => c.AeroportosDisponiveis(dataPartida))
                .ShouldReturn()
                .ResultOfType<IEnumerable<Aeroporto>>()
                .Passing(c => c.Any(a => a.AeroportoId == aeroporto2.AeroportoId));

        }

        [Fact]
        public void DisponibilidadeIntervaloTesteDisponivel()
        {
            Aeroporto aeroporto1 = new Aeroporto()
            {
                AeroportoId = 1,
                Nome = "Aeroporto1",
                Latitude = 38.779444,
                Longitude = -9.136111,
            };

            Aeroporto aeroporto2 = new Aeroporto()
            {
                AeroportoId = 2,
                Nome = "Aeroporto2",
                Latitude = 40.472222,
                Longitude = -3.560833,
            };

            Companhia companhia = new Companhia()
            {
                Nome = "Companhia1",
                CompanhiaId = 1,
                ListaReservas = new List<Reserva>()
            };

            Jato jato1 = new Jato()
            {
                CompanhiaId = 1,
                AeroportoId = 1,
                Nome = "Jato1",
                EmFuncionamento = true,
                CreditosBase = 2200,
                CreditosPorKilometro = 1.1
            };

            Disponibilidade disponibilidade1 = new Disponibilidade()
            {
                DisponibilidadeId = 1,
                Inicio = "22/09/2017",
                Fim = "24/09/2017"
            };

            jato1.ListaDisponibilidade.Add(disponibilidade1);

            DateTime dataPartida = new DateTime(2017, 9, 23);

            MyController<ClienteController>.Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        aeroporto1, aeroporto2, companhia, jato1, disponibilidade1)))
                .Calling(c => c.AeroportosDestinoDisponiveis(aeroporto1.AeroportoId, dataPartida))
                .ShouldReturn()
                .ResultOfType<IEnumerable<Aeroporto>>()
                .Passing(c => c.Any(a => a.AeroportoId == aeroporto2.AeroportoId));

        }

        [Fact]
        public void DisponibilidadeIntervaloTesteIndisponivel()
        {
            Aeroporto aeroporto1 = new Aeroporto()
            {
                AeroportoId = 1,
                Nome = "Aeroporto1",
                Latitude = 38.779444,
                Longitude = -9.136111,
            };

            Aeroporto aeroporto2 = new Aeroporto()
            {
                AeroportoId = 2,
                Nome = "Aeroporto2",
                Latitude = 40.472222,
                Longitude = -3.560833,
            };

            Companhia companhia = new Companhia()
            {
                Nome = "Companhia1",
                CompanhiaId = 1,
                ListaReservas = new List<Reserva>()
            };

            Jato jato1 = new Jato()
            {
                CompanhiaId = 1,
                AeroportoId = 1,
                Nome = "Jato1",
                EmFuncionamento = true,
                CreditosBase = 2200,
                CreditosPorKilometro = 1.1
            };

            Disponibilidade disponibilidade1 = new Disponibilidade()
            {
                DisponibilidadeId = 1,
                Inicio = "22/09/2017",
                Fim = "24/09/2017"
            };

            jato1.ListaDisponibilidade.Add(disponibilidade1);

            DateTime dataPartida = new DateTime(2017, 10, 23);

            MyController<ClienteController>.Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        aeroporto1, aeroporto2, companhia, jato1, disponibilidade1)))
                .Calling(c => c.AeroportosDestinoDisponiveis(aeroporto1.AeroportoId, dataPartida))
                .ShouldReturn()
                .ResultOfType<IEnumerable<Aeroporto>>()
                .Passing(c => c.Count() == 0);

        }

        [Fact]
        public void DisponibilidadeIntervaloTesteIndisponivelReservado()
        {
            Aeroporto aeroporto1 = new Aeroporto()
            {
                AeroportoId = 1,
                Nome = "Aeroporto1",
                Latitude = 38.779444,
                Longitude = -9.136111,
            };

            Aeroporto aeroporto2 = new Aeroporto()
            {
                AeroportoId = 2,
                Nome = "Aeroporto2",
                Latitude = 40.472222,
                Longitude = -3.560833,
            };

            Companhia companhia = new Companhia()
            {
                Nome = "Companhia1",
                CompanhiaId = 1,
                ListaReservas = new List<Reserva>()
            };

            Jato jato1 = new Jato()
            {
                CompanhiaId = 1,
                AeroportoId = 1,
                Nome = "Jato1",
                EmFuncionamento = true,
                CreditosBase = 2200,
                CreditosPorKilometro = 1.1
            };

            Disponibilidade disponibilidade1 = new Disponibilidade()
            {
                DisponibilidadeId = 1,
                Inicio = "22/09/2017",
                Fim = "24/09/2017"
            };

            Reserva reserva = new Reserva()
            {
                DataPartida = new DateTime(2017, 9, 20),
                DataChegada = new DateTime(2017, 9, 25),
                JatoId = 1,
                AeroportoPartidaId = 1,
                AeroportoDestinoId = 2
            };
            companhia.ListaReservas.Add(reserva);

            jato1.ListaDisponibilidade.Add(disponibilidade1);

            DateTime dataPartida = new DateTime(2017, 9, 23);

            MyController<ClienteController>.Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        aeroporto1, aeroporto2, reserva, companhia, jato1, disponibilidade1)))
                .Calling(c => c.AeroportosDestinoDisponiveis(aeroporto1.AeroportoId, dataPartida))
                .ShouldReturn()
                .ResultOfType<IEnumerable<Aeroporto>>()
                .Passing(c => c.Count() == 0);

        }






        public EditarPerfilViewModel GetEditarPerfilViewModel()
        {
            return new EditarPerfilViewModel {Apelido = "manel", Nome = "arroz" , Contacto = "123"};
        }
    }
}
