using System;
using System.Collections;
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
    /// Classe responsável por receber todos os pedidos do browser e tratar dos mesmos.
    /// </summary>
    /// <remarks>
    /// Apenas são autorizados pedidos cujo papel do utilizador seja o de Helpdesk.
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
        /// Responsável por redireccionar o utilizador de helpdesk para a página inicial do helpdesk.
        /// </summary>
        /// <returns>Retorna a view da página inicial do helpdesk</returns>
        public IActionResult Index()
        {
            // Informação Companhia
            ViewBag.NumeroCompanhiasAceites = _context.Companhia.Select(p => p).Where(p => p.EstadoId == 1).Count();
            ViewBag.NumeroCompanhiasPendentes = _context.Companhia.Select(c => c).Where(p => p.EstadoId == 2).Count();

            // Informação Clientes
            ViewBag.NumeroTotalClientes = _context.Cliente.Select(p => p).Count();
            ViewBag.NumeroClientesRegistadosHoje = _context.Cliente.Select(p => p).Where(c => c.DataCriacao.Date == DateTime.Now.Date).Count();

            //Informação Viagens
            ViewBag.NumeroTotalViagens = _context.Reserva.Select(p => p).Count();
            //ViewBag.CreditosMovidos = _context.Reserva.Select(p => new { Custo = p.Sum(s => s.Custo)});
            var creditosMovidos = from p in _context.Reserva group p by 1 into g select new { Custo = g.Sum(s => s.Custo) };
            
            //não existe outra forma?
            decimal val = 0.0m;
            foreach(var c in creditosMovidos)
            {
                val = c.Custo;      // LAYOUT PROBLEM -> valor alto - sai fora do circulo ja existente
            }
            ViewBag.CreditosMovidos = val;

            return View();
        }

        /// <summary>
        /// Responsável por redireccionar o utilizador de helpdesk para a página que apresenta a informação de todos os clientes.
        /// </summary>
        /// <returns>Retorna a view dos clientes</returns>
        public IActionResult Clientes()
        {

            var clientes = _context.Cliente.Select(p => p)
                                                   .Include(p => p.ContaDeCreditos)
                                                   .Include(r => r.ListaReservas);   // Este r.ListaReservas carrega a lista de reservas. Necessário fazê-lo sempre que existe uma coleção!

            return View(clientes);
        }

        /// <summary>
        /// Responsável por redireccionar o utilizador de helpdesk para a página que apresenta a informação de todas as companhias.
        /// </summary>
        /// <remarks>
        /// São apresentados todas as companhias pendentes (caso existam) e aceites
        /// </remarks>
        /// <param name="id">id da companhia</param>
        /// <param name="estadoId">id do estado da companhia</param>
        /// <returns>Retorna a view dos clientes</returns>
        public IActionResult Companhias()
        {
            
            


            var companhiasPendentes = _context.Companhia.Select(c => c).Include(p => p.Pais)
                                                                    .Include(p => p.ContaDeCreditos)
                                                                    .Include(p => p.ListaReservas)
                                                                    .Include(p => p.ListaColaboradores)
                                                                    .Include(p => p.ListaJatos)
                                                                    .Where(p => p.EstadoId == 2); // EstadoId = 2 => Pendente

            ViewBag.CompanhiasPendentes = companhiasPendentes;
            ViewBag.NumeroCompanhiasPendentes = companhiasPendentes.Count();

            // passar estas coisas todas para métodos!!!!!!!
            ViewBag.NumeroCompanhiasRecusadas = _context.Companhia.Select(c => c).Include(p => p.Pais)
                                                                    .Include(p => p.ContaDeCreditos)
                                                                    .Include(p => p.ListaReservas)
                                                                    .Include(p => p.ListaColaboradores)
                                                                    .Include(p => p.ListaJatos)
                                                                    .Where(p => p.EstadoId == 3).Count(); // EstadoId = 3 => Recusada
            ViewBag.CompanhiasAceites = getCompanhiaPorEstado(1);


            return View();
        }

        /// <summary>
        /// Responsável por redireccionar o utilizador de helpdesk para a página que apresenta a informação das companhias recusadas.
        /// </summary>
        /// <remarks>
        /// É apresentada uma listagem de companhias caso estas existam, se não é apresentada uma mensagem a informar que n
        /// não existem companhias recusadas.
        /// </remarks>
        /// <returns>Retorna a view das companhias recusadas</returns>
        public IActionResult RecuperarCompanhias()
        {
            var companhiasRecusadas = getCompanhiaPorEstado(3);
            ViewBag.CompanhiasRecusadas = companhiasRecusadas;
            ViewBag.NumeroCompanhiasRecusadas = companhiasRecusadas.Count();

            return View();
        }
        
        /// <summary>
        /// Responsável por alterar o estado de uma companhia. 
        /// </summary>
        /// <param name="id">id da companhia</param>
        /// <param name="estadoActualId">estado actual da companhia</param>
        /// <param name="estadoDestinoId">estado que o utilizador de helpdesk indicou para a companhia</param>
        /// <returns>Retorna a view das companhias ou das companhias recusadas, dependendo da 
        /// localização do utilizador de helpdesk</returns>
        public IActionResult AlterarEstadoCompanhia(int id, int estadoActualId, int estadoDestinoId)
        {

            var comp = _context.Companhia.Select(c => c).Where(i => (i.CompanhiaId == id)).First();
            Companhia companhia = (Companhia)comp;

            companhia.EstadoId = estadoDestinoId;
            _context.Update(companhia);
            _context.SaveChanges();

            if(estadoActualId == 0) //no state
            {
                ViewBag.CompanhiasAceites = getCompanhiaPorEstado(1);
                ViewBag.NumeroCompanhiasRecusadas = getCompanhiaPorEstado(3).Count();
            }

            // se a companhia estiver recusada volto para a página de companhias recusadas.
            //return estadoActualId == 3 ? View("~/Views/Helpdesk/RecuperarCompanhias.cshtml") : View("~/Views/Helpdesk/Companhias.cshtml");
            
            // optar por esta forma, porque assim o url é sempre actualizado!
            return estadoActualId == 3 ? RedirectToAction("RecuperarCompanhias") : RedirectToAction("Companhias");
        }
    
        /// <summary>
        /// Responsável por redireccionar o utilizador de helpdesk para a página que apresenta a informação de todas as viagens.
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
                                          .Include(r => r.ListaExtras).ToList();
            return View(viagens);
        }

        /// <summary>
        /// Responsável por redireccionar o utilizador de helpdesk para a página que apresenta a informação de todas as viagens
        /// de um utilizador. 
        /// </summary>
        /// <remarks>
        /// As informações do utilizador são recebidas como parâmetro. 
        /// </remarks>
        /// <param name="email">email do utilizador selecionado</param>
        /// <param name="count">númerto de reservas do utilizador selecionado</param>
        /// <returns>Retorna View Modal caso email seja valido e ccontenha viagens, NotFound caso contratio</returns>
        public IActionResult ModalViagens(string email, int? count)
        {
            if (email != null && count != null && count > 0)
            {
                var listaViagens = _context.Reserva.Select(c => c)
                                                   .Include(c => c.Cliente)
                                                   .Include(c => c.ListaExtras)
                                                   .Where(c => c.Cliente.Email == email);

                //A ideia era apresentar uma página modal
                return View(listaViagens);

            }
            return NotFound();
        }

        /*
         * Auxiliar
         */ 

        /// <summary>
        /// Retornar uma lista de companhias de acordo com o estado da companhia.
        /// Estado = 1 -> Companhia Aceite
        /// Estado = 2 -> Companhia Pendente
        /// Estado = 3 -> Companhia Recusada
        /// </summary>
        /// <param name="estado">estado de uma companhia</param>
        /// <returns>Lista de companhias de acordo com o estado</returns>
        private IList<Companhia> getCompanhiaPorEstado(int estado)
        {
            return _context.Companhia.Select(c => c).Include(p => p.Pais)
                                                                    .Include(p => p.ContaDeCreditos)
                                                                    .Include(p => p.ListaReservas)
                                                                    .Include(p => p.ListaColaboradores)
                                                                    .Include(p => p.ListaJatos)
                                                                    .Where(p => p.EstadoId == estado).ToList().ToList();
        }

    }
}