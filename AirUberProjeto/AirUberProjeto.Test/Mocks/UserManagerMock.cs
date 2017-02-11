using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AirUberProjeto.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AirUberProjeto.Test.Mocks
{
    public class UserManagerMock : UserManager<ApplicationUser>
    {
        public static ApplicationUser CLIENTE_TEST = new Cliente
        {
            Id = "1",
            Nome = "Tiago",
            Contacto = "91919191"
        };
        public static HistoricoTransacoeMonetarias HISTORICO_TEST = new HistoricoTransacoeMonetarias();
      

        public static ContaDeCreditos CONTA_CREDITOS_TEST = new ContaDeCreditos
        {
         HistoricoTransacoeMonetarias   = HISTORICO_TEST
        };

        public UserManagerMock(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
           
           ((Cliente) CLIENTE_TEST).ListaReservas.Add(new Reserva());
           ((Cliente) CLIENTE_TEST).ListaReservas.Add(new Reserva());
            ((Cliente) CLIENTE_TEST).ContaDeCreditos = CONTA_CREDITOS_TEST;
            HISTORICO_TEST.MovimentosMonetarios.Add(new MovimentoMonetario {HistoricoTransacoeMonetarias = HISTORICO_TEST, Montante = 2500, TipoMovimento = TipoMovimento.Carregamento});

        }


        public override Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal)
        {
        
            return Task.FromResult(CLIENTE_TEST);
         
        }
    }
}
