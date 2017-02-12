using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirUberProjeto.Data;
using AirUberProjeto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirUberProjeto.Controllers
{

    /// <summary>
    /// Controlador responsável pela gestão de creditos
    /// </summary>
    [Authorize(Roles = Roles.ROLE_CLIENTE + ", " + Roles.ROLE_COLABORADOR +", " + Roles.ROLE_COLABORADOR_ADMIN)]
    public class CreditosController : Controller
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
        /// Construtor do controlador
        /// </summary>
        /// <param name="context">O DB context da aplicação</param>
        /// <param name="userManager">O manager dos utilizadores</param>
        public CreditosController(AirUberDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// A acção index é a ação default do controlador e redireciona para a ação comprar
        /// </summary>
        /// <returns>Retorna a view retornada pela acção comprar</returns>
        [HttpGet]
        [Authorize(Roles = Roles.ROLE_CLIENTE)]
        public IActionResult Index()
        {
            setupNav();
            return RedirectToAction("Comprar");
        }

        public void setupNav()
        {
            string id = _userManager.GetUserAsync(this.User).Result.Id;
            List<Notificacao> notificacoes = _context.Notificacao.Where((n) =>
                    n.UtilizadorId == id
                    && !n.Lida
            ).ToList();
            ViewBag.navegacao = true;
            ViewBag.notificacoes = notificacoes;
        }


        /// <summary>
        /// A acção comprar para poder comprar creditos
        /// </summary>
        /// <returns>Retorna a view Comprar</returns>
        [HttpGet]
        [Authorize(Roles = Roles.ROLE_CLIENTE)]
        public IActionResult Comprar()
        {
            setupNav();
            return View();
        }

        /// <summary>
        /// A ação para poder carregar a conta
        /// </summary>
        /// <param name="amount">O montante a carregar</param>
        /// <returns>Os creditos totais do utilizador</returns>
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_CLIENTE)]
        public string Carregar(int amount)
        {
            setupNav();
            Cliente cliente = (Cliente) _userManager.GetUserAsync(this.User).Result;

            cliente = _context.Cliente.Select(c=>c).Include(c => c.ContaDeCreditos).Include(c => c.ContaDeCreditos.HistoricoTransacoeMonetarias).Single(c => c.Id == cliente.Id);
            cliente.ContaDeCreditos.JetCashActual += amount;
            cliente.ContaDeCreditos.HistoricoTransacoeMonetarias.MovimentosMonetarios.Add(new MovimentoMonetario {Montante = amount, TipoMovimento = TipoMovimento.Carregamento, HistoricoTransacoeMonetariasId = cliente.ContaDeCreditos.HistoricoTransacoeMonetariasId});

            _context.Update(cliente);
            _context.SaveChanges();
            

            return ""+cliente.ContaDeCreditos.JetCashActual;
        }

        /// <summary>
        /// Acao responsavel para devolver uma view com as transaçoes do utilizador
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        [Authorize(Roles = Roles.ROLE_CLIENTE)]
        public IActionResult VerHistoricoTransacoes()
        {
            setupNav();
            Cliente cliente = (Cliente)_userManager.GetUserAsync(this.User).Result;

            cliente = _context.Cliente.Select(c => c).Include(c => c.ContaDeCreditos).Include(c => c.ContaDeCreditos.HistoricoTransacoeMonetarias).Include(c=>c.ContaDeCreditos.HistoricoTransacoeMonetarias.MovimentosMonetarios).Single(c => c.Id == cliente.Id);

            return View(cliente.ContaDeCreditos.HistoricoTransacoeMonetarias.MovimentosMonetarios);
        }

        /// <summary>
        /// Acao responsavel para devolver uma view com as transaçoes do utilizador
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        [Authorize(Roles = Roles.ROLE_COLABORADOR_ADMIN + ", " + Roles.ROLE_COLABORADOR)]
        public IActionResult VerHistoricoTransacoesCompanhia()
        {
            setupNav();
            Colaborador colaborador = (Colaborador)_userManager.GetUserAsync(this.User).Result;

            colaborador = _context.Colaborador.Select(c => c)
                .Include(c => c.Companhia)
                .Include(c => c.Companhia.ContaDeCreditos)
                .Include(c => c.Companhia.ContaDeCreditos.HistoricoTransacoeMonetarias)
                .Include(c => c.Companhia.ContaDeCreditos.HistoricoTransacoeMonetarias.MovimentosMonetarios)
                .Single(c => c.Id == colaborador.Id);

            return View(colaborador.Companhia.ContaDeCreditos.HistoricoTransacoeMonetarias.MovimentosMonetarios);
        }

    }
}
