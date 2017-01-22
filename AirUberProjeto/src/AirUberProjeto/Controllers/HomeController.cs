using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AirUberProjeto.Controllers
{
    /// <summary>
    /// Controlador acções por omissão
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Acção por defeito do home controller
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Apresenta a página do about
        /// </summary>
        /// <returns>Retorna a view da página about da home</returns>
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Apresenta a página dos contactos
        /// </summary>
        /// <returns>Retorna a view da página contact do home</returns>
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Apresenta a página de erros
        /// </summary>
        /// <returns>Retorna a view da página erro do home</returns>
        public IActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// Apresenta a página que corresponde a um utilizador de helpdesk após a autenticação
        /// </summary>
        /// <returns>Retorna a view da página do login helpdesk</returns>
        public string HelpdeskLogin()
        {
            return "helpdesk";
        }

        /// <summary>
        /// Apresenta a página que corresponde a um utilizador de uma companhia após a autenticação
        /// </summary>
        /// <returns>Retorna a view da página do login do colaborador</returns>
        public string ColaboradorLogin()
        {
            return "colaborador";
        }

        /// <summary>
        /// Apresenta a página que corresponde a um cliente após a autenticação
        /// </summary>
        /// <returns>Retorna a view da página do login do cliente</returns>
        public string ClienteLogin()
        {
            return "cliente";
        }

    }
}
