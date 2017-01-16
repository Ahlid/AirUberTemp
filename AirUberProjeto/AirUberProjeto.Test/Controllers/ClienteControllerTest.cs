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

        [Fact]
        public void EditarPerfilPostTest()
            => MyController<ClienteController>
                .Instance()
                 .WithDbContext(dbContext => dbContext // <---
                    .WithEntities(entities => entities.AddRange(
                        clienteTest)))
                .Calling(c => c.EditarPerfil(GetEditarPerfilViewModel()))
                .ShouldReturn()
                .View();


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

        public EditarPerfilViewModel GetEditarPerfilViewModel()
        {
         return   new EditarPerfilViewModel {Apelido = "manel", Nome = "arroz" , Contacto = "123"};
        }
    }
}
