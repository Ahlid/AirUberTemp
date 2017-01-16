using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirUberProjeto.Data;
using AirUberProjeto.Models;
using AirUberProjeto.Models.CompanhiaViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirUberProjeto.Controllers
{
    public class CompanhiaController : Controller
    {
        /// <summary>
        /// O contexto da aplicação para poder aceder a dados.
        /// </summary>
        private readonly AirUberDbContext _context;

        /// <summary>
        /// User manager que vai permitir utilizar metodos feitos pela windows de forma a controlar os user.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;



        /// <summary>
        /// Construtor do controlador Companhia
        /// </summary>
        /// <param name="context">O contexto da aplicação</param>
        /// <param name="userManager">O manager dos utilizadores</param>
        public CompanhiaController(AirUberDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            string idColaboradorAdmin = _userManager.GetUserAsync(this.User).Result.Id;

            Colaborador colaborador = (_context.Colaborador.Select(c => c)
                                                           .Include(c => c.Companhia)
                                                           .Where(c => c.Id == idColaboradorAdmin))
                                                           .Single();

            PerfilCompanhiaViewModel perfil = new PerfilCompanhiaViewModel()
            {
                Colaborador = colaborador,
                Companhia = colaborador.Companhia
            };

            //ViewBag.id = idCompanhia + "ada";


            //Colaborador cliente = _context.Colaborador.Select(c => c)
                
                /*(_context.Colaborador
                        .Include(c => c.ContaDeCreditos)
                        .Include(c => c.ListaReservas)
                        .Where(c => c.Id == idCliente)
                        .Select(c => c))
                        .Single();*/

            /* Cliente cliente = (_context.Companhia
                                     .Include(c => c.ContaDeCreditos)
                                     .Include(c => c.ListaReservas)
                                     .Where(c => c.Id == idCliente)
                                     .Select(c => c))
                                     .Single();

             PerfilViewModel viewModel = new PerfilViewModel()
             {
                 Cliente = cliente,
                 NumeroViagens = cliente.ListaReservas.Count
             };



             List<Notificacao> notificacoes = _context.Notificacao.Where((n) =>
                n.UtilizadorId == cliente.Id
             ).ToList();

             foreach (Notificacao notificacao in notificacoes)
             {
                 viewModel.Notificacoes.Add(notificacao);
             }


             return View(viewModel);*/

            return View(perfil);
        }
    }
}