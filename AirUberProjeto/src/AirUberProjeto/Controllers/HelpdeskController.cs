using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirUberProjeto.Data;
using AirUberProjeto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirUberProjeto.Controllers
{
    
    [Authorize(Roles = Roles.ROLE_HELPDESK)]
    public class HelpdeskController : Controller
    {
        private readonly AirUberDbContext _context;

        public HelpdeskController(AirUberDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Clientes()
        {

            var clientes = _context.ApplicationUser.Select(p => p);

            return View(clientes);
        }

        public IActionResult Companhias()
        {
            //TODO arranjar maneira de distinguir companhias validas e por validar
            var companhias = _context.Companhia.Select(c => c).Include(p => p.Pais);
            return View(companhias);
        }

        public void Viagens()
        {
            //TODO fazer este metodo
        }


    }

    

}