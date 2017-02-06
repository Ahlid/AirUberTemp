using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AirUberProjeto.Models;
using AirUberProjeto.Test.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AirUberProjeto.Test.Mocks
{
    public class UserManagerMock2 : UserManager<ApplicationUser>
    {
        public static ApplicationUser Colaborador = new Colaborador
        {
            Id = "1",
            CompanhiaId = 1,
            Nome = "Colaborador1",
            Email = "colaborador1@teste.com",
            IsAdministrador = true

        };
        public static Colaborador Colaborador2 = new Colaborador
        {
            Id = "2",
            CompanhiaId = 1,
            Nome = "Colaborador2",
            Email = "colaborador2@teste.com",
            IsAdministrador = true

        };

        public static Companhia Companhia = new Companhia
        {
            Nome = "Compahia1",
            CompanhiaId = 1,
            Contact = "91919191",

        };

        public static TipoExtra TipoExtra = new TipoExtra
        {
            Nome = "ExtraTipo1",
            TipoExtraId = 1,

        };

        public static Extra Extra = new Extra
        {
            ExtraId = 2,
            Nome = "Extra1",
            CompanhiaId = 1,
            Valor = 2.6m,
            TipoExtraId = 1,
            
        };

        public static Modelo modeloJato5 = new Modelo
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
        };

        public static Jato Jato5 = new Jato
        {
            JatoId = 5,
            CompanhiaId = 1,
            ModeloId = 1
     
        };


        public UserManagerMock2(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

            Companhia.ContaDeCreditos = new ContaDeCreditos { ContaDeCreditosId = 1 };
            Companhia.ContaDeCreditosId = 1;
            Companhia.ListaColaboradores.Add((Colaborador)Colaborador);
            Companhia.ListaColaboradores.Add(Colaborador2);
            Companhia.ListaExtras.Add(Extra);
            
            Companhia.ListaJatos.Add(Jato5);
            Companhia.ListaReservas.Add(new Reserva());
        }


        public override Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal)
        {
            ApplicationUser colaborador = new Colaborador
            {
                Id = "1"
            };
            return Task.FromResult(Colaborador);

        }
    }
}
