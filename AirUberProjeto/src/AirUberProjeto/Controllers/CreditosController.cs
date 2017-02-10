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

namespace AirUberProjeto.Controllers
{

    /// <summary>
    /// Controlador responsável pela gestão de creditos
    /// </summary>
    [Authorize(Roles = Roles.ROLE_CLIENTE)]
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
        public IActionResult Index()
        {
            return RedirectToAction("Comprar");
        }



        /// <summary>
        /// A acção comprar para poder comprar creditos
        /// </summary>
        /// <returns>Retorna a view Comprar</returns>
        [HttpGet]
        public IActionResult Comprar()
        {
            return View();
        }



    }
}
