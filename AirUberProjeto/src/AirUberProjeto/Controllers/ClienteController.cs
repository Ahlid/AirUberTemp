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

    [Authorize(Roles = Roles.ROLE_CLIENTE)]
    public class ClienteController : Controller
    {
        
        /// <summary>
        /// Utilizado para sabermos o caminho absoluto do servidor
        /// </summary>
        private IHostingEnvironment _environment;

        /// <summary>
        /// O contexto da aplicação para poder aceder a dados.
        /// </summary>
        private readonly AirUberDbContext _context;

        /// <summary>
        /// User manager que vai permitir utilizar metodos feitos pela windows de forma a controlar os user.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;


        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        /// <param name="environment"></param>
        public ClienteController(AirUberDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        { 
            _environment = environment;
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Perfil");
        }

         

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Perfil()
        {
            string idCliente = _userManager.GetUserAsync(this.User).Result.Id;

            Cliente cliente = (_context.Cliente.Include(c => c.ContaDeCreditos).Where(c => c.Id == idCliente).Select(c => c)).Single() ;
            
            PerfilViewModel viewModel = new PerfilViewModel() {Cliente = cliente};

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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditarPerfil()
        {
            Cliente cliente = (Cliente)_userManager.GetUserAsync(this.User).Result;

            return View(cliente);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditarImagemPerfil (IFormFile file)
        {

            string extension = Path.GetExtension(file.FileName);
            if (extension != ".jpg" && extension != ".png")
                return null;

            Cliente cliente = (Cliente)_userManager.GetUserAsync(this.User).Result;

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

    }
}
