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
using Remotion.Linq.Clauses;

namespace AirUberProjeto.Controllers
{

    /// <summary>
    /// Controlador responsável pela gestão de clientes
    /// </summary>
    [Authorize(Roles = Roles.ROLE_CLIENTE)]
    public class ClienteController : Controller
    {

        /// <summary>
        /// Utilizado para sabermos o caminho absoluto da pasta wwwRoot
        /// </summary>
        private readonly IHostingEnvironment _environment;

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
        /// <param name="environment">O ambiente da aplicação</param>
        public ClienteController(AirUberDbContext context, UserManager<ApplicationUser> userManager,
            IHostingEnvironment environment)
        {
            _environment = environment;
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// A acção index é a ação default do controlador e redireciona para a ação Perfil
        /// </summary>
        /// <returns>Retorna a view retornada pela acção Perfil</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Perfil");
        }



        /// <summary>
        /// Apresenta a página de perfil do cliente tendo em conta o utilizador atual
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
        /// Permite marcar uma notificação como lida através do seu ID
        /// </summary>
        /// <param name="id">Id da notificação ao qual se quer marcar como lida</param>
        /// <returns>Variavel booleana que indica se houve sucesso a marcar a notificação como lida</returns>
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
        /// Apresenta a página de edição de perfil do cliente tendo em conta o utilizador atual
        /// </summary>
        /// <returns>A View de edição do perfil do cliente</returns>
        [HttpGet]
        public IActionResult EditarPerfil()
        {
            Cliente cliente = (Cliente) _userManager.GetUserAsync(this.User).Result;

            return View(cliente);
        }


        /// <summary>
        /// Trata de um pedido de alteração de dados de um cliente
        /// </summary>
        /// <param name="viewModel">ViewModel do pedido de alteração de dados</param>
        /// <returns>A view de edição de perfil do cliente</returns>
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
        /// <param name="file">ficheiro de imagem que irá substituir a imagem de perfil</param>
        /// <returns>Um redirecionamento para a ação editar perfil</returns>
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

            //Cria a directoria caso ainda não exista
            Directory.CreateDirectory(forderPath);

            //Transmite o ficheiro através de um FileStream
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
        /// 
        /// </summary>
        private enum TipoDisponibilidade
        {
            DisponivelDeslocacao, //disponivel para deslocar
            Disponivel, //disponivel no aeroporto
            NaoDisponivel //nao disponivel
        }

        //Verifica se o jato consegue executar a distância
        private bool DistanciaDeslocaoValida(int jatoId, int aeroportoPartidaId, int aeroportoDestinoId)
        {
            Jato jato = _context.Jato
                .Single(j => j.JatoId == jatoId);

            Aeroporto aeroportoPartida = _context.Aeroporto
                .Single(j => j.AeroportoId == aeroportoPartidaId);
            Aeroporto aeroportoDestino = _context.Aeroporto
                .Single(j => j.AeroportoId == aeroportoDestinoId);


            double distanciaMaxima = 5000;//todo = jato.DistanciaMaxima;
            double distancia = DistanciaEntreCoordenadas(
                aeroportoPartida.Latitude,
                aeroportoPartida.Longitude,
                aeroportoDestino.Latitude,
                aeroportoDestino.Longitude
            );

            return distancia <= distanciaMaxima;

        }

        //calcula o tempo de deslocação de uma jato desde um aeroporto até outro em ticks
        private double CalcularTempoDeslocacaoTicks(int jatoId, int aeroportoPartidaId, int aeroportoDestinoId)
        {
            if (!DistanciaDeslocaoValida(jatoId, aeroportoPartidaId, aeroportoDestinoId))
            {
                throw new Exception("Deslocação inválida");
            }


            Jato jato = _context.Jato
                .Single(j => j.JatoId == jatoId);

            Aeroporto aeroportoPartida = _context.Aeroporto
                .Single(j => j.AeroportoId == aeroportoPartidaId);
            Aeroporto aeroportoDestino = _context.Aeroporto
                .Single(j => j.AeroportoId == aeroportoDestinoId);

            double distancia = DistanciaEntreCoordenadas(
                aeroportoPartida.Latitude,
                aeroportoPartida.Longitude,
                aeroportoDestino.Latitude,
                aeroportoDestino.Longitude
            );
            //Metros por segundo
            double velocidade = 5; //todo: jato.Velocidade; 

            return (distancia/velocidade)* TimeSpan.TicksPerSecond;

        }

        private TipoDisponibilidade JatoDisponivelPontual(int jatoId, int aeroportoPartidaId, DateTime dataReserva)
        {
            
            if(dataReserva.Ticks < DateTime.Now.Ticks)
                return TipoDisponibilidade.NaoDisponivel;

            Jato jato = _context.Jato
                .Include(j => j.ListaDisponibilidade)
                .Include(j => j.Companhia)
                .Include(j => j.Companhia.ListaReservas)
                .Single(j => j.JatoId == jatoId);
            
            foreach (Disponibilidade disponibilidade in jato.ListaDisponibilidade)
            {

                DateTime d1 = Convert.ToDateTime(disponibilidade.Inicio);
                DateTime d2 = Convert.ToDateTime(disponibilidade.Fim);
                bool p1 = dataReserva.Ticks > d1.Ticks;
                bool p2 = dataReserva.Ticks < d2.Ticks;

                //Data enquadra-se na disponibilidade
                if (p1 && p2)
                {
                    bool existeReserva = jato.Companhia.ListaReservas
                        .Where(r => r.JatoId == jatoId)
                        .Any(r => r.DataChegada.Ticks >= dataReserva.Ticks &&
                                r.DataPartida.Ticks <= dataReserva.Ticks);

                    if (existeReserva)
                    {
                        return TipoDisponibilidade.NaoDisponivel;
                    }

                    //Última reserva antes da data
                    Reserva reserva = jato.Companhia.ListaReservas
                        .Where(r => r.JatoId == jatoId)
                        .Where(r => r.DataChegada.Ticks < dataReserva.Ticks)
                        .OrderByDescending(r => r.DataChegada.Ticks).FirstOrDefault();
                    
                    double tempoDePreparacao = 0; //todo: = jato.TempoPreparacao;
                    double tempoDeDeslocacao;
                    double tempoTotalDispendido;

                    if (reserva == null)
                    {
                        //Nao existe reserva logo vemos a posição atual do avião e 
                        //calculamos o tempo de voo até chegar ao aeroporto de partida
                       
                        //Se o jato está na posição de partida
                        if (jato.AeroportoId == aeroportoPartidaId)
                        {
                            return TipoDisponibilidade.Disponivel;
                        }

                        //Se a deslocação não possivel para a distância
                        if (!DistanciaDeslocaoValida(jatoId, jato.AeroportoId, aeroportoPartidaId))
                        {
                            return TipoDisponibilidade.NaoDisponivel;
                        }

                        tempoDeDeslocacao = CalcularTempoDeslocacaoTicks(jatoId, jato.AeroportoId, aeroportoPartidaId);
                        tempoTotalDispendido = tempoDeDeslocacao + tempoDePreparacao;
                        double tempoDisponivel = dataReserva.Ticks - DateTime.Now.Ticks;

                        if (tempoTotalDispendido < tempoDisponivel)
                        {
                            return TipoDisponibilidade.DisponivelDeslocacao;
                        }


                        return TipoDisponibilidade.NaoDisponivel;
                    }

                    //Se a última reserva tem destino ao aeroporto de partida 
                    if (reserva.AeroportoDestinoId == aeroportoPartidaId)
                    {
                        //Nesta fase não conseguimos garantir que não haja
                        //uma reserva sobreposta pois ainda não sabemos o destino.
                        //Portanto assume-se que o jato está disponível no aeroporto
                        //sem recurrer a deslocações.

                        return TipoDisponibilidade.Disponivel;                
                    }

                    //Se a deslocação não é possivel para a distância
                    if (!DistanciaDeslocaoValida(jatoId, jato.AeroportoId, aeroportoPartidaId))
                    {
                        return TipoDisponibilidade.NaoDisponivel;
                    }

                    tempoDeDeslocacao = CalcularTempoDeslocacaoTicks(jatoId, reserva.AeroportoDestinoId, aeroportoPartidaId);
                    tempoTotalDispendido = tempoDeDeslocacao + tempoDePreparacao;

                    //Se o tempo total dispendido for inferior ao disponível
                    if (tempoTotalDispendido < dataReserva.Ticks - reserva.DataChegada.Ticks)
                    {
                        return TipoDisponibilidade.DisponivelDeslocacao;
                    }
                }
            }

            return TipoDisponibilidade.NaoDisponivel;
        }

        private TipoDisponibilidade JatoDisponivelIntervalo(int jatoId, int aeroportoPartidaId, int aeroportoDestinoId, DateTime dataReserva)
        {

            //todo verificar se a data é posterior a hoje + offset

            Jato jato = _context.Jato
                .Include(j => j.ListaDisponibilidade)
                .Include(j => j.Companhia)
                .Include(j => j.Companhia.ListaReservas)
                .Single(j => j.JatoId == jatoId);

            Aeroporto aeroportoPartida = _context.Aeroporto
                .Single(j => j.AeroportoId == aeroportoPartidaId);
            Aeroporto aeroportoDestino = _context.Aeroporto
                .Single(j => j.AeroportoId == aeroportoDestinoId);

            double tempoDePreparacao = 0; //todo: = jato.TempoPreparacao; 
            double tempoDeDeslocacao = CalcularTempoDeslocacaoTicks(jatoId, aeroportoPartidaId, aeroportoDestinoId);
            double tempoTotalDispendido = tempoDePreparacao + tempoDeDeslocacao;

            foreach (Disponibilidade disponibilidade in jato.ListaDisponibilidade)
            {
                DateTime disponibilidadeInicio = Convert.ToDateTime(disponibilidade.Inicio);
                DateTime disponibilidadeFim = Convert.ToDateTime(disponibilidade.Fim);
                bool p1 = dataReserva.Ticks > disponibilidadeInicio.Ticks;
                bool p2 = dataReserva.Ticks < disponibilidadeFim.Ticks;
                bool p3 = dataReserva.Ticks + tempoTotalDispendido < disponibilidadeFim.Ticks;

                //Não existe disponibilidade para a viagem
                if (!(p1 && p2 && p3))
                {
                    return TipoDisponibilidade.NaoDisponivel;
                }


                bool existeReserva = jato.Companhia.ListaReservas
                         .Where(r => r.JatoId == jatoId)
                         .Any(r => 
                                (r.DataChegada.Ticks >= dataReserva.Ticks &&
                                 r.DataPartida.Ticks <= dataReserva.Ticks) ||
                                 (r.DataChegada.Ticks >= dataReserva.Ticks + tempoTotalDispendido &&
                                 r.DataPartida.Ticks <= dataReserva.Ticks + tempoTotalDispendido)
                         );

                if (existeReserva)
                {
                    return TipoDisponibilidade.NaoDisponivel;
                }




                //Última reserva antes da data
                Reserva reservaAnterior = jato.Companhia.ListaReservas
                    .Where(r => r.JatoId == jatoId)
                    .Where(r => r.DataChegada.Ticks < dataReserva.Ticks)
                    .OrderByDescending(r => r.DataChegada.Ticks).FirstOrDefault();

                bool semDeslocacaoPrevia = false;

                //Não existe reserva que antecede a data
                if (reservaAnterior == null)
                {

                    // Nao existe reserva logo vemos a posição atual do avião e
                    //calculamos o tempo de voo até chegar ao aeroporto de partida

                    //Se o jato está na posição de partida
                    //---PosicaoJato---A___A---NovaReserva---B
                    if (jato.AeroportoId == aeroportoPartidaId)
                    {
                        semDeslocacaoPrevia = true;
                    }

                    //Se a deslocação não é possivel para a distância
                    if (!DistanciaDeslocaoValida(jatoId, jato.AeroportoId, aeroportoPartidaId))
                    {
                        return TipoDisponibilidade.NaoDisponivel;
                    }

                    tempoDeDeslocacao = CalcularTempoDeslocacaoTicks(jatoId, jato.AeroportoId, aeroportoPartidaId);
                    tempoTotalDispendido = tempoDeDeslocacao + tempoDePreparacao;

                    if (!(tempoTotalDispendido < dataReserva.Ticks - DateTime.Now.Ticks))
                    {
                        return TipoDisponibilidade.NaoDisponivel;
                    }


                }
                else
                {
                    //Se a última reserva tem destino ao aeroporto de partida 
                    // C---ReservaAnterior---A___A---NovaReserva---B
                    if (reservaAnterior.AeroportoDestinoId == aeroportoPartidaId)
                    {
                        semDeslocacaoPrevia = true;
                    }

                    //Se a deslocação não é possivel para a distância
                    if (!DistanciaDeslocaoValida(jatoId, jato.AeroportoId, aeroportoPartidaId))
                    {
                        return TipoDisponibilidade.NaoDisponivel;
                    }

                    tempoDeDeslocacao = CalcularTempoDeslocacaoTicks(jatoId, reservaAnterior.AeroportoDestinoId, aeroportoPartidaId);
                    tempoTotalDispendido = tempoDeDeslocacao + tempoDePreparacao;

                    //Se o tempo total dispendido for inferior ao disponível
                    if (tempoTotalDispendido >= dataReserva.Ticks - reservaAnterior.DataChegada.Ticks)
                    {
                        return TipoDisponibilidade.NaoDisponivel;
                    } 
                }


                //Primeira reserva depois da data de chegadas deste voo
                Reserva reservaPosterior = jato.Companhia.ListaReservas
                .Where(r => dataReserva.Ticks + tempoTotalDispendido < r.DataPartida.Ticks)
                .OrderBy(r => r.DataChegada.Ticks).FirstOrDefault();

                // A---NovaReserva---B______________
                if (reservaPosterior == null)
                {
                    return semDeslocacaoPrevia
                        ? TipoDisponibilidade.Disponivel
                        : TipoDisponibilidade.DisponivelDeslocacao;
                }

                //Se o jato vai estar no mesmo aeroporto 
                // A---NovaReserva---B____B---ReservaPosterior---C
                if (aeroportoDestinoId == reservaPosterior.AeroportoPartidaId)
                {
                    return semDeslocacaoPrevia
                        ? TipoDisponibilidade.Disponivel
                        : TipoDisponibilidade.DisponivelDeslocacao;
                }

                // A---NovaReserva---B____C---ReservaPosterior---D
                //Temos que calcular a deslocação

                //Se a deslocação não é possivel para a distância
                if (!DistanciaDeslocaoValida(jatoId, aeroportoDestinoId, reservaPosterior.AeroportoPartida.AeroportoId ))
                {
                    return TipoDisponibilidade.NaoDisponivel;
                }

                double tempoDeDeslocacaoAteProximaReserva = CalcularTempoDeslocacaoTicks(jatoId, aeroportoDestinoId, reservaPosterior.AeroportoPartida.AeroportoId);
                double tempoTotalDispendidoAteProximaReserva = tempoDeDeslocacaoAteProximaReserva + tempoDePreparacao;
                double tempoDisponivel = reservaPosterior.DataPartida.Ticks - (dataReserva.Ticks + tempoDeDeslocacao);
                //Tempo dispendido até ao aeroporto de partida da proxima reserva superior ao tempo disponivel
                if (tempoTotalDispendidoAteProximaReserva > tempoDisponivel)
                {
                    return TipoDisponibilidade.NaoDisponivel;
                }

                return TipoDisponibilidade.DisponivelDeslocacao;


            }

            return TipoDisponibilidade.NaoDisponivel;
        }

        private bool AeroportoDisponivel(int aeroportoId, DateTime dataReserva)
        {
            IEnumerable<Jato> jatos = _context.Jato
                .Include(j => j.Aeroporto)
                .ToList();

            foreach (Jato jato in jatos)
            {
                TipoDisponibilidade disponibilidade = JatoDisponivelPontual(jato.JatoId, aeroportoId, dataReserva);

                switch(disponibilidade)
                {
                    case TipoDisponibilidade.Disponivel:
                        case TipoDisponibilidade.DisponivelDeslocacao:
                        return true;
                    
                }
    
            }

            return false;
        }

        private bool AeroportoDestinoDisponivel(int aeroportoId, int aeroportoDestinoId, DateTime dataReserva)
        {
            IEnumerable<Jato> jatos = _context.Jato
                .Include(j => j.Aeroporto)
                .ToList();

            foreach (Jato jato in jatos)
            {
                TipoDisponibilidade disponibilidade = JatoDisponivelIntervalo(jato.JatoId, aeroportoId, aeroportoDestinoId, dataReserva);

                switch (disponibilidade)
                {
                    case TipoDisponibilidade.Disponivel:
                    case TipoDisponibilidade.DisponivelDeslocacao:
                        return true;

                }

            }

            return false;
        }
        

        private double DegreesToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private double RadiansToDegrees(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        private double DistanciaEntreCoordenadas(double lat1, double lng1, double lat2, double lng2)
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

        public IEnumerable<Aeroporto> AeroportosDisponiveis(DateTime data)
        {
            IEnumerable<Aeroporto> aeroportos = _context.Aeroporto
                .Select(a => new Aeroporto
                {
                    AeroportoId = a.AeroportoId,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    Nome = a.Nome
                })
                .ToList();
            aeroportos = aeroportos.Where(c => AeroportoDisponivel(c.AeroportoId, data)).ToList();

            return aeroportos;
        }

        public IEnumerable<Aeroporto> AeroportosDestinoDisponiveis(int id, DateTime data)
        {
            IEnumerable<Aeroporto> aeroportos = _context.Aeroporto
                .Select(a => new Aeroporto
                {
                    AeroportoId = a.AeroportoId,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    Nome = a.Nome
                })
                .Where(a => a.AeroportoId != id)
                .ToList();

            aeroportos = aeroportos.Where(c => AeroportoDestinoDisponivel(id, c.AeroportoId, data)).ToList();

            return aeroportos;
        }



        /// <summary>
        /// Responsável por redireccionar o utilizador para a página que apresenta a informação de todas as viagens feitas na companhia.
        /// </summary>
        /// <returns>Retorna a view das viagens</returns>
        public IActionResult VerViagens()
        {

            Cliente cliente = (Cliente) _userManager.GetUserAsync(this.User).Result;

            IEnumerable<Reserva> viagens = _context.Reserva.Select(c => c)
                .Include(a => a.AeroportoDestino)
                .Include(a => a.AeroportoPartida)
                .Include(a => a.Cliente)
                .Include(a => a.Jato)
                .Include(a => a.Jato.Companhia)
                .Include(r => r.ListaExtras)
                .Where(c => c.Cliente.Id == cliente.Id).ToList();

            return View(viagens);

        }


        /// <summary>
        /// Responsável por redireccionar o utilizador para a página de pesquisa de ofertas
        /// </summary>
        /// <returns>Retorna a view da procura de ofertas</returns>
        public IActionResult ProcurarOfertas()
        {

            return View();

        }


        public IActionResult SelecionarDestino(int id, DateTime data)
        {

            //todo validar

            ViewData["id"] = id;
            ViewData["data"] = data;

            return View();

        }

        /// <summary>
        /// Responsável por redireccionar o utilizador para a página de pesquisa de ofertas
        /// </summary>
        /// <returns>Retorna a view da procura de ofertas</returns>
        [HttpPost]
        public IActionResult ProcurarOfertas(int id)
        {
            //TODO: verificar se o o id existe, redirecionar para a lista de jatos disponíveis.

            var viagens = _context.Aeroporto.Select(c => c).ToList();
            
            return View(viagens);

        }

        /// <summary>
        /// Responsável por redireccionar o utilizador para a página de pesquisa de ofertas
        /// </summary>
        /// <returns>Retorna a view da procura de ofertas</returns>
        [HttpGet]
        public IActionResult VerJatos(int idpartida, int iddestino, DateTime data)
        {
            //todo: validar dados

            IEnumerable<Jato> jatos = _context.Jato.Include(c => c.ListaDisponibilidade)
                .Include(c => c.Modelo)
                .Include(c => c.Modelo.TipoJato)
                .Include(c => c.Companhia)
                .Include(c => c.Aeroporto).ToList();
            jatos = jatos
                .Where(c => JatoDisponivelIntervalo(c.JatoId, idpartida, iddestino , data) != TipoDisponibilidade.NaoDisponivel).ToList();

            Aeroporto aeroportoPartida = _context.Aeroporto
                .Single(a => a.AeroportoId == idpartida);

            Aeroporto aeroportoDestino = _context.Aeroporto
                .Single(a => a.AeroportoId == iddestino);

            VerJatoViewModel viewModel = new VerJatoViewModel()
            {
                AeroportoPartidaId = aeroportoPartida.AeroportoId,
                AeroportoDestinoId = aeroportoDestino.AeroportoId,
                DataPartida = data,
                JatodDisponiveis = jatos
            };

            return View(viewModel);

        }
        

        /// <summary>
        /// Responsável por redireccionar o utilizador para a página que apresenta a informação de uma oferta
        /// </summary>
        /// <returns>Retorna a view das ofertas</returns>
        public IActionResult VerOferta(int idpartida, int iddestino, DateTime data, int jatoid)
        {

            try
            {
                Aeroporto aeroportoPartida = _context.Aeroporto.Single(a => a.AeroportoId == idpartida);
                Aeroporto aeroportoChegada = _context.Aeroporto.Single(a => a.AeroportoId == iddestino);

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

                int estrelas;

                if (reservasAvalidas == 0)
                {
                    estrelas = 5;
                }
                else
                {
                    estrelas = Reservas.Where(c => c.Avaliacao >= 0 && c.Avaliacao < 6).Select(c => c.Avaliacao).Sum() / reservasAvalidas;
                }

                VerOfertaViewModel verOfertaViewModel = new VerOfertaViewModel()
                {
                    Jato = Jato,
                    AeroportoChegada = aeroportoChegada,
                    AeroportoPartida = aeroportoPartida,
                    DataPartida = data,
                    Estrelas = estrelas,
                    Kilometros = DistanciaEntreCoordenadas(aeroportoPartida.Latitude,
                                            aeroportoPartida.Longitude,
                                            aeroportoChegada.Latitude,
                                            aeroportoChegada.Longitude)
                };


                return View(verOfertaViewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

        }

        private bool filtrarExtras(Extra extra, List<int> extrasids)
        {
            foreach (int id in extrasids)
            {
                if (extra.ExtraId == id)
                {
                    return true;
                }
            }
            return false;

        }

        private double calcularCusto(double distancia, Jato jato, List<int> extrasids)
        {
            ICollection<Extra> extras = _context.Extra.Where(e => filtrarExtras(e, extrasids)).ToList();

            double custo = jato.CreditosBase + 
                jato.CreditosPorKilometro * distancia + 
                Double.Parse(extras.Select(e => e.Valor).Sum().ToString());

            return custo;
        }



        /// <summary>
        /// Responsável por redireccionar o utilizador para a página de reserva concluída
        /// </summary>
        /// <returns>Retorna de reserva concluída</returns>
        [HttpGet]
        public IActionResult ReservaConcluida(int idpartida, int iddestino, DateTime data, int jatoid, List<int> extrasids)
        {
            try
            {
                //TODO: remover disponibilidades
                

                //Aeroporto partida = _context.Aeroporto.Single(a => a.AeroportoId == id);
                string idCliente = _userManager.GetUserAsync(this.User).Result.Id;

                Aeroporto aeroportoPartida = _context.Aeroporto.Single(a => a.AeroportoId == idpartida);
                Aeroporto aeroportoChegada = _context.Aeroporto.Single(a => a.AeroportoId == iddestino);

                Jato jato = _context.Jato
                    .Include(a => a.ListaDisponibilidade)
                    .Include(a => a.Aeroporto)
                    .Single(a => a.JatoId == jatoid);

                TipoDisponibilidade disponibilidade = JatoDisponivelIntervalo(jatoid, idpartida, iddestino, data);

                if (disponibilidade == TipoDisponibilidade.NaoDisponivel)
                {
                    throw new Exception("O jato não está disponível");
                }

                double distancia = DistanciaEntreCoordenadas(aeroportoPartida.Latitude,
                    aeroportoPartida.Longitude,
                    aeroportoChegada.Latitude,
                    aeroportoChegada.Longitude);

                double custo = calcularCusto(distancia, jato, extrasids);


                Cliente cliente = _context.Cliente.Include(c => c.ContaDeCreditos).Single(c => c.Id == idCliente);

                //todo: subtrair aos creditos dos cliente todos os custos das reservas nao pagas associadas a  este
                if (double.Parse(cliente.ContaDeCreditos.JetCashActual.ToString()) < custo)
                {
                    throw new Exception("Os créditos não são suficientes");
                }

                Reserva reserva = new Reserva()
                {
                    AeroportoDestinoId = aeroportoChegada.AeroportoId,
                    AeroportoPartidaId = aeroportoPartida.AeroportoId,
                    Aprovada = false,
                    Custo = decimal.Parse(custo.ToString()),
                    DataPartida = data,
                    JatoId = jatoid,
                    Paga = false,
                    Realizada = false,
                    ApplicationUserId = idCliente,
                };

                _context.Reserva.Add(reserva);

                Notificacao notificacaoCliente = new Notificacao()
                {
                    Lida = false,
                    UtilizadorId = idCliente,
                    Mensagem = "Efetuou recentemente uma reserva, aguarde validação",
                    Tipo = Notificacao.TYPE_SUCCESS,
                    
                };
                
                _context.Notificacao.Add(notificacaoCliente);
                _context.SaveChanges();

                return View("ReservaConcluida");
        
            }
            catch (Exception ex)
            {
                ViewData["erro"] = ex.Message; 
                return View("ReservaNaoConcluida");
            }
        }
    }

}

