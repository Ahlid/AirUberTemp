using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirUberProjeto.Data;
using AirUberProjeto.Models;
using AirUberProjeto.Models.ClienteViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AirUberProjeto.Controllers
{

    /// <summary>
    /// Controlador responsável pela gestão de clientes
    /// </summary>
    [Authorize(Roles = Roles.ROLE_CLIENTE)]
    public class ClienteController : Controller
    {

        /// <summary>
        /// Utilizado para sabermos o caminho absoluto da pasta wwwRoot
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
        /// Construtor do controlador
        /// </summary>
        /// <param name="context">O DB context da aplicação</param>
        /// <param name="userManager">O manager dos utilizadores</param>
        /// <param name="environment">O ambiente da aplicação</param>
        public ClienteController(AirUberDbContext context, UserManager<ApplicationUser> userManager,
            IHostingEnvironment environment)
        {
            _environment = environment;
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// A acção index é a ação default do controlador e redireciona para a ação Perfil
        /// </summary>
        /// <returns>Retorna a view retornada pela acção Perfil</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Perfil");
        }



        /// <summary>
        /// Apresenta a página de perfil do cliente tendo em conta o utilizador atual
        /// </summary>
        /// <returns>A view do perfil do cliente</returns>
        [HttpGet]
        public IActionResult Perfil()
        {
            string idCliente = _userManager.GetUserAsync(this.User).Result.Id;

            Cliente cliente = (_context.Cliente
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


            return View(viewModel);
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


        /// <summary>
        /// Apresenta a página de edição de perfil do cliente tendo em conta o utilizador atual
        /// </summary>
        /// <returns>A View de edição do perfil do cliente</returns>
        [HttpGet]
        public IActionResult EditarPerfil()
        {
            Cliente cliente = (Cliente) _userManager.GetUserAsync(this.User).Result;

            return View(cliente);
        }


        /// <summary>
        /// Trata de um pedido de alteração de dados de um cliente
        /// </summary>
        /// <param name="viewModel">ViewModel do pedido de alteração de dados</param>
        /// <returns>A view de edição de perfil do cliente</returns>
        [HttpPost]
        public IActionResult EditarPerfil(EditarPerfilViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = (Cliente) _userManager.GetUserAsync(this.User).Result;
                cliente.Nome = viewModel.Nome;
                cliente.Apelido = viewModel.Apelido;
                cliente.Contacto = viewModel.Contacto;
                _context.Update(cliente);
                _context.SaveChanges();
                ViewData["Success"] = true;
                return View(cliente);
            }

            return RedirectToAction(nameof(ClienteController.EditarPerfil), "Cliente");

        }


        /// <summary>
        /// Permite alterar a imagem de perfil do cliente
        /// </summary>
        /// <param name="file">ficheiro de imagem que irá substituir a imagem de perfil</param>
        /// <returns>Um redirecionamento para a ação editar perfil</returns>
        [HttpPost]
        public async Task<IActionResult> AlterarImagemPerfil(IFormFile file)
        {
            if (file == null)
            {
                return RedirectToAction(nameof(ClienteController.EditarPerfil), "Cliente");
            }


            string extension = Path.GetExtension(file.FileName);
            if (extension != ".jpg" && extension != ".png")
                return null;

            Cliente cliente = (Cliente) _userManager.GetUserAsync(this.User).Result;

            string fileName = "imagem-perfil" + extension;
            string folderName = Path.Combine("clientes", "client-" + cliente.Email);
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

            cliente.RelativePathImagemPerfil = relativePathToFile;
            _context.Update(cliente);
            _context.SaveChanges();

            return RedirectToAction(nameof(ClienteController.EditarPerfil), "Cliente");
        }


        //Viagens

        /// <summary>
        /// Responsável por redireccionar o utilizador para a página que apresenta a informação de todas as viagens feitas na companhia.
        /// </summary>
        /// <returns>Retorna a view das viagens</returns>
        public IActionResult VerViagens()
        {

            Cliente cliente = (Cliente) _userManager.GetUserAsync(this.User).Result;


            var viagens = _context.Reserva.Select(c => c)
                .Include(a => a.AeroportoDestino)
                .Include(a => a.AeroportoPartida)
                .Include(a => a.Cliente)
                .Include(a => a.Jato)
                .Include(a => a.Jato.Companhia)
                .Include(r => r.ListaExtras)
                .Where(c => c.Cliente.Id == cliente.Id).ToList();
            return View(viagens);
        }

        /// <summary>
        /// Responsável por redireccionar o utilizador para a página que apresenta a informação de uma oferta
        /// </summary>
        /// <returns>Retorna a view das ofertas</returns>
        public IActionResult VerOferta()
        {




            return View();

        }


        /// <summary>
        /// Responsável por redireccionar o utilizador para a página de pesquisa de ofertas
        /// </summary>
        /// <returns>Retorna a view da procura de ofertas</returns>
        public IActionResult ProcurarOfertas()
        {
            //TODO: filtrar pelas disponíbilidades dos jatos que estão nas localizações

            var aeroportos = _context.Jato.Select(c => c.Aeroporto).ToList();
            return View(aeroportos);

        }

        /// <summary>
        /// Responsável por redireccionar o utilizador para a página de pesquisa de ofertas
        /// </summary>
        /// <returns>Retorna a view da procura de ofertas</returns>
        [HttpPost]
        public IActionResult ProcurarOfertas(int id)
        {
            //TODO: verificar se o o id existe, redirecionar para a lista de jatos disponíveis.


            var viagens = _context.Aeroporto.Select(c => c).ToList();
            return View(viagens);

        }



    }
}
