using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirUberProjeto.Controllers;
using AirUberProjeto.Models;
using AirUberProjeto.Test.Mocks;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace AirUberProjeto.Test.Controllers
{
    public class CreditosControllerTest
    {

        private Cliente clienteTest;
        private ContaDeCreditos contaTest;
        private HistoricoTransacoeMonetarias historicoTest;

        public CreditosControllerTest()
        {
            clienteTest = (Cliente)UserManagerMock.CLIENTE_TEST;
            contaTest = UserManagerMock.CONTA_CREDITOS_TEST;
            historicoTest = UserManagerMock.HISTORICO_TEST;
        }




        [Fact]
        public void IndexShouldReturnRedirect()
            => MyController<CreditosController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .Redirect()
                .ToAction("Comprar");


        [Fact]
        public void ComprarGetShouldReturnView()
           => MyController<CreditosController>
               .Instance()
               .Calling(c => c.Comprar())
               .ShouldReturn()
               .View();


        [Fact]
        public void CarregarPostShouldChangeAmountAndReturnTotalAmount()
          => MyController<CreditosController>
              .Instance()
              .WithDbContext(dbContext => dbContext // <---
                    .WithEntities(entities => entities.AddRange(
                        clienteTest)))
              .Calling(c => c.Carregar(5000))
              .ShouldReturn()
              .ResultOfType<string>().
              Passing( c => c == ""+contaTest.JetCashActual);
        [Fact]
        public void VerHistoricoTransacoesShouldReturnViewWithModel()
            => MyController<CreditosController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(enti => enti.AddRange(clienteTest, contaTest, historicoTest)))
                .Calling(c => c.VerHistoricoTransacoes())
                .ShouldReturn()
                .View()
                .WithModel(historicoTest.MovimentosMonetarios);
    }
}

