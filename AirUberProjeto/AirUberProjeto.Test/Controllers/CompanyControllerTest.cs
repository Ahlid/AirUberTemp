using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AirUberProjeto.Controllers;
using AirUberProjeto.Models;
using AirUberProjeto.Models.CompanhiaViewModels;
using AirUberProjeto.Test.Mocks;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace AirUberProjeto.Test.Controllers
{
    //https://mytestedasp.net
    public class CompanyControllerTest
    {
        private Companhia companhiaTeste;
        private Colaborador colaboradorTeste;
        private Jato jato5Teste;
        private Modelo modeloJato5Teste;
        private Extra extraTeste;
        private TipoExtra tipoExtraTeste;
        private Colaborador colaborador2Teste;

        public CompanyControllerTest()
        {
            companhiaTeste = UserManagerMock2.Companhia;
            colaboradorTeste = (Colaborador)UserManagerMock2.Colaborador;
            jato5Teste = UserManagerMock2.Jato5;
            modeloJato5Teste = UserManagerMock2.modeloJato5;
            extraTeste = UserManagerMock2.Extra;
            tipoExtraTeste = UserManagerMock2.TipoExtra;
            colaborador2Teste = UserManagerMock2.Colaborador2;
        }


        [Fact]
        public void IndexTextShouldReturnRedirect()
            => MyController<CompanhiaController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .Redirect()
                .ToAction("PerfilCompanhia");


[Fact]
public void PerfilCompanhiaTest()
    => MyController<CompanhiaController>
        .Instance()
        .WithDbContext(dbContext => dbContext 
            .WithEntities(entities => entities.AddRange(
                companhiaTeste, colaboradorTeste)))
        .Calling(c => c.PerfilCompanhia())
        .ShouldReturn()
        .View()
        .WithModelOfType<PerfilCompanhiaViewModel>()
        .Passing(model => model.Companhia == companhiaTeste && 
                    model.Colaborador == colaboradorTeste);



        [Fact]
        public void EditarPerfilCompanhiaGetTest()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext 
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste)))
                .Calling(c => c.EditarPerfilCompanhia())
                .ShouldReturn()
                .View()
                .WithModelOfType<Companhia>()
                .Passing(model => Assert.Equal(model, companhiaTeste));

        [Fact]
        public void EditarPerfilCompanhiaPostTest()
            => MyController<CompanhiaController>
                .Instance()
                    .WithDbContext(dbContext => dbContext 
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste)))
                .Calling(c => c.EditarPerfilCompanhia(new EditarCompanhiaViewModel()
                {
                    Nome = "NomeCompany",
                    Contact = "565656565",
                    Morada = "Morada1",
                    Nif = "16151651",
                }))
                .ShouldReturn()
                .View();

        [Fact]
        public void UploadImageShouldReturnRedirectTest()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste)))
                .Calling(c => c.AlterarImagemPerfil(null))
                .ShouldReturn()
                .Redirect().ToAction("EditarPerfilCompanhia");


        

        [Fact]
        public void AlterarImagemJato()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste)))
                .Calling(c => c.AlterarImagemJato(null, 5))
                .ShouldReturn()
                .Redirect().ToAction("EditarJatos");

        [Fact]
        public void NotificacaoShouldReturnFalse()
            =>
            MyController<CompanhiaController>.Instance()
                .Calling(c => c.MarcarNotificacaoLida(0))
                .ShouldReturn()
                .ResultOfType<bool>()
                .Passing(c => c == false);


        [Fact]
        public void NotificacaoShouldReturnTrue()
            =>
            MyController<CompanhiaController>.Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        new Notificacao
                        {
                            Lida = false,
                            Mensagem = "Test",
                            NotificacaoId = 1,
                            Tipo = "teste"
                        })))
                .Calling(c => c.MarcarNotificacaoLida(1))
                .ShouldReturn()
                .ResultOfType<bool>()
                .Passing(c => c == true);

        [Fact]
        public void VerJatosShouldReturnView()
            =>
            MyController<CompanhiaController>.Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste)))
                .Calling(c => c.VerJatos())
                .ShouldReturn()
                .View()
                .WithModelOfType<IEnumerable<Jato>>();


    
        [Fact]
        public void EditarJatosShouldReturnView()
            =>
            MyController<CompanhiaController>.Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, modeloJato5Teste, jato5Teste)))
                    
                .Calling(c => c.EditarJatos(5))
                .ShouldReturn()
                .View()
                .WithModelOfType<Jato>()
                .Passing(c => c != null);


   
        [Fact]
        public void EditarJatoPostTest()
            => MyController<CompanhiaController>
                .Instance()
                    .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, modeloJato5Teste, jato5Teste)))
                .Calling(c => c.EditarJatos(new EditarJatoViewModel()
                {
                    Nome = "JatoTeste",
                    AeroportoId = 1,
                    CompanhiaId = 1,
                    JatoId = 5,
                    ModeloId = 1
                }))
                .ShouldReturn()
                .Redirect().ToAction("VerJatos");


        //Deverá retornar uma view

        [Fact]
        public void CriarJatoShouldReturnView()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, modeloJato5Teste, jato5Teste)))
                .Calling(c => c.CriarJato())
                .ShouldReturn()
                .View();


        [Fact]
        public void CriarJatoPost()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, modeloJato5Teste, jato5Teste)))
                .Calling(c => c.CriarJato(new CriarJatoViewModel
                    {
                        CompanhiaId = 1,
                        AeroportoId = 1,
                        Nome = "Teste",
                    }))
                .ShouldReturn()
                .Redirect().ToAction("VerJatos");



        [Fact]
        public void ApagarJatoGet()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, modeloJato5Teste, jato5Teste)))
                .Calling(c => c.ApagarJato(5))
                .ShouldReturn()
                .View()
                .WithModelOfType<Jato>();


        [Fact]
        public void ApagarJatoPost()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, modeloJato5Teste, jato5Teste)))
                .Calling(c => c.ApagarJatoConfirmacao(5))
                .ShouldReturn().Redirect().ToAction("VerJatos");


        [Fact]
        public void VerColaboradorGet()
                => MyController<CompanhiaController>
                    .Instance()
                    .WithDbContext(dbContext => dbContext
                        .WithEntities(entities => entities.AddRange(
                            companhiaTeste, modeloJato5Teste, jato5Teste)))
                    .Calling(c => c.VerColaboradores())
                    .ShouldReturn().View();

        [Fact]
        public void AdicionarColaboradorGet()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, colaborador2Teste, 
                        modeloJato5Teste, jato5Teste)))
                .Calling(c => c.AdicionarColaborador())
                .ShouldReturn().View();

        [Fact]
        public void AdicionarColaboradorPost()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, 
                        colaborador2Teste,  
                        modeloJato5Teste, 
                        jato5Teste)))
                .Calling(c => c.AdicionarColaborador(new CriarColaboradorViewModel
                {
                    CompanhiaId = 1,
                    Apelido = "ColaboradorTeste",
                    Email = "teste@testes.pt",
                    Password = "sagTt13986991",
                    ConfirmPassword = "sagTt13986991",
                    IsAdministrador = false,
                    PrimeiroNome = "Teste",
                    
                }))
                .ShouldReturn().Redirect().ToAction("VerColaboradores");

        [Fact]
        public void EditarColaboradorGet()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, colaborador2Teste, modeloJato5Teste, jato5Teste)))
                .Calling(c => c.EditarColaborador("2"))
                .ShouldReturn().View().WithModelOfType<Colaborador>();


        
        [Fact]
        public void EditarColaboradorPost()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, colaborador2Teste, modeloJato5Teste, jato5Teste)))
                .Calling(c => c.EditarColaborador(new EditarColaboradorViewModel()
                {
                    CompanhiaId = 1,
                    Apelido = "ColaboradorTeste2",
                    Email = "teste@testes.pt",
                    IsAdministrador = true,
                    Id = "2",
                    Nome = "Colaborador1",
                    ColaboradorId = "2"
                }))
                .ShouldReturn().Redirect().ToAction("VerColaboradores");


        [Fact]
        public void ApagarColaboradorGet()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, colaborador2Teste, modeloJato5Teste, jato5Teste)))
                .Calling(c => c.ApagarColaborador("1"))
                .ShouldReturn().View().WithModelOfType<Colaborador>();



        [Fact]
        public void ApagarColaboradorPost()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, colaborador2Teste, modeloJato5Teste, jato5Teste)))
                .Calling(c => c.ApagarColaboradorConfirmacao("2"))
                .ShouldReturn().Redirect().ToAction("VerColaboradores");


        //EXTRAS


        [Fact]
        public void VerExtrasGet()
                => MyController<CompanhiaController>
                    .Instance()
                    .WithDbContext(dbContext => dbContext
                        .WithEntities(entities => entities.AddRange(
                            companhiaTeste, extraTeste, tipoExtraTeste, modeloJato5Teste, jato5Teste)))
                    .Calling(c => c.VerExtras())
                    .ShouldReturn().View();

        [Fact]
        public void CriarExtra()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, extraTeste, tipoExtraTeste, modeloJato5Teste, jato5Teste)))
                .Calling(c => c.CriarExtra())
                .ShouldReturn()
                .View();




        [Fact]
        public void CriarExtraPost()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste,
                        tipoExtraTeste,
                        modeloJato5Teste,
                        extraTeste,
                        jato5Teste)))
                .Calling(c => c.CriarExtra(new CriarExtraViewModel
                {
                    
                    CompanhiaId = 1,
                    Valor = 2.6m,
                    Nome = "Teste2",
                    TipoExtraId = 1


                }))
                .ShouldReturn()
                .Redirect().ToAction("VerExtras");  



        [Fact]
        public void ApagarExtraGet()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, extraTeste, tipoExtraTeste, modeloJato5Teste, jato5Teste)))
                .Calling(c => c.ApagarExtra(extraTeste.ExtraId))
                .ShouldReturn()
                .View()
                .WithModelOfType<Extra>();


        [Fact]
        public void ApagarExtraPost()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, extraTeste, tipoExtraTeste, modeloJato5Teste, jato5Teste)))
                .Calling(c => c.ApagarExtraConfirmacao(extraTeste.ExtraId))
                .ShouldReturn().Redirect().ToAction("VerExtras");




        [Fact]
        public void VerModelosGet()
                => MyController<CompanhiaController>
                    .Instance()
                    .WithDbContext(dbContext => dbContext
                        .WithEntities(entities => entities.AddRange(
                            companhiaTeste, extraTeste, tipoExtraTeste, modeloJato5Teste, jato5Teste)))
                    .Calling(c => c.VerModelos())
                    .ShouldReturn().View();

        [Fact]
        public void AdicionarModeloGet()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, extraTeste, tipoExtraTeste, modeloJato5Teste, jato5Teste)))
                .Calling(c => c.AdicionarModelo())
                .ShouldReturn()
                .View();

        [Fact]
        public void AdicionarModeloPost()
            => MyController<CompanhiaController>
                .Instance()
                .WithDbContext(dbContext => dbContext
                    .WithEntities(entities => entities.AddRange(
                        companhiaTeste, extraTeste, tipoExtraTeste, modeloJato5Teste, jato5Teste)))
                .Calling(c => c.AdicionarModelo(new CriarModeloViewModel

                        {
        
                            Alcance = 32,
                            AltitudeIdeal = 2343,
                            AlturaCabine = 3,
                            Capacidade = 50,
                            ComprimentoCabine = 200,
                            Descricao = "TTsdgsdgs",
                            LarguraCabine = 32,
                            NumeroMotores = 2,
                            PesoMaximaBagagens = 50,
                            VelocidadeMaxima = 1500,
                            TipoJatoId = 1
                        }))
                .ShouldReturn()
                .Redirect().ToAction("VerModelos");


    }
}
