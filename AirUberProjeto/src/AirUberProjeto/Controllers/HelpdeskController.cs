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

            //var clientes = _context.ApplicationUser.Select(p => p);
            var clientes = _context.Cliente.Select(p => p);

            // Contar aqui depois o n� de viagens de cada cliente e passar uma lista com todos os valores?
            return View(clientes);
        }

        public IActionResult Companhias()
        {
           /* List<Companhia> listaCompanhias = new List<Companhia>();
            Companhia tap = new Companhia
            {
                Nome = "TAP",
                Contact = "+351 ...",
                PaisId = 1,
                Nif = "8712373261287",
                JetCashAtual = 1000000,
                DataCriacao = DateTime.Now,
                Activada = true,
                Email = "tap@airuber.com"
            };

            Companhia rayner = new Companhia
            {
                Nome = "Rayner",
                Contact = "+000 ...",
                PaisId = 2,
                Nif = "123132231",
                JetCashAtual = 2000000,
                DataCriacao = DateTime.Now,
                Activada = false,
                Email = "rayner@airuber.com"
            };
            listaCompanhias.Add(tap);
            listaCompanhias.Add(rayner);

            var queryable = listaCompanhias.AsQueryable();
            
            List<Pais> listaPaises = new List<Pais>();

            foreach (var b in _context.Pais)
            {
                listaPaises.Add(b);
            }

            ViewBag.ListaPaises = listaPaises;

    
            // NOT WORKING -> 
            _context.Companhia.Add(tap);
            _context.Companhia.Add(rayner);

    */
            //TODO arranjar maneira de distinguir companhias validas e por validar  -> Ver o MR
            var companhias = _context.Companhia.Select(c => c).Include(p => p.Pais);
            //not working -> companhias  -> N�o h� a tabela Companhia!!!!

            // @Html.DisplayFor(modelItem => item.Pais.Nome)  -> N�o est� a funcionar porque o _context.Companhia n�o tem nada!!!
            return View(companhias);
        }

        public IActionResult Viagens()
        {
            //ViewBag.Title = "Viagens";
            //TODO fazer este metodo
            var viagens = _context.Reserva.Select(c => c);

            return View(viagens);
        }


    }

    

}