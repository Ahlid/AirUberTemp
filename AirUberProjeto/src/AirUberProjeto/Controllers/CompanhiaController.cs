using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AirUberProjeto.Data;
using AirUberProjeto.Models;
using AirUberProjeto.Models.CompanhiaViewModels;
using AirUberProjeto.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Org.BouncyCastle.Security;

using Newtonsoft.Json;

namespace AirUberProjeto.Controllers
{
    /// <summary>
    /// Classe responsável por receber todos os pedidos do browser e tratar dos mesmos relativamente às companhias
    /// </summary>
    [Authorize(Roles = Roles.ROLE_COLABORADOR_ADMIN + ", " + Roles.ROLE_COLABORADOR)]
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
        /// Server para enviar os emails.
        /// </summary>
        private readonly IEmailSender _emailSender;

        

        /// <summary>
        /// Construtor do controlador Companhia
        /// </summary>
        /// <param name="context">O contexto da aplicação</param>
        /// <param name="userManager">O manager dos utilizadores</param>
        /// <param name="environment">O ambiente da aplicação</param>
        /// <param name="emailSender">O email sender a usar para enviar os emails</param>
        public CompanhiaController(AirUberDbContext context, UserManager<ApplicationUser> userManager,
            IHostingEnvironment environment, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
            _emailSender = emailSender;
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


            Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;

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


            /*
             * Existe um problema com as notificações, porque como nao ficam associadas a uma companhia mas sim a um user.
             * Estas terão que ficar associadas ao colaborador admin que é criado ao mesmo tempo que a companhia.
             * 
             * Logo ou terei que ir buscar o id do colaborador admin e depois ir buscar as notificações que têm o id dele.
             * Ou então é adicionado o campo companhia nas notificacoes, e assim cada user tem as suas notificacoes.
             * 
             * 
             * Actualmente apenas o colaborador admin ve as notifiacções adicionadas
             * 
             */ 


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
            Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;

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
                Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;
                Companhia companhia =
                    (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

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


        /***********************
         *       Jatos         *
         *                     *
         ***********************/

        /// <summary>
        /// Apresenta a página com todos os jatos da companhia 
        /// </summary>
        /// <returns>View para visualizar jatos</returns>
        [HttpGet]
        public IActionResult VerJatos()
        {
            Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;
            Companhia companhia =
                (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

            var jatos = _context.Jato.Select(j => j).Include(j => j.Modelo)
                .Include(j => j.Modelo.TipoJato)
                .Include(j => j.Companhia)
                .Include(j => j.Aeroporto)
                .Where(j => j.CompanhiaId == companhia.CompanhiaId);
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

            if (id != null) //se o id do jato existe, ou seja, se foi selecionado um jato
            {
                Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;

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

                var aeroportos = _context.Aeroporto.Select(a => new {Id = a.AeroportoId, Valor = a.Nome});

                var modelos = (from Modelo in _context.Modelo
                    join TipoJato in _context.TipoJato
                    on Modelo.TipoJatoId equals TipoJato.TipoJatoId
                    select new {Modelo.ModeloId, TipoJato.Nome});

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
                Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;
                Companhia companhia =
                    (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

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

            Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;
            Companhia companhia =
                (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

            var aeroportos = _context.Aeroporto.Select(a => new {Id = a.AeroportoId, Valor = a.Nome});

            //var modelos = _context.Modelo.Select(m => new { Id = m.ModeloId, Valor = m.Descricao });

            // o nome do modelo esta no tipo de jato e é necessário o join, porque estão em tabelas separadas
            var modelos = (from Modelo in _context.Modelo
                join TipoJato in _context.TipoJato
                on Modelo.TipoJatoId equals TipoJato.TipoJatoId
                select new {Modelo.ModeloId, TipoJato.Nome});

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
                Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;

                Companhia companhia =
                    (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

                //logger
                addAcaoColaborador(new Acao()
                {
                    TipoAcaoId = 3, // insert
                    Target = "Criar Jato",
                    Detalhes = "O colaborador " + colaborador.Nome + " " + colaborador.Apelido + ", que pertence " +
                               "à companhia de nome " + companhia.Nome + " tentou criar um jato",

                }, colaborador);

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

        /// <summary>
        /// Apresenta a página para apagar um jato
        /// </summary>
        /// <param name="id">id do jato</param>
        /// <returns>View para apagar jatos</returns>
        [HttpGet]
        public IActionResult ApagarJato(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var jato = _context.Jato.Select(j => j)
                .Include(j => j.Aeroporto)
                .Include(j => j.Companhia)
                .Include(j => j.Modelo)
                .Include(j => j.Modelo.TipoJato)
                .SingleOrDefault(j => j.JatoId == id);
            if (jato == null)
            {
                return NotFound();
            }
            return View(jato);
        }

        /// <summary>
        /// Trata de um pedido de eliminação de um jato 
        /// </summary>
        /// <param name="id">id do jato</param>
        /// <returns>A view de criação de um jato</returns>
        [HttpPost, ActionName("ApagarJato")]
        [ValidateAntiForgeryToken]
        public IActionResult ApagarJatoConfirmacao(int? id)
        {

            Colaborador colaborador = (Colaborador)_userManager.GetUserAsync(this.User).Result;

            Companhia companhia = (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

            var jato = _context.Jato.SingleOrDefault(j => j.JatoId == id);

            bool existe = companhia.ListaJatos.Any(c => c.JatoId == jato.JatoId);    // verifica se a companhia tem aquele aviao

            if (existe)
            {
                _context.Jato.Remove(jato);
                _context.SaveChanges();

                return RedirectToAction("VerJatos");
            }
            return NotFound();
        }


        /***********************
         *    Colaboradores    *
         *                     *
         ***********************/

        /// <summary>
        /// Apresenta a página com todos os colaboradores
        /// </summary>
        /// <returns>View para criar jatos</returns>
        [HttpGet]
        public IActionResult VerColaboradores()
        {
            Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;
            Companhia companhia = (_context.Companhia.Select(c => c)
                .Include(c => c.ListaColaboradores)
                .Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

            return View(companhia.ListaColaboradores);
        }

        /// <summary>
        /// Apresenta a página para criar colaboradores de uma companhia
        /// </summary>
        /// <returns>View para criar colaboradores</returns>
        [HttpGet]
        public IActionResult AdicionarColaborador()
        {
            Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;

            ViewData["CompanhiaId"] = new SelectList(_context.Companhia.Select(c => c)
                    .Where(c => c.CompanhiaId == colaborador.CompanhiaId),
                "CompanhiaId", "Nome");
            return View();
        }

        /// <summary>
        /// Trata de um pedido para adicionar um colaborador
        /// </summary>
        /// <param name="viewModel">ViewModel do pedido de criação de um colaborador</param>
        /// <returns>A view de criação de um colaborador</returns>
        [HttpPost]
        public async Task<IActionResult> AdicionarColaborador(CriarColaboradorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;

                Companhia companhia =
                    (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

                //logger
                addAcaoColaborador(new Acao()
                {
                    TipoAcaoId = 3, // insert
                    Target = "Criar Colaborador",
                    Detalhes = "O colaborador " + colaborador.Nome + " " + colaborador.Apelido + ", que pertence " +
                               "à companhia de nome " + companhia.Nome + " tentou criar um colaborador",

                }, colaborador);


                Colaborador novoColaborador = new Colaborador()
                {
                    Nome = viewModel.PrimeiroNome,
                    Apelido = viewModel.Apelido,
                    Email = viewModel.Email,
                    //CompanhiaId = viewModel.CompanhiaId,  // Se usar o ID é apresentado um erro!
                    Companhia = companhia,
                    IsAdministrador = viewModel.IsAdministrador,
                    UserName = viewModel.Email,
                };

                //criar utilizador colaborador, para se poder autenticar
                var result = await _userManager.CreateAsync(novoColaborador, viewModel.Password);
                if (result.Succeeded)
                {
                    await
                        _userManager.AddToRoleAsync(novoColaborador,
                            novoColaborador.IsAdministrador ? Roles.ROLE_COLABORADOR_ADMIN : Roles.ROLE_COLABORADOR);
                        //atribui a role


                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(novoColaborador);
                    var callbackUrl = Url.Action("ConfirmEmail", "Autenticacao",
                        new {userId = novoColaborador.Id, code = code}, protocol: HttpContext.Request.Scheme);
                    await _emailSender.SendEmailAsync(novoColaborador.Email, "Confirm your account",
                        $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                    //await _signInManager.SignInAsync(novoColaborador, isPersistent: false);//para ele depois fazer login, regista-se e fica logo loged-in
                    //_logger.LogInformation(3, "User created a new account with password.");
                }



                companhia.ListaColaboradores.Add(novoColaborador);

                _context.Update(companhia);
                _context.SaveChanges();
                return RedirectToAction("VerColaboradores");
            }

            return RedirectToAction("AdicionarColaborador");
        }

        /// <summary>
        /// Apresenta a página para editar os dados de um colaborador
        /// </summary>
        /// <param name="id">id do colaborador</param>
        /// <returns>View para visualizar a página de edição de jatos</returns>
        [HttpGet]
        public IActionResult EditarColaborador(string id)
        {
            if (id != null)
            {
                Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;

                Companhia companhia = (_context.Companhia.Select(c => c).Include(c => c.Pais)
                    .Include(c => c.Estado)
                    .Include(c => c.ContaDeCreditos)
                    .Include(c => c.ListaReservas)
                    .Include(c => c.ListaColaboradores)
                    .Include(c => c.ListaJatos)
                    .Include(c => c.ListaExtras)
                    .Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

                Colaborador colaboradorAEditar = (_context.Colaborador.Select(c => c)
                    .Include(c => c.Companhia)
                    .Where(c => c.Id == id)).Single();

                //logger
                addAcaoColaborador(new Acao()
                {
                    TipoAcaoId = 3, // insert
                    Target = "Editar Colaborador",
                    Detalhes = "O colaborador " + colaborador.Nome + " " + colaborador.Apelido + ", que pertence " +
                               "à companhia de nome " + companhia.Nome + " tentou editar o colaborador " +
                               colaboradorAEditar.Nome + " " + colaboradorAEditar.Apelido,

                }, colaborador);

                ViewBag.companhia = companhia.Nome;

                return View(colaboradorAEditar);
            }
            return NotFound();
        }


        /// <summary>
        /// Tratar de um pedido de edição dos dados de um colaborador
        /// </summary>
        /// <param name="viewModel">viewModel do pedido de alteração de dados</param>
        /// <returns>A view de edição de um colaborador</returns>
        [HttpPost]
        public IActionResult EditarColaborador(EditarColaboradorViewModel viewModel)
        {
            if (ModelState.IsValid) // se os dados forem válidos
            {
                Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;
                Companhia companhia =
                    (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

                Colaborador colaboradorAEditar = (_context.Colaborador.Select(c => c)
                    .Include(c => c.Companhia)
                    .Where(c => c.Id == viewModel.Id)).Single();

                colaboradorAEditar.Nome = viewModel.Nome;
                colaboradorAEditar.Apelido = viewModel.Apelido;
                colaboradorAEditar.Email = viewModel.Email;
                colaboradorAEditar.IsAdministrador = viewModel.IsAdministrador;


                /*
                 * Acção - logger
                 * */
                _context.Update(colaboradorAEditar);
                _context.SaveChanges();
                return RedirectToAction("VerColaboradores");
            }

            return RedirectToAction("EditarColaborador");

        }

        /// <summary>
        /// Apresenta a página para apagar um colaborador
        /// </summary>
        /// <param name="id">id do colaborador</param>
        /// <returns>View para apagar colaboradores</returns>
        [HttpGet]
        public IActionResult ApagarColaborador(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colaborador = _context.Colaborador.Select(c => c)
                .Include(c => c.Companhia)
                .SingleOrDefault(c => c.Id == id);
            if (colaborador == null)
            {   
                return NotFound();
            }
            return View(colaborador);
        }


        /// <summary>
        /// Trata de um pedido de eliminação de um colaborador
        /// </summary>
        /// <param name="id">id do colaborador</param>
        /// <returns>A view de eliminação de um colaborador</returns>
        [HttpPost, ActionName("ApagarColaborador")]
        [ValidateAntiForgeryToken]
        public IActionResult ApagarColaboradorConfirmacao(string id)
        {

            Colaborador colaborador = (Colaborador)_userManager.GetUserAsync(this.User).Result;

            Companhia companhia = (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

            var colaboradorAEliminar = _context.Colaborador.SingleOrDefault(j => j.Id == id);

            bool existe = companhia.ListaColaboradores.Any(c => c.Id == colaboradorAEliminar.Id);    // verifica se o colaborador a eliminar faz parte da companhia logada

            if (existe)
            {
                _context.Colaborador.Remove(colaboradorAEliminar);
                _context.SaveChanges();

                return RedirectToAction("VerColaboradores");
            }
            return NotFound();

        }


        /***********************
         *       Extras        *
         *                     *
         ***********************/

        /// <summary>
        /// Apresenta a página para ver todos os extras de uma companhia
        /// </summary>
        /// <returns>View para ver extras</returns>
        [HttpGet]
        public IActionResult VerExtras()
        {

            Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;

            Companhia companhia = (_context.Companhia.Select(c => c)
                .Include(c => c.ListaExtras)
                .Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

            var extras = _context.Extra.Select(j => j)
                .Include(j => j.Companhia)
                .Include(j => j.TipoExtra)
                .Where(j => j.CompanhiaId == companhia.CompanhiaId);

            return View(extras);
        }

        /// <summary>
        /// Apresenta a página para criar extras para um companhia
        /// </summary>
        /// <returns>View para criar extras</returns>
        [HttpGet]
        public IActionResult CriarExtra()
        {
            Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;

            Companhia companhia = (_context.Companhia.Select(c => c)
                .Include(c => c.ListaExtras)
                .Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();


            ViewData["CompanhiaId"] =
                new SelectList(_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == companhia.CompanhiaId),
                    "CompanhiaId", "Nome");
            ViewData["TipoExtraId"] = new SelectList(_context.TipoExtra, "TipoExtraId", "Nome");
            return View();
        }

        /*
         * 
         * 
         * 
         * 
         * Problema com valores decimais com '.'
         * 
         *  
         * 
         * 
         * 
         * 
         */


        /// <summary>
        /// Trata de um pedido de criação de um extra
        /// </summary>
        /// <param name="viewModel">viewModel do pedido de criação de um extra</param>
        /// <returns>A view de criação de um extra</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CriarExtra(CriarExtraViewModel viewModel)
        {

            if (ModelState.IsValid)
            {

                // é recebido o objecto da classe view model
                Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;

                Companhia companhia =
                    (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();


                Extra extra = new Extra()
                {
                    CompanhiaId = viewModel.CompanhiaId,
                    Nome = viewModel.Nome,
                    TipoExtraId = viewModel.TipoExtraId,
                    Valor = Convert.ToDecimal(viewModel.Valor)
                    
                };


                companhia.ListaExtras.Add(extra);
                _context.Update(companhia);
                _context.SaveChanges();

                return RedirectToAction("VerExtras");
            }
            ViewData["CompanhiaId"] = new SelectList(_context.Companhia, "CompanhiaId", "Contact", viewModel.CompanhiaId);
            ViewData["TipoExtraId"] = new SelectList(_context.TipoExtra, "TipoExtraId", "Nome", viewModel.TipoExtraId);
            return RedirectToAction("CriarExtra");
        }


        /// <summary>
        /// Apresenta a página para editar extras de uma companhia
        /// </summary>
        /// <param name="id">id extra</param>
        /// <returns>View para editar extras colaboradores</returns>
        [HttpGet]
        public IActionResult EditarExtra(int id)
        {
            var extra = _context.Extra.Single(e => e.ExtraId == id);
            if (extra != null)
            {

                Colaborador colaborador = (Colaborador) _userManager.GetUserAsync(this.User).Result;

                Companhia companhia = (_context.Companhia.Select(c => c)
                    .Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

                ViewBag.companhia = companhia.Nome;
                ViewBag.tipos = new SelectList(_context.TipoExtra, "TipoExtraId", "Nome");

                return View(extra);
            }

            return NotFound();
        }


        /// <summary>
        /// Trata de um pedido de alteração de dados de um extra
        /// </summary>
        /// <param name="viewModel">viewModel do pedido de alteração de dados</param>
        /// <returns>A view de edição de extras</returns>
        [HttpPost]
        public IActionResult EditarExtra(EditarExtraViewModel viewModel)
        {
            if (ModelState.IsValid) // se os dados forem válidos
            {
                //Colaborador colaborador = (Colaborador)_userManager.GetUserAsync(this.User).Result;
                //Companhia companhia = (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

                Extra extraAEditar = (_context.Extra.Select(c => c).Where(c => c.ExtraId == viewModel.ExtraId)).Single();

                extraAEditar.Nome = viewModel.Nome;
                extraAEditar.TipoExtraId = viewModel.TipoExtraId;
                extraAEditar.Valor = viewModel.Valor;

                /*
                 * Acção - logger
                 * */

                _context.Update(extraAEditar);
                _context.SaveChanges();
                return RedirectToAction("VerExtras");
            }

            return RedirectToAction("EditarExtra");

        }

        /// <summary>
        /// Apresenta a página para apagar um extra
        /// </summary>
        /// <param name="id">id do extra</param>
        /// <returns>View para apagar um extra</returns>
        [HttpGet]
        public IActionResult ApagarExtra(int id)
        {

            var extra = _context.Extra.Include(e => e.Companhia)
                .Include(e => e.TipoExtra)
                .Single(e => e.ExtraId == id);

            if (extra == null)
            {
                return NotFound();
            }

            ViewData["TipoExtraId"] = new SelectList(_context.TipoExtra, "TipoExtraId", "Nome");
            return View(extra);
        }

 
        /// <summary>
        /// Trata de um pedido de eliminação de um extra
        /// </summary>
        /// <param name="id">id do extra</param>
        /// <returns>A view de eliminação de um extra</returns>
        [HttpPost, ActionName("ApagarExtra")]
        [ValidateAntiForgeryToken]
        public IActionResult ApagarExtraConfirmacao(int id)
        {

            Colaborador colaborador = (Colaborador)_userManager.GetUserAsync(this.User).Result;

            Companhia companhia = (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

            var extra = _context.Extra.Single(e => e.ExtraId == id);

            bool existe = companhia.ListaExtras.Any(c => c.ExtraId == extra.ExtraId);    // verifica se o colaborador a eliminar faz parte da companhia logada

            if (existe)
            {
                _context.Extra.Remove(extra);
                _context.SaveChanges();

                return RedirectToAction("VerExtras");
            }
            return NotFound();


            /*
            var extra = _context.Extra.Single(e => e.ExtraId == id);

            _context.Extra.Remove(extra);
            _context.SaveChanges();

            return RedirectToAction("VerExtras");
            */
        }


        /***********************
         *       Modelos       *
         *                     *
         ***********************/

        /// <summary>
        /// Apresenta a página para com todos os modelos de aviões
        /// </summary>
        /// <returns>View para ver modelos</returns>
        [HttpGet]
        public IActionResult VerModelos()
        {
            var modelos = _context.Modelo.Select(m => m)
                .Include(m => m.TipoJato);


            return View(modelos);
        }

        /// <summary>
        /// Apresenta a página para adicionar um modelo
        /// </summary>
        /// <returns>View para adicionar modelo</returns>
        [HttpGet]
        public IActionResult AdicionarModelo()
        {

            ViewBag.tipos = new SelectList(_context.TipoJato, "TipoJatoId", "Nome");
            return View();
        }


        /// <summary>
        /// Trata de um pedido de criação de um modelo 
        /// </summary>
        /// <param name="viewModel">viewModel do pedido de criação de um modelo</param>
        /// <returns>A view de criação de um modelo</returns>
        [HttpPost]
        public IActionResult AdicionarModelo(CriarModeloViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                Modelo modelo = new Modelo()
                {
                    Capacidade = viewModel.Capacidade,
                    Alcance = viewModel.Alcance,
                    VelocidadeMaxima = viewModel.VelocidadeMaxima,
                    PesoMaximaBagagens = viewModel.PesoMaximaBagagens,
                    NumeroMotores = viewModel.NumeroMotores,
                    AltitudeIdeal = viewModel.AltitudeIdeal,
                    AlturaCabine = viewModel.AlturaCabine,
                    LarguraCabine = viewModel.LarguraCabine,
                    ComprimentoCabine = viewModel.ComprimentoCabine,
                    Descricao = viewModel.Descricao,

                    TipoJatoId = viewModel.TipoJatoId


                };


                _context.Add(modelo);
                _context.SaveChanges();

                return RedirectToAction("VerModelos");
            }
            ViewBag.tipos = new SelectList(_context.TipoJato, "TipoJatoId", "Nome");
            return RedirectToAction("AdicionarModelo");
        }


        /// <summary>
        /// Permite alterar a imagem de perfil da companhia
        /// </summary>
        /// <param name="file">ficheiro de imagem que irá substituir a imagem de perfil</param>
        /// <returns>Um redirecionamento para a ação editar perfil</returns>
        [HttpPost]
        public async Task<IActionResult> AlterarImagemPerfil(IFormFile file)
        {
            if (file == null)
            {
                return RedirectToAction(nameof(CompanhiaController.EditarPerfilCompanhia), "Companhia");
            }


            string extension = Path.GetExtension(file.FileName);
            if (extension != ".jpg" && extension != ".png")
                return null;

            Colaborador colaborador = (Colaborador)_userManager.GetUserAsync(this.User).Result;
            Companhia companhia = (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();


            string fileName = "imagem-perfil" + extension;
            string folderName = Path.Combine("companhias", "company-" + companhia.Email);
            string relativePathToFile = Path.Combine(folderName, fileName);

            string forderPath = Path.Combine(_environment.WebRootPath, folderName);
            string absolutePathToFile = Path.Combine(_environment.WebRootPath, relativePathToFile);

            //Cria a directoria caso ainda não exista
            Directory.CreateDirectory(forderPath);

            //Transmite o ficheiro através de um FileStream
            using (var fileStream = new FileStream(absolutePathToFile, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            companhia.RelativePathImagemPerfil = relativePathToFile;
            _context.Update(companhia);
            _context.SaveChanges();

            return RedirectToAction(nameof(CompanhiaController.EditarPerfilCompanhia), "Companhia");
        }


        /// <summary>
        /// Permite alterar a imagem do jato
        /// </summary>
        /// <param name="file">ficheiro de imagem que irá substituir a imagem do jato</param>
        /// <param name="id">id do jato a que a se irá alterar a foto</param>
        /// <returns>Um redirecionamento para a ação editar jato</returns>
        [HttpPost]
        public async Task<IActionResult> AlterarImagemJato(IFormFile file, int id)
        {
            if (file == null)
            {
                // permite ficar na mesma página, para alterar a imagem, pq não foi alterada
                return RedirectToAction("EditarJatos", new { id = id });    
            }


            string extension = Path.GetExtension(file.FileName);
            if (extension != ".jpg" && extension != ".png")
                return null;


            Colaborador colaborador = (Colaborador)_userManager.GetUserAsync(this.User).Result;
            Companhia companhia = (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();

            Jato jato = (_context.Jato.Select(c => c).Include(c => c.Aeroporto)
                                                     .Include(c => c.Companhia)
                                                     .Include(c => c.Modelo)
                                                     .Where(c => c.JatoId == id))
                                                     .Single();


            string fileName = "imagem-jato-" + jato.Nome + extension;
            //string folderName = Path.Combine("jatos", "airplaine-" + jato.Nome);
            string folderName = Path.Combine("companhias", "company-" + companhia.Email);
            string relativePathToFile = Path.Combine(folderName, fileName);

            string forderPath = Path.Combine(_environment.WebRootPath, folderName);
            string absolutePathToFile = Path.Combine(_environment.WebRootPath, relativePathToFile);

            //Cria a directoria caso ainda não exista
            Directory.CreateDirectory(forderPath);

            //Transmite o ficheiro através de um FileStream
            using (var fileStream = new FileStream(absolutePathToFile, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            jato.RelativePathImagemPerfil = relativePathToFile;
            _context.Update(jato);
            _context.SaveChanges();

            return RedirectToAction(nameof(CompanhiaController.VerJatos), "Companhia"); 
        }
        


        /// <summary>
        /// Adiciona à lista de acções de um colaborador uma acção realizada por ele 
        /// </summary>
        /// <param name="acao">acao</param>
        /// <param name="colaborador">colaborador</param>
        private void addAcaoColaborador (Acao acao, Colaborador colaborador)

        {
            colaborador.ListaAcoes.Add(acao);
        }


        /// <summary>
        /// Retorna uma string json com toda informação à cerca da disponibilidade de um jato.
        /// </summary>
        /// <param name="jatoId">id do jato</param>
        /// <returns>string json</returns>
        [HttpGet]
        public string VerDisponibilidade(int jatoId)
        {

            Jato jato = _context.Jato
                .Where(j => j.JatoId == jatoId)
                .Include(j => j.ListaDisponibilidade)
                .Single();



            string json = "";

            foreach (Disponibilidade d in jato.ListaDisponibilidade)
            {
                if (!json.Equals(""))
                {
                    json += ",";
                }
                var k =
                    new
                    {
                        idJato = jatoId,
                        idDisp = d.DisponibilidadeId,
                        start = d.Inicio,
                        end = d.Fim,
                        title = "disponibilidade"
                    };


                json += JsonConvert.SerializeObject(k);

            }



            return "[" + json + "]";
        }

        /// <summary>
        /// Indica um intervalo de tempo que o jato estará disponível.
        /// </summary>
        /// <param name="IdJato">id jato</param>
        /// <param name="Inicio">data início da disponibilidade</param>
        /// <param name="Fim">data fim da disponibilidade</param>
        /// <returns>Retorna no final o id mais a data de inicio da disponibilidade</returns>
        [HttpPost]
        public string AdicionarDisponibilidade(int IdJato, string Inicio, string Fim)

        {

            Jato jato = _context.Jato
                .Where(j => j.JatoId == IdJato)
                .Include(j => j.ListaDisponibilidade)
                .Single();


            Disponibilidade d = new Disponibilidade {Inicio = Inicio, Fim = Fim};
            jato.ListaDisponibilidade.Add(d);
            _context.Update(jato);
            _context.SaveChanges();


            return IdJato + " " + Inicio;
        }

        /// <summary>
        /// Apaga um intervalo de tempo que um jato estava disponível
        /// </summary>
        /// <param name="idJato">id do jato</param>
        /// <param name="idDisp">id da disponibilidade</param>
        /// <returns>Retorna true se o jato já estaria disponível naquele intervalo de tempo, se não retorna false</returns>
        [HttpPost]
        public bool ApagarDisponibilidade(int idJato, int idDisp)
        {

            Jato jato = _context.Jato
                .Where(j => j.JatoId == idJato)
                .Include(j => j.ListaDisponibilidade)
                .Single();





            foreach (Disponibilidade d in jato.ListaDisponibilidade)
            {
                if (d.DisponibilidadeId == idDisp)
                {
                    jato.ListaDisponibilidade.Remove(d);
                    _context.Update(jato);
                    _context.SaveChanges();
                    return true;
                }
            }



            return false;
        }

        /// <summary>
        /// Modifica um intervalo de tempo em que o jato está disponível
        /// </summary>
        /// <param name="Inicio">nova data início</param>
        /// <param name="Fim">nova data fim</param>
        /// <param name="idDisp">id disponibilidade</param>
        /// <param name="jatoId">id jato</param>
        /// <returns>Retorna true se a disponibilidade for modificada, se não retorna false</returns>
        [HttpPost]
        public bool EditarDisponibilidade(string Inicio, string Fim, int idDisp, int jatoId)
        {


            Jato jato = _context.Jato
                .Where(j => j.JatoId == jatoId)
                .Include(j => j.ListaDisponibilidade)
                .Single();





            foreach (Disponibilidade d in jato.ListaDisponibilidade)
            {
                if (d.DisponibilidadeId == idDisp)
                {
                    d.Fim = Fim;
                    d.Inicio = Inicio;
                    _context.Update(d);
                    _context.SaveChanges();
                    return true;
                }
            }



            return false;

        }


        /// <summary>
        /// Permite marcar uma notificação como lida através do seu ID
        /// </summary>
        /// <param name="id">Id da notificação ao qual se quer marcar como lida</param>
        /// <returns>Variavel booleana que indica se houve sucesso a marcar a notificação como lida</returns>
        [HttpPost]
        public bool MarcarNotificacaoLida(int id)
        {
            try
            {
                Notificacao notificacao = _context.Notificacao.First(n => n.NotificacaoId == id);
                notificacao.Lida = true;
                _context.Notificacao.Update(notificacao);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        //Viagens

        /// <summary>
        /// Responsável por redireccionar o utilizador para a página que apresenta a informação de todas as viagens feitas na companhia.
        /// </summary>
        /// <returns>Retorna a view das viagens</returns>
        public IActionResult VerViagens()
        {
            Colaborador colaborador = (Colaborador)_userManager.GetUserAsync(this.User).Result;
            Companhia companhia = (_context.Companhia.Select(c => c).Where(c => c.CompanhiaId == colaborador.CompanhiaId)).Single();


            var viagens = _context.Reserva.Select(c => c)
                                          .Include(a => a.AeroportoDestino)
                                          .Include(a => a.AeroportoPartida)
                                          .Include(a => a.Cliente)
                                          .Include(a => a.Jato)
                                          .Include(a => a.Jato.Companhia)
                                          .Include(r => r.ListaExtras)
                                          .Where(c => c.Jato.Companhia.CompanhiaId == companhia.CompanhiaId).ToList();
            return View(viagens);
        }



    }
}