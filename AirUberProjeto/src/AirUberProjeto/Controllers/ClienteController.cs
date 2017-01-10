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

        private IHostingEnvironment _environment;



        /// <summary>
        /// O contexto da aplicação para poder aceder a dados.
        /// </summary>
        private readonly AirUberDbContext _context;

        /// <summary>
        /// User manager que vai permitir utilizar metodos feitos pela windows de forma a controlar os user.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;


        
        public ClienteController(AirUberDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        { 
            _environment = environment;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Perfil");
        }

         

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

        [HttpGet]
        public IActionResult EditarPerfil()
        {
            Cliente cliente = (Cliente)_userManager.GetUserAsync(this.User).Result;

            return View(cliente);
        }

        [HttpPost]
        public IActionResult EditarPerfil(EditarPerfilViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = (Cliente)_userManager.GetUserAsync(this.User).Result;
                cliente.Nome = viewModel.Nome;
                cliente.Apelido = viewModel.Apelido;
                cliente.Contacto = viewModel.Contacto;
                _context.Update(cliente);
                _context.SaveChanges();
            }
            

            return RedirectToAction(nameof(ClienteController.EditarPerfil), "Cliente");
        }


        [HttpPost]
        public async Task<IActionResult> EditarImagemPerfil (IFormFile file)
        {
            Cliente cliente = (Cliente)_userManager.GetUserAsync(this.User).Result;
            string dirPath = Path.Combine(_environment.WebRootPath, "clientes", "client-" + cliente.Email);
            string path = Path.Combine(dirPath, "imagem-perfil" + Path.GetExtension(file.FileName));

            Directory.CreateDirectory(dirPath);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            cliente.RelativePathImagemPerfil = Path.Combine("clientes", "client-" + cliente.Email, "imagem-perfil" + Path.GetExtension(file.FileName));
            _context.Update(cliente);
            _context.SaveChanges();

            return RedirectToAction(nameof(ClienteController.EditarPerfil), "Cliente");
        }

    }
}
