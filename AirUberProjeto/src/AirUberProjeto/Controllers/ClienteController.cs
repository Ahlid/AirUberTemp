using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AirUberProjeto.Controllers
{

    /// <summary>
    /// Controlador respons�vel pela gest�o de clientes
    /// </summary>
    [Authorize(Roles = Roles.ROLE_CLIENTE)]
    public class ClienteController : Controller
    {

        /// <summary>
        /// Utilizado para sabermos o caminho absoluto da pasta wwwRoot
        /// </summary>
        private readonly IHostingEnvironment _environment;

        /// <summary>
        /// O contexto da aplica��o para poder aceder a dados.
        /// </summary>
        private readonly AirUberDbContext _context;

        /// <summary>
        /// User manager que vai permitir utilizar metodos feitos pela windows de forma a controlar os user.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;



        /// <summary>
        /// Construtor do controlador
        /// </summary>
        /// <param name="context">O DB context da aplica��o</param>
        /// <param name="userManager">O manager dos utilizadores</param>
        /// <param name="environment">O ambiente da aplica��o</param>
        public ClienteController(AirUberDbContext context, UserManager<ApplicationUser> userManager,
            IHostingEnvironment environment)
        {
            _environment = environment;
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// A ac��o index � a a��o default do controlador e redireciona para a a��o Perfil
        /// </summary>
        /// <returns>Retorna a view retornada pela ac��o Perfil</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Perfil");
        }



        /// <summary>
        /// Apresenta a p�gina de perfil do cliente tendo em conta o utilizador atual
        /// </summary>
        /// <returns>A view do perfil do cliente</returns>
        [HttpGet]
        public IActionResult Perfil()
        {
            string idCliente = _userManager.GetUserAsync(this.User).Result.Id;

            Cliente cliente = (_context.Cliente
                    .Include(c => c.ContaDeCreditos)
                    .Include(c => c.ListaReservas)
                    .Where(c => c.Id == idCliente)
                    .Select(c => c))
                .Single();

            PerfilViewModel viewModel = new PerfilViewModel()
            {
                Cliente = cliente,
                NumeroViagens = cliente.ListaReservas.Count
            };

            List<Notificacao> notificacoes = _context.Notificacao.Where((n) =>
                        n.UtilizadorId == cliente.Id
            ).ToList();

            foreach (Notificacao notificacao in notificacoes)
            {
                viewModel.Notificacoes.Add(notificacao);
            }


            return View(viewModel);
        }



        /// <summary>
        /// Permite marcar uma notifica��o como lida atrav�s do seu ID
        /// </summary>
        /// <param name="id">Id da notifica��o ao qual se quer marcar como lida</param>
        /// <returns>Variavel booleana que indica se houve sucesso a marcar a notifica��o como lida</returns>
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
        /// Apresenta a p�gina de edi��o de perfil do cliente tendo em conta o utilizador atual
        /// </summary>
        /// <returns>A View de edi��o do perfil do cliente</returns>
        [HttpGet]
        public IActionResult EditarPerfil()
        {
            Cliente cliente = (Cliente) _userManager.GetUserAsync(this.User).Result;

            return View(cliente);
        }


        /// <summary>
        /// Trata de um pedido de altera��o de dados de um cliente
        /// </summary>
        /// <param name="viewModel">ViewModel do pedido de altera��o de dados</param>
        /// <returns>A view de edi��o de perfil do cliente</returns>
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
        /// Permite alterar a imagem de perfil do cliente
        /// </summary>
        /// <param name="file">ficheiro de imagem que ir� substituir a imagem de perfil</param>
        /// <returns>Um redirecionamento para a a��o editar perfil</returns>
        [HttpPost]
        public async Task<IActionResult> AlterarImagemPerfil(IFormFile file)
        {
            if (file == null)
            {
                return RedirectToAction(nameof(ClienteController.EditarPerfil), "Cliente");
            }


            string extension = Path.GetExtension(file.FileName);
            if (extension != ".jpg" && extension != ".png")
                return null;

            Cliente cliente = (Cliente) _userManager.GetUserAsync(this.User).Result;

            string fileName = "imagem-perfil" + extension;
            string folderName = Path.Combine("clientes", "client-" + cliente.Email);
            string relativePathToFile = Path.Combine(folderName, fileName);

            string forderPath = Path.Combine(_environment.WebRootPath, folderName);
            string absolutePathToFile = Path.Combine(_environment.WebRootPath, relativePathToFile);

            //Cria a directoria caso ainda n�o exista
            Directory.CreateDirectory(forderPath);

            //Transmite o ficheiro atrav�s de um FileStream
            using (var fileStream = new FileStream(absolutePathToFile, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            cliente.RelativePathImagemPerfil = relativePathToFile;
            _context.Update(cliente);
            _context.SaveChanges();

            return RedirectToAction(nameof(ClienteController.EditarPerfil), "Cliente");
        }


        //Viagens

        /// <summary>
        /// Respons�vel por redireccionar o utilizador para a p�gina que apresenta a informa��o de todas as viagens feitas na companhia.
        /// </summary>
        /// <returns>Retorna a view das viagens</returns>
        public IActionResult VerViagens()
        {

            Cliente cliente = (Cliente) _userManager.GetUserAsync(this.User).Result;

            var viagens = _context.Reserva.Select(c => c)
                .Include(a => a.AeroportoDestino)
                .Include(a => a.AeroportoPartida)
                .Include(a => a.Cliente)
                .Include(a => a.Jato)
                .Include(a => a.Jato.Companhia)
                .Include(r => r.ListaExtras)
                .Where(c => c.Cliente.Id == cliente.Id).ToList();

            return View(viagens);

        }

        private double DegreesToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private double RadiansToDegrees(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        public double distFrom(double lat1, double lng1, double lat2, double lng2)
        {
            double earthRadius = 3958.75;
            double dLat = DegreesToRadians(lat2 - lat1);
            double dLng = DegreesToRadians(lng2 - lng1);
            double sindLat = Math.Sin(dLat / 2);
            double sindLng = Math.Sin(dLng / 2);
            double a = Math.Pow(sindLat, 2) + Math.Pow(sindLng, 2)
                    * Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2));
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double dist = earthRadius * c;

            return dist;
        }


        /// <summary>
        /// Respons�vel por redireccionar o utilizador para a p�gina que apresenta a informa��o de uma oferta
        /// </summary>
        /// <returns>Retorna a view das ofertas</returns>
        public IActionResult VerOferta(int id, DateTime data, double lat, double lon, int jatoid)
        {

            try
            {

                Aeroporto aeroporto = _context.Aeroporto.Single(a => a.AeroportoId == id);
                

                Jato Jato = _context.Jato
                    .Select(c => c)
                    .Include(a => a.Companhia)
                    .Include(a => a.Modelo)
                    .Include(a => a.Aeroporto)
                    .Include(a => a.Companhia.ListaExtras)
                    .Include(a => a.Companhia.ListaReservas)
                    .First(a => a.JatoId == jatoid);

                if (Jato == null)
                    return RedirectToAction("Index");

                if (Jato.RelativePathImagemPerfil == null)
                    Jato.RelativePathImagemPerfil = Path.Combine("images", "aviao-default.svg");

                ICollection<Reserva> Reservas = Jato.Companhia.ListaReservas;
                int reservasAvalidas = Reservas.Count(c => c.Avaliacao >= 0 && c.Avaliacao < 6);

                int estrelas = 0;

                if (reservasAvalidas == 0)
                {
                    estrelas = 5;
                }
                else {
                    estrelas = Reservas.Where(c => c.Avaliacao >= 0 && c.Avaliacao < 6).Select(c => c.Avaliacao).Sum() / reservasAvalidas;
                }

                VerOfertaViewModel verOfertaViewModel = new VerOfertaViewModel()
                {
                    Jato = Jato,
                    Partida = Jato.Aeroporto.Nome,
                    Chegada = "Aeroporto Paris",
                    DataPartida = DateTime.Now,
                    Estrelas = estrelas,
                    Kilometros = distFrom(aeroporto.Latitude, 
                                            aeroporto.Longitude,
                                            lat, 
                                            lon)
                };


                return View(verOfertaViewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

        }

        public IEnumerable<Aeroporto> AeroportosDisponiveis(DateTime data)
        {

            IEnumerable<Jato> jatosDIsponiveis = _context.Jato
                .Include(c => c.ListaDisponibilidade)
                .Include(c => c.Aeroporto)
                .Where(c => jatoDisponivel(c, data)).ToList();

            IEnumerable<Aeroporto> aeroportos = jatosDIsponiveis
                .Select(c => c.Aeroporto)
                .Distinct()
                .ToList();

            return aeroportos;

        }

        public IActionResult SelecionarDestino(int id, DateTime data)
        {

            //todo validar

            ViewData["id"] = id;
            ViewData["data"] = data;

            return View();

        }

        /// <summary>
        /// Respons�vel por redireccionar o utilizador para a p�gina de pesquisa de ofertas
        /// </summary>
        /// <returns>Retorna a view da procura de ofertas</returns>
        public IActionResult ProcurarOfertas()
        {

            return View();

        }

        /// <summary>
        /// Respons�vel por redireccionar o utilizador para a p�gina de pesquisa de ofertas
        /// </summary>
        /// <returns>Retorna a view da procura de ofertas</returns>
        [HttpPost]
        public IActionResult ProcurarOfertas(int id)
        {
            //TODO: verificar se o o id existe, redirecionar para a lista de jatos dispon�veis.


            var viagens = _context.Aeroporto.Select(c => c).ToList();
            return View(viagens);

        }


        private bool jatoDisponivel(Jato jato, DateTime DataReserva)
        {
            foreach (Disponibilidade disponibilidade in jato.ListaDisponibilidade)
            {
                
                DateTime d1 = Convert.ToDateTime(disponibilidade.Inicio);

                DateTime d2 = Convert.ToDateTime(disponibilidade.Fim);

                if (DataReserva.Ticks > d1.Ticks && DataReserva.Ticks < d2.Ticks)
                {
                    return true;
                }
            }

            return false;

        }

        /// <summary>
        /// Respons�vel por redireccionar o utilizador para a p�gina de pesquisa de ofertas
        /// </summary>
        /// <returns>Retorna a view da procura de ofertas</returns>
        [HttpGet]
        public IActionResult VerJatos(int id, DateTime data, double lat, double lon)
        {
            //todo: validar dadods

            

            IEnumerable<Jato> jatos = _context.Jato.Include(c => c.ListaDisponibilidade)
                .Include(c => c.Modelo)
                .Include(c => c.Modelo.TipoJato)
                .Include(c => c.Companhia)
                .Include(c => c.Aeroporto)
                .Where(c => c.AeroportoId == id)
                .Where(c => jatoDisponivel(c, data)).ToList();

            VerJatoViewModel viewModel = new VerJatoViewModel()
            {
                AeroportoId = id,
                DataPartida = data,
                Latitude = lat,
                Longitude = lon,
                JatodDisponiveis = jatos
            };

            return View(viewModel);

        }

        /// <summary>
        /// Respons�vel por redireccionar o utilizador para a p�gina de pesquisa de ofertas
        /// </summary>
        /// <returns>Retorna a view da procura de ofertas</returns>
        [HttpGet]
        public IActionResult ReservaConcluida(int id, DateTime data, double lat, double lon)
        {
            return View();
        }


    }

}

