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
    /// Controlador respons�vel pela gest�o de clientes
    /// </summary>
    [Authorize(Roles = Roles.ROLE_CLIENTE)]
    public class ClienteController : Controller
    {
        
        /// <summary>
        /// Utilizado para sabermos o caminho absoluto da pasta wwwRoot
        /// </summary>
        private readonly IHostingEnvironment _environment;

        /// <summary>
        /// O contexto da aplica��o para poder aceder a dados.
        /// </summary>
        private readonly AirUberDbContext _context;

        /// <summary>
        /// User manager que vai permitir utilizar metodos feitos pela windows de forma a controlar os user.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;


        
        /// <summary>
        /// Construtor do controlador
        /// </summary>
        /// <param name="context">O DB context da aplica��o</param>
        /// <param name="userManager">O manager dos utilizadores</param>
        /// <param name="environment">O ambiente da aplica��o</param>
        public ClienteController(AirUberDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        { 
            _environment = environment;
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// A ac��o index � a a��o default do controlador e redireciona para a a��o Perfil
        /// </summary>
        /// <returns>Retorna a view retornada pela ac��o Perfil</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Perfil");
        }

         

        /// <summary>
        /// Apresenta a p�gina de perfil do cliente tendo em conta o utilizador atual
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
                                    .Single() ;

            PerfilViewModel viewModel = new PerfilViewModel()
            {
                Cliente = cliente,
                NumeroViagens = cliente.ListaReservas.Count
            };



            List<Notificacao> notificacoes =  _context.Notificacao.Where((n) =>
                n.UtilizadorId == cliente.Id
            ).ToList();

            foreach (Notificacao notificacao in notificacoes)
            {
                viewModel.Notificacoes.Add(notificacao);
            }


            return View(viewModel);
        }



        /// <summary>
        /// Permite marcar uma notifica��o como lida atrav�s do seu ID
        /// </summary>
        /// <param name="id">Id da notifica��o ao qual se quer marcar como lida</param>
        /// <returns>Variavel booleana que indica se houve sucesso a marcar a notifica��o como lida</returns>
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
        /// Apresenta a p�gina de edi��o de perfil do cliente tendo em conta o utilizador atual
        /// </summary>
        /// <returns>A View de edi��o do perfil do cliente</returns>
        [HttpGet]
        public IActionResult EditarPerfil()
        {
            Cliente cliente = (Cliente)_userManager.GetUserAsync(this.User).Result;

            return View(cliente);
        }


        /// <summary>
        /// Trata de um pedido de altera��o de dados de um cliente
        /// </summary>
        /// <param name="viewModel">ViewModel do pedido de altera��o de dados</param>
        /// <returns>A view de edi��o de perfil do cliente</returns>
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
        /// <param name="file">ficheiro de imagem que ir� substituir a imagem de perfil</param>
        /// <returns>Um redirecionamento para a a��o editar perfil</returns>
        [HttpPost]
        public async Task<IActionResult> AlterarImagemPerfil (IFormFile file)
        {
            if (file == null)
            {
                return RedirectToAction(nameof(ClienteController.EditarPerfil), "Cliente");
            }


            string extension = Path.GetExtension(file.FileName);
            if (extension != ".jpg" && extension != ".png")
                return null;

            Cliente cliente = (Cliente)_userManager.GetUserAsync(this.User).Result;

            string fileName = "imagem-perfil" + extension;
            string folderName = Path.Combine("clientes", "client-" + cliente.Email);
            string relativePathToFile = Path.Combine(folderName, fileName);

            string forderPath = Path.Combine(_environment.WebRootPath, folderName);
            string absolutePathToFile = Path.Combine(_environment.WebRootPath, relativePathToFile);

            //Cria a directoria caso ainda n�o exista
            Directory.CreateDirectory(forderPath);
            
            //Transmite o ficheiro atrav�s de um FileStream
            using (var fileStream = new FileStream(absolutePathToFile, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            cliente.RelativePathImagemPerfil = relativePathToFile;
            _context.Update(cliente);
            _context.SaveChanges();

            return RedirectToAction(nameof(ClienteController.EditarPerfil), "Cliente");
        }

    }
}
