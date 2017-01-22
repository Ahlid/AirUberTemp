using AirUberProjeto.Controllers;
using AirUberProjeto.Data;
using AirUberProjeto.Models;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace AirUberProjeto.Test.Controllers
{
    public class RegistoTest
    {
        [Fact]
        public void AddressAndPaymentShouldReturnDefaultView()
            => MyController<AutenticacaoController>
                .Instance()
                .Calling(c => c.ConfirmEmail("1","1"))
                .ShouldReturn()
                .View("Error");
    }
}
