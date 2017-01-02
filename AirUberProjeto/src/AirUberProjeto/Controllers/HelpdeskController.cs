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

            var clientes =  getClientes();

            return View(clientes);
        }

        /// <summary>
        /// Respons�vel por redireccionar o utilizador de helpdesk para a p�gina que apresenta a informa��o de todas as companhias.
        /// </summary>
        /// <remarks>
        /// S�o apresentados todas as companhias pendentes (caso existam) e aceites
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

            // passar estas coisas todas para m�todos!!!!!!!
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
        /// Respons�vel por redireccionar o utilizador de helpdesk para a p�gina que apresenta a informa��o das companhias recusadas.
        /// </summary>
        /// <remarks>
        /// � apresentada uma listagem de companhias caso estas existam, se n�o � apresentada uma mensagem a informar que n
        /// n�o existem companhias recusadas.
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
        /// Respons�vel por alterar o estado de uma companhia. 
        /// </summary>
        /// <param name="id">id da companhia</param>
        /// <param name="estadoActualId">estado actual da companhia</param>
        /// <param name="estadoDestinoId">estado que o utilizador de helpdesk indicou para a companhia</param>
        /// <returns>Retorna a view das companhias ou das companhias recusadas, dependendo da 
        /// localiza��o do utilizador de helpdesk</returns>
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

            // se a companhia estiver recusada volto para a p�gina de companhias recusadas.
            //return estadoActualId == 3 ? View("~/Views/Helpdesk/RecuperarCompanhias.cshtml") : View("~/Views/Helpdesk/Companhias.cshtml");
            
            // optar por esta forma, porque assim o url � sempre actualizado!
            return estadoActualId == 3 ? RedirectToAction("RecuperarCompanhias") : RedirectToAction("Companhias");
        }
    
        /// <summary>
        /// Respons�vel por redireccionar o utilizador de helpdesk para a p�gina que apresenta a informa��o de todas as viagens.
        /// </summary>
        /// <returns>Retorna a view das viagens</returns>
        public IActionResult Viagens()
        {
            var viagens = getReservas();
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
        /// <returns>Retorna View Modal caso email seja valido e ccontenha viagens, NotFound caso contratio</returns>
        public IActionResult ModalViagens(string email, int? count)
        {
            if (email != null && count != null && count > 0)
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

        /// <summary>
        /// Retorna a lista de todas as reservas registadas no sistema.
        /// </summary>
        /// <returns>Lista de reservas</returns>
        private IList<Reserva> getReservas()
        {
            return _context.Reserva.Select(c => c)
                                          .Include(a => a.AeroportoDestino)
                                          .Include(a => a.AeroportoPartida)
                                          .Include(a => a.Cliente)
                                          .Include(a => a.Jato)
                                          .Include(a => a.Jato.Companhia)
                                          .Include(r => r.ListaExtras).ToList();
        }

        /// <summary>
        /// Retorna uma query que cujo resultado � composto por todos os clientes registados no sistema.
        /// </summary>
        /// <returns>Query cujo resultado � composto pelas clientes</returns>
        private IQueryable<Cliente> getClientes()
        {
            // � um IQueryable porque est� ligado � forma como a view foi feita
            // para mudar para IList<Cliente> a view deve ser adaptada, a forma como se percorrer os elementos!
            return _context.Cliente.Select(p => p)
                                                   .Include(p => p.ContaDeCreditos)
                                                   .Include(r => r.ListaReservas);   // Este r.ListaReservas carrega a lista de reservas. Necess�rio faz�-lo sempre que existe uma cole��o!
        }

    }
}