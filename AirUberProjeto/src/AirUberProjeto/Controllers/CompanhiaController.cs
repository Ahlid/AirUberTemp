using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirUberProjeto.Data;
using AirUberProjeto.Models;
using AirUberProjeto.Models.CompanhiaViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirUberProjeto.Controllers
{
    /// <summary>
    /// Classe responsável por receber todos os pedidos do browser e tratar dos mesmos relativamente às companhias
    /// </summary>
    public class CompanhiaController : Controller
    {

        /// <summary>
        /// Utilizado para saber o caminho absoluto da pasta wwwRoot
        /// </summary>
        private readonly IHostingEnvironment _environment;

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
        /// <param name="environment">O ambiente da aplicação</param>
        public CompanhiaController(AirUberDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        /// <summary>
        /// Redirecciona para a acção 'Perfil Companhia'
        /// </summary>
        /// <returns>Retorna a View retornada pela acção Perfil Companhia</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("PerfilCompanhia");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PerfilCompanhia()
        {

            string idColaborador = _userManager.GetUserAsync(this.User).Result.Id;

            Colaborador colaborador = (_context.Colaborador.Where(c => c.Id == idColaborador)
                                                           .Select(c => c))
                                                           .Single();

            Companhia companhia = (_context.Companhia.Select(c => c).Include(c => c.Pais)
                                                             .Include(c => c.Estado)
                                                             .Include(c => c.ContaDeCreditos)
                                                             .Include(c => c.ListaReservas)
                                                             .Include(c => c.ListaColaboradores)
                                                             .Include(c => c.ListaJatos)
                                                             .Include(c => c.ListaExtras)
                                                             .Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

            PerfilCompanhiaViewModel perfilViewModel = new PerfilCompanhiaViewModel()
            {
                Colaborador = colaborador,
                Companhia = companhia,
            };


            List<Notificacao> notificacoes = _context.Notificacao.Where(n => n.UtilizadorId == colaborador.Id).ToList();

            foreach (Notificacao notificacao in notificacoes)
            {
                perfilViewModel.Notificacoes.Add(notificacao);

            }

            return View(perfilViewModel);
        }

        /// <summary>
        /// Apresenta a página de edição de perfil da companhia
        /// </summary>
        /// <returns>A View de edição do perfil da companhia</returns>
        [HttpGet]
        public IActionResult EditarPerfilCompanhia()
        {
            Colaborador colaborador = (Colaborador)_userManager.GetUserAsync(this.User).Result;

            Companhia companhia = (_context.Companhia.Select(c => c).Include(c => c.Pais)
                                                             .Include(c => c.Estado)
                                                             .Include(c => c.ContaDeCreditos)
                                                             .Include(c => c.ListaReservas)
                                                             .Include(c => c.ListaColaboradores)
                                                             .Include(c => c.ListaJatos)
                                                             .Include(c => c.ListaExtras)
                                                             .Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();
            return View(companhia);
        }


        /// <summary>
        /// Trata de um pedido de alteração de dados de uma companhia
        /// </summary>
        /// <param name="viewModel">ViewModel do pedido de alteração de dados</param>
        /// <returns>A view de edição de perfil da companhia</returns>
        [HttpPost]
        public IActionResult EditarPerfilCompanhia(EditarCompanhiaViewModel viewModel)
        {
           
            if (ModelState.IsValid) // se os dados forem válidos
            {
                Colaborador colaborador = (Colaborador)_userManager.GetUserAsync(this.User).Result;
                Companhia companhia = (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

                companhia.Nome = viewModel.Nome;
                companhia.Nif = viewModel.Nif;
                companhia.Contact = viewModel.Contact;
                _context.Update(companhia);
                _context.SaveChanges();
                ViewData["Success"] = true;
                return View(companhia);
            }
            
            return RedirectToAction("EditarPerfilCompanhia");
        }



        [HttpGet]
        public IActionResult VerJatos()
        {

            return View();
        }

        [HttpGet]
        public IActionResult EditarJatos()
        {

            return View();
        }

        [HttpPost]
        public IActionResult EditarJatos(EditarJatoViewModel viewModel)
        {

            return View();
        }

        [HttpGet]
        public IActionResult CriarJato(CriarJatoViewModel viewModel)
        {

            return View();
        }

    }
}