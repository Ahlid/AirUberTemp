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
using Microsoft.AspNetCore.Mvc.Rendering;
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
        /// Apresenta a página de perfil da companhia
        /// </summary>
        /// <returns>A View de perfil da companhia</returns>
        [HttpGet]
        public IActionResult PerfilCompanhia()
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


        /// <summary>
        /// Apresenta a página com todos os jatos da companhia 
        /// </summary>
        /// <returns>View para visualizar jatos</returns>
        [HttpGet]
        public IActionResult VerJatos()
        {
            Colaborador colaborador = (Colaborador)_userManager.GetUserAsync(this.User).Result;
            Companhia companhia = (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

            var jatos = _context.Jato.Select(j => j).Include(j => j.Modelo)
                                                    .Include(j => j.Modelo.TipoJato)
                                                    .Include(j => j.Companhia)
                                                    .Include(j => j.Aeroporto)
                                                    .Where(j => j.CompanhiaId == companhia.CompanhiaId);
                                                    // IR BUSCAR A COMPANHIA E SO DEVOLVE OS JATOS DA COMPANHIAL
            return View(jatos);
        }


        /// <summary>
        /// Apresenta a página para editar os dados dos jatos
        /// </summary>
        /// <param name="id">identificador único do jato</param>
        /// <returns>View para visualizar a página de edição de jatos</returns>
        [HttpGet]
        public IActionResult EditarJatos(int? id)
        {

            if(id != null) //se o id do jato existe, ou seja, se foi selecionado um jato
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

                Jato jato = (_context.Jato.Select(c => c).Include(c => c.Aeroporto)
                                                         .Include(c => c.Companhia)
                                                         .Include(c => c.Modelo)
                                                         .Where(c => c.JatoId == id)).Single();

                var aeroportos = _context.Aeroporto.Select(a => new { Id = a.AeroportoId, Valor = a.Nome });

                var modelos = (from Modelo in _context.Modelo
                               join TipoJato in _context.TipoJato
                                   on Modelo.TipoJatoId equals TipoJato.TipoJatoId
                               select new { Modelo.ModeloId, TipoJato.Nome });

                ViewBag.aeroportos = new SelectList(aeroportos, "Id", "Valor");
                ViewBag.companhia = companhia.Nome;
                ViewBag.modelos = new SelectList(modelos, "ModeloId", "Nome");

                return View(jato);
            }
           
            return NotFound();
        }


        /// <summary>
        /// Trata de um pedido de alteração de dados de um jato
        /// </summary>
        /// <param name="viewModel">ViewModel do pedido de alteração de dados</param>
        /// <returns>A view de edição de um jato da companhia</returns>
        [HttpPost]
        public IActionResult EditarJatos(EditarJatoViewModel viewModel)
        {
            if (ModelState.IsValid) // se os dados forem válidos
            {
                Colaborador colaborador = (Colaborador)_userManager.GetUserAsync(this.User).Result;
                Companhia companhia = (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

                Jato jato = (_context.Jato.Select(c => c).Include(c => c.Aeroporto)
                                                         .Include(c => c.Companhia)
                                                         .Include(c => c.Modelo)
                                                         .Where(c => c.JatoId == viewModel.JatoId))
                                                         .Single();

                jato.Nome = viewModel.Nome;
                jato.AeroportoId = viewModel.AeroportoId;
                jato.ModeloId = viewModel.ModeloId;
                
                _context.Update(jato);
                _context.SaveChanges();
                return RedirectToAction("VerJatos");
            }

            return RedirectToAction("EditarJatos");
        }

        /// <summary>
        /// Apresenta a página para criar jatos de uma companhia
        /// </summary>
        /// <returns>View para criar jatos</returns>
        [HttpGet]
        public IActionResult CriarJato()
        {

            Colaborador colaborador = (Colaborador)_userManager.GetUserAsync(this.User).Result;
            Companhia companhia = (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

            var aeroportos = _context.Aeroporto.Select(a => new { Id = a.AeroportoId, Valor = a.Nome });

            //var modelos = _context.Modelo.Select(m => new { Id = m.ModeloId, Valor = m.Descricao });

            // o nome do modelo esta no tipo de jato e é necessário o join, porque estão em tabelas separadas
            var modelos = (from Modelo in _context.Modelo
                         join TipoJato in _context.TipoJato
                             on Modelo.TipoJatoId equals TipoJato.TipoJatoId
                         select new { Modelo.ModeloId, TipoJato.Nome });

            ViewBag.aeroportos = new SelectList(aeroportos, "Id", "Valor");
            ViewBag.companhia = companhia.Nome;
            ViewBag.modelos = new SelectList(modelos, "ModeloId", "Nome");

            return View();
        }


        /// <summary>
        /// Trata de um pedido de criação de um jato
        /// </summary>
        /// <param name="viewModel">ViewModel do pedido de criação de dados</param>
        /// <returns>A view de criação de um jato</returns>
        [HttpPost]
        public IActionResult CriarJato(CriarJatoViewModel viewModel)
        {
            if (ModelState.IsValid) // se os dados forem válidos
            {
                Colaborador colaborador = (Colaborador)_userManager.GetUserAsync(this.User).Result;

                //logger
                addAcaoColaborador(new Acao()
                {
                    TipoAcaoId = 1,  // create
                    Target = "Criar Jato",
                    Detalhes = "O colaborador " + colaborador.Nome + " " + colaborador.Apelido + ", que pertence " +
                                "à companhia de nome " + colaborador.Companhia.Nome + " tentou criar um jato",

                }, colaborador);

                Companhia companhia = (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

                Jato jato = new Jato()
                {
                    Companhia = companhia,
                    Nome = viewModel.Nome,
                    AeroportoId = viewModel.AeroportoId,
                    CompanhiaId = viewModel.CompanhiaId,
                    EmFuncionamento = false,
                    ModeloId = viewModel.ModeloId,
                };

                companhia.ListaJatos.Add(jato);
                _context.Update(companhia);
                _context.SaveChanges();
                ViewData["Success"] = true;
                return RedirectToAction("VerJatos");
            }

            return RedirectToAction("CriarJato");
        }

        private void addAcaoColaborador (Acao acao, Colaborador colaborador)
        {
            colaborador.ListaAcoes.Add(acao);
        }

    }
}