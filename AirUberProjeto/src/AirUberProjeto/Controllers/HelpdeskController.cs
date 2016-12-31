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
    /// <summary>
    /// Classe respons�vel por receber todos os pedidos do browser e tratar dos mesmos.
    /// </summary>
    /// <remarks>
    /// Apenas s�o autorizados pedidos cujo papel do utilizador seja o de Helpdesk.
    /// </remarks>
    [Authorize(Roles = Roles.ROLE_HELPDESK)]
    public class HelpdeskController : Controller
    {
        private readonly AirUberDbContext _context;

        public HelpdeskController(AirUberDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Respons�vel por redireccionar o utilizador de helpdesk para a p�gina inicial do helpdesk.
        /// </summary>
        /// <returns>Retorna a view da p�gina inicial do helpdesk</returns>
        public IActionResult Index()
        {
            // Informa��o Companhia
            ViewBag.NumeroCompanhiasAceites = _context.Companhia.Select(p => p).Where(p => p.EstadoId == 1).Count();
            ViewBag.NumeroCompanhiasPendentes = _context.Companhia.Select(c => c).Where(p => p.EstadoId == 2).Count();

            // Informa��o Clientes
            ViewBag.NumeroTotalClientes = _context.Cliente.Select(p => p).Count();
            ViewBag.NumeroClientesRegistadosHoje = _context.Cliente.Select(p => p).Where(c => c.DataCriacao.Date == DateTime.Now.Date).Count();

            //Informa��o Viagens
            ViewBag.NumeroTotalViagens = _context.Reserva.Select(p => p).Count();
            //ViewBag.CreditosMovidos = _context.Reserva.Select(p => new { Custo = p.Sum(s => s.Custo)});
            var creditosMovidos = from p in _context.Reserva group p by 1 into g select new { Custo = g.Sum(s => s.Custo) };
            
            //n�o existe outra forma?
            decimal val = 0.0m;
            foreach(var c in creditosMovidos)
            {
                val = c.Custo;      // LAYOUT PROBLEM -> valor alto - sai fora do circulo ja existente
            }
            ViewBag.CreditosMovidos = val;

            return View();
        }

        /// <summary>
        /// Respons�vel por redireccionar o utilizador de helpdesk para a p�gina que apresenta a informa��o de todos os clientes.
        /// </summary>
        /// <returns>Retorna a view dos clientes</returns>
        public IActionResult Clientes()
        {

            var clientes = _context.Cliente.Select(p => p)
                                                   .Include(p => p.ContaDeCreditos)
                                                   .Include(r => r.ListaReservas);   // Este r.ListaReservas carrega a lista de reservas. Necess�rio faz�-lo sempre que existe uma cole��o!
            return View(clientes);
        }


//Se fizer algo do g�nero -> actualiza��es s�o feitas! http://localhost:43636/Helpdesk/Companhias?id=3&estadoId=1
// Esta actualiza��o deveria ser feita no m�todo post?  -> raz�es de seguran�a

        /// <summary>
        /// Respons�vel por redireccionar o utilizador de helpdesk para a p�gina que apresenta a informa��o de todas as companhias.
        /// </summary>
        /// <remarks>
        /// S�o apresentados todas as companhias pendentes (caso existam) e aceites
        /// </remarks>
        /// <param name="id">id da companhia</param>
        /// <param name="estadoId">id do estado da companhia</param>
        /// <returns>Retorna a view dos clientes</returns>
        public IActionResult Companhias(int? id, int? estadoId)
        {
            //var companhias_aceites = Enumerable.Empty<Companhia>().AsQueryable();

            //var companhias_pendentes = Enumerable.Empty<Companhia>().AsQueryable();

            if (id != null && estadoId != null)
            {
                var comp = _context.Companhia.Select(c => c).Where(i => (i.CompanhiaId == id)).First();
                Companhia companhia = (Companhia) comp;
                
                companhia.EstadoId = estadoId.Value;
                _context.Update(companhia);
                _context.SaveChanges();

            }

            ViewBag.CompanhiasPendentes = _context.Companhia.Select(c => c).Include(p => p.Pais)
                                                                    .Include(p => p.ContaDeCreditos)
                                                                    .Include(p => p.ListaReservas)
                                                                    .Include(p => p.ListaColaboradores)
                                                                    .Include(p => p.ListaJatos)
                                                                    .Where(p => p.EstadoId == 2); // EstadoId = 2 => Pendente

            ViewBag.CompanhiasAceites = _context.Companhia.Select(c => c).Include(p => p.Pais)
                                                                  .Include(p => p.ContaDeCreditos)
                                                                  .Include(p => p.ListaReservas)
                                                                  .Include(p => p.ListaColaboradores)
                                                                  .Include(p => p.ListaJatos)
                                                                  .Where(p => p.EstadoId == 1); // EstadoId = 1 => Aceite

            return View();
        }

        /// <summary>
        /// Respons�vel por redireccionar o utilizador de helpdesk para a p�gina que apresenta a informa��o de todas as viagens.
        /// </summary>
        /// <returns>Retorna a view das viagens</returns>
        public IActionResult Viagens()
        {
            var viagens = _context.Reserva.Select(c => c)
                                          .Include(a => a.AeroportoDestino)
                                          .Include(a => a.AeroportoPartida)
                                          .Include(a => a.Cliente)
                                          .Include(a => a.Jato)
                                          .Include(a => a.Jato.Companhia)
                                          .Include(r => r.ListaExtras);

            return View(viagens);
        }

        /// <summary>
        /// Respons�vel por redireccionar o utilizador de helpdesk para a p�gina que apresenta a informa��o de todas as viagens
        /// de um utilizador. 
        /// </summary>
        /// <remarks>
        /// As informa��es do utilizador s�o recebidas como par�metro. 
        /// </remarks>
        /// <param name="email">email do utilizador selecionado</param>
        /// <param name="count">n�merto de reservas do utilizador selecionado</param>
        /// <returns></returns>
        public IActionResult ModalViagens(string email, int? count)
        {
            if(email != null && count != null && count > 0)
            {
                var listaViagens = _context.Reserva.Select(c => c)
                                                   .Include(c => c.Cliente)
                                                   .Include(c => c.ListaExtras)
                                                   .Where(c => c.Cliente.Email == email);

                //A ideia era apresentar uma p�gina modal
                return View(listaViagens);
                
            }
            return NotFound();
        }

    }

    

}