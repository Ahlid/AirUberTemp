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
            // Informação Companhia
            ViewBag.NumeroCompanhiasAceites = _context.Companhia.Select(p => p).Count();
            ViewBag.NumeroCompanhiasPendentes = _context.Companhia.Select(c => c).Where(p => p.EstadoId == 2).Count();

            // Informação Clientes
            ViewBag.NumeroTotalClientes = _context.Cliente.Select(p => p).Count();
            ViewBag.NumeroClientesRegistadosHoje = _context.Cliente.Select(p => p).Where(c => c.DataCriacao.Date == DateTime.Now.Date).Count();

            //Informação Viagens
            ViewBag.NumeroTotalViagens = _context.Reserva.Select(p => p).Count();
            //ViewBag.CreditosMovidos = _context.Reserva.Select(p => new { Custo = p.Sum(s => s.Custo)});
            var creditosMovidos = from p in _context.Reserva group p by 1 into g select new { Custo = g.Sum(s => s.Custo) };

            //ugly
            decimal val = 0.0m;
            foreach(var c in creditosMovidos)
            {
                val = c.Custo;      // valor alto - sai fora do circulo ja existente
            }
            ViewBag.CreditosMovidos = val;

            return View();
        }



        public IActionResult Clientes()
        {

            var clientes = _context.Cliente.Select(p => p).Include(r => r.ListaReservas);   // Este r.ListaReservas carrega a lista de reservas. Necessário fazê-lo sempre que existe uma coleção!
            return View(clientes);
        }

        //working
        /* public IActionResult Companhias()
         {

             //TODO arranjar maneira de distinguir companhias validas e por validar  -> Ver o MR
             var companhias = _context.Companhia.Select(c => c).Include(p => p.Pais).Include(r => r.ListaReservas);

             var companhias_pendentes = _context.Companhia.Select(c => c).Include(p => p.Pais).Include(r => r.ListaReservas).Where(p => p.EstadoId == 2); // EstadoId = 2 => Pendente
             var companhias_aceites = _context.Companhia.Select(c => c).Include(p => p.Pais).Include(r => r.ListaReservas).Where(p => p.EstadoId == 1); // EstadoId = 1 => Aceite

             ViewBag.CompanhiasPendentes = companhias_pendentes;
             ViewBag.CompanhiasAceites = companhias_aceites;


             //return View(companhias);
             return View();
         }*/


//Se fizer algo do género -> actualizações são feitas! http://localhost:43636/Helpdesk/Companhias?id=3&estadoId=1
// Esta actualização deveria ser feita no método post?
        public IActionResult Companhias(int? id, int? estadoId)
        {
            var companhias_aceites = Enumerable.Empty<Companhia>().AsQueryable();

            var companhias_pendentes = Enumerable.Empty<Companhia>().AsQueryable();

            if (id != null && estadoId != null)
            {
                var comp = _context.Companhia.Select(c => c).Where(i => (i.CompanhiaId == id)).First();
                Companhia companhia = (Companhia) comp;
                
                companhia.EstadoId = estadoId.Value;
                _context.Update(companhia);
                _context.SaveChanges();

            }

            companhias_pendentes = _context.Companhia.Select(c => c).Include(p => p.Pais).Include(r => r.ListaReservas).Where(p => p.EstadoId == 2); // EstadoId = 2 => Pendente
            companhias_aceites = _context.Companhia.Select(c => c).Include(p => p.Pais).Include(r => r.ListaReservas).Where(p => p.EstadoId == 1); // EstadoId = 1 => Aceite

            ViewBag.CompanhiasPendentes = companhias_pendentes;
            ViewBag.CompanhiasAceites = companhias_aceites;


            //return View(companhias);
            return View();
        }

        public IActionResult Viagens()
        {
            var viagens = _context.Reserva.Select(c => c).Include(a => a.AeroportoDestino).Include(a => a.AeroportoPartida).Include(s => s.Cliente).Include(co => co.Companhia);

            return View(viagens);
        }

    }

    

}