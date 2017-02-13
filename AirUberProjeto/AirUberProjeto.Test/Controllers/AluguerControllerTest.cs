using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirUberProjeto.Controllers;
using AirUberProjeto.Models;
using AirUberProjeto.Models.ClienteViewModels;
using AirUberProjeto.Test.Mocks;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace AirUberProjeto.Test.Controllers
{
    public class AluguerControllerTest
    {

        private Cliente clienteTest;
        private ContaDeCreditos contaTest;
        private HistoricoTransacoeMonetarias historicoTest;

        public AluguerControllerTest()
        {
            clienteTest = (Cliente)UserManagerMock.CLIENTE_TEST;
            contaTest = UserManagerMock.CONTA_CREDITOS_TEST;
            historicoTest = UserManagerMock.HISTORICO_TEST;
        }



        [Fact]
        public void ProcurarOfertaShouReturnView()
            => MyController<ClienteController>
                .Instance()
                .Calling(c => c.ProcurarOfertas())
                .ShouldReturn()
                .View();

        [Fact]
        public void AeroportosDisponiveisShowReturnListWithOneElement()
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
                Inicio = "2017-09-22",
                Fim = "2017-09-24"
            };

            jato1.ListaDisponibilidade.Add(disponibilidade1);

            DateTime dataPartida = new DateTime(2017, 9, 23);

             MyController<ClienteController>
                .Instance()
                .WithDbContext(dbContext => dbContext // <---
                    .WithEntities(entities => entities.AddRange(
                         aeroporto1, companhia, jato1, disponibilidade1)))
                .Calling(c => c.AeroportosDisponiveis(dataPartida))
                .ShouldReturn()
                .ResultOfType<IEnumerable<Aeroporto>>()
                .Passing( c => c.Count() == 1);

            
        }



        [Fact]
        public void AeroportosDisponiveisShowReturnListEmpty()
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
                Inicio = "2017-09-22",
                Fim = "2017-09-24"
            };

            jato1.ListaDisponibilidade.Add(disponibilidade1);

            DateTime dataPartida = new DateTime(2017, 9, 21);

            MyController<ClienteController>
               .Instance()
               .WithDbContext(dbContext => dbContext // <---
                   .WithEntities(entities => entities.AddRange(
                        aeroporto1, companhia, jato1, disponibilidade1)))
               .Calling(c => c.AeroportosDisponiveis(dataPartida))
               .ShouldReturn()
               .ResultOfType<IEnumerable<Aeroporto>>()
               .Passing(c => c.Count() == 0);


        }




        [Fact]
        public void AeroportosDisponiveisDestinoShowReturnListWithOneElement()
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
                Latitude = 39.779444,
                Longitude = -10.136111,
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
                CreditosPorKilometro = 1.1,
                DistanciaMaxima = 1000000,
                TempoPreparacao = 1,
                VelocidadeMedia = 200
            };

            Disponibilidade disponibilidade1 = new Disponibilidade()
            {
                DisponibilidadeId = 1,
                Inicio = "2017-09-22",
                Fim = "2017-09-26"
            };

            jato1.ListaDisponibilidade.Add(disponibilidade1);

            DateTime dataPartida = new DateTime(2017, 9, 23);

            MyController<ClienteController>
               .Instance()
               .WithDbContext(dbContext => dbContext // <---
                   .WithEntities(entities => entities.AddRange(
                      aeroporto2,  aeroporto1, companhia, jato1, disponibilidade1)))
               .Calling(c => c.AeroportosDestinoDisponiveis(aeroporto1.AeroportoId,dataPartida))
               .ShouldReturn()
               .ResultOfType<IEnumerable<Aeroporto>>()
               .Passing(c => c.Count() == 1);


        }



        [Fact]
        public void AeroportosDisponiveisDestinoShowReturnListEmpty()
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
                Latitude = 39.779444,
                Longitude = -10.136111,
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
                CreditosPorKilometro = 1.1,
                DistanciaMaxima = 1000000,
                TempoPreparacao = 1,
                VelocidadeMedia = 1
            };

            Disponibilidade disponibilidade1 = new Disponibilidade()
            {
                DisponibilidadeId = 1,
                Inicio = "2017-09-22",
                Fim = "2017-09-26"
            };

            jato1.ListaDisponibilidade.Add(disponibilidade1);

            DateTime dataPartida = new DateTime(2017, 9, 29);

            MyController<ClienteController>
               .Instance()
               .WithDbContext(dbContext => dbContext // <---
                   .WithEntities(entities => entities.AddRange(
                      aeroporto2, aeroporto1, companhia, jato1, disponibilidade1)))
               .Calling(c => c.AeroportosDestinoDisponiveis(aeroporto1.AeroportoId, dataPartida))
               .ShouldReturn()
               .ResultOfType<IEnumerable<Aeroporto>>()
               .Passing(c => c.Count() == 0);


        }



        [Fact]
        public void VerJatosShowldReturnView()
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
                Nome = "Aeroporto1",
                Latitude = 39.779444,
                Longitude = -10.136111,
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
                CreditosPorKilometro = 1.1,
                DistanciaMaxima = 1000000,
                TempoPreparacao = 1,
                VelocidadeMedia = 200
            };

            Disponibilidade disponibilidade1 = new Disponibilidade()
            {
                DisponibilidadeId = 1,
                Inicio = "2017-09-22",
                Fim = "2017-09-24"
            };

            jato1.ListaDisponibilidade.Add(disponibilidade1);

            DateTime dataPartida = new DateTime(2017, 9, 23);

            MyController<ClienteController>
                .Instance()
                .WithDbContext(dbContext => dbContext // <---
                    .WithEntities(entities => entities.AddRange(
                        aeroporto2, aeroporto1, companhia, jato1, disponibilidade1)))
                .Calling(c => c.VerJatos(aeroporto1.AeroportoId, aeroporto2.AeroportoId, dataPartida))
                .ShouldReturn()
                .View();


        }





        [Fact]
        public void VerOertaShowldReturnViewWithViewModel()
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
                Nome = "Aeroporto1",
                Latitude = 39.779444,
                Longitude = -10.136111,
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
                CreditosPorKilometro = 1.1,
                DistanciaMaxima = 1000000,
                TempoPreparacao = 1,
                VelocidadeMedia = 200
            };

            Disponibilidade disponibilidade1 = new Disponibilidade()
            {
                DisponibilidadeId = 1,
                Inicio = "2017-09-22",
                Fim = "2017-09-24"
            };

            jato1.ListaDisponibilidade.Add(disponibilidade1);

            DateTime dataPartida = new DateTime(2017, 9, 23);



            MyController<ClienteController>
                .Instance()
                .WithDbContext(dbContext => dbContext // <---
                    .WithEntities(entities => entities.AddRange(
                        aeroporto2, aeroporto1, companhia, jato1, disponibilidade1)))
                .Calling(c => c.VerOferta(aeroporto1.AeroportoId, aeroporto2.AeroportoId, dataPartida, jato1.JatoId))
                .ShouldReturn()
                .View();



        }


    }
}
