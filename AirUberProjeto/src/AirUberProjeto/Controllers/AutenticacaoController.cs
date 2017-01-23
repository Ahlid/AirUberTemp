using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AirUberProjeto.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using AirUberProjeto.Models;
using AirUberProjeto.Models.AutenticacaoViewModels;
using AirUberProjeto.Services;
using MailKit.Net.Smtp;
using MimeKit;

namespace AirUberProjeto.Controllers
{
    /// <summary>
    /// Controlador responsável pela autenticação dos utilizadores web do sistema.
    /// </summary>
    [Authorize]
    public class AutenticacaoController : Controller
    {
        /// <summary>
        /// User manager que vai permitir utilizar metodos feitos pela windows de forma a controlar os user.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;
        /// <summary>
        /// Manager responsavel pelo tratamento das sessões dos utilizadores.
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager;
        /// <summary>
        /// Server para enviar os emails.
        /// </summary>
        private readonly IEmailSender _emailSender;
        /// <summary>
        /// Logger para cada ação.
        /// </summary>
        private readonly ILogger _logger;
        /// <summary>
        /// O contexto da aplicação para poder aceder a dados.
        /// </summary>
        private readonly AirUberDbContext _context;

        /// <summary>
        /// Construtor do controlador
        /// </summary>
        /// <param name="userManager">O user manager a usar para o controlo de utilizadores</param>
        /// <param name="signInManager">O sign in manager a usar para o controlo de sessões</param>
        /// <param name="emailSender">O email sender a usar para enviar os emails</param>
        /// <param name="loggerFactory">O logger a usar para guardar ações no sistema</param>
        /// <param name="context">O contexto da aplicação</param>
        public AutenticacaoController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILoggerFactory loggerFactory,
            AirUberDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = loggerFactory.CreateLogger<AutenticacaoController>();
            _context = context;

     

            var res = this._userManager.FindByEmailAsync("helpdesk@airuber.com");

            if (res.Result == null)
            {

                Helpdesk h = new Helpdesk
                {
                    Nome = "Helpdesk",
                    Apelido = "Helpdesk",
                    Email = "helpdesk@airuber.com",
                    UserName = "helpdesk@airuber.com"
                };

                var result = this._userManager.CreateAsync(h, "ost:43636/Acc").Result;
                if (result.Succeeded)
                {


                    this._userManager.AddToRoleAsync(h, Roles.ROLE_HELPDESK).Wait();

                }

            }

            

        }

        //
        // GET: /Account/Login
        /// <summary>
        /// Método responsavel por devover a pagina de login
        /// </summary>
        /// <param name="returnUrl">O url de retorno que é a pagina de onde veio</param>
        /// <returns>A view para efectuar login</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        /// <summary>
        /// Método responsável pelo pedido de login do utilizador, verificando os dados e autorizando ou não o login.
        /// </summary>
        /// <param name="model">O login model com os dados necessarios para o login</param>
        /// <param name="returnUrl">O url de retorno que é a pagina de onde veio</param>
        /// <returns>A View Login caso seja inválido, um redirecionamento caso seja login válido</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");


                    var user = await _userManager.FindByEmailAsync(model.Email);
                    // Get the roles for the user
                    var roles = await _userManager.GetRolesAsync(user);
                    
                    foreach (var role in roles)
                    {

                        switch (role.ToString())
                        {
                            case Roles.ROLE_COLABORADOR_ADMIN:
                                return RedirectToAction(nameof(CompanhiaController.Index), "Companhia");

                            case Roles.ROLE_COLABORADOR:
                                return RedirectToAction(nameof(CompanhiaController.Index), "Companhia");

                            case Roles.ROLE_HELPDESK:
                                return RedirectToAction(nameof(HelpdeskController.Index), "Helpdesk");

                            case Roles.ROLE_CLIENTE:
                                return RedirectToAction(nameof(ClienteController.Perfil), "Cliente");

                        }
                        
                    }


                }
                
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        /// <summary>
        /// Metodo responsável pelos pedidos da pagina de registo.
        /// </summary>
        /// <param name="returnUrl">O url de retorno que é a pagina de onde veio</param>
        /// <returns>A view de resgisto</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        //
        // GET: /Account/RegisterCliente
        /// <summary>
        /// Método responsável pelo pedido da pagina de registo de cliente.
        /// </summary>
        /// <param name="returnUrl">O url de retorno que é a pagina de onde veio</param>
        /// <returns>A view de registo do cliente</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterCliente(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Register
        /// <summary>
        /// Método responsável pelo tratamento de dados para um registo de cliente 
        /// </summary>
        /// <param name="model">Os dados para o registo do cliente</param>
        /// <param name="returnUrl">O url de retorno que é a pagina de onde veio</param>
        /// <returns>Se o registo oi valido retorna um redirecionamento para a home page, caso contrario retorna a view de Registo de Cliente</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCliente(RegisterClienteViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                ContaDeCreditos conta = new ContaDeCreditos();
                Cliente user = new Cliente { Nome = model.Nome, Apelido = model.Apelido,  UserName = model.Email, Email = model.Email, ContaDeCreditos = conta};
                var result = await _userManager.CreateAsync(user, model.Password);// cria um user com a pw
                if (result.Succeeded)
                {
                    Notificacao notificacao1 = new Notificacao() {Mensagem = "Bem vindo à plataforma", Tipo = Notificacao.TYPE_INFO, Lida = false, UtilizadorId = user.Id};
                    _context.Notificacao.Add(notificacao1);

                    Notificacao notificacao2 = new Notificacao() { Mensagem = "Confirme o seu email", Tipo = Notificacao.TYPE_WARNING, Lida = false, UtilizadorId = user.Id };
                    _context.Notificacao.Add(notificacao2);
                   

                    await _userManager.AddToRoleAsync(user, Roles.ROLE_CLIENTE);//atribui a role

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Action("ConfirmEmail", "Autenticacao", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                    await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                        $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");

                    await _signInManager.SignInAsync(user, isPersistent: false);//para ele depois fazer login, regista-se e fica logo loged-in

                    _logger.LogInformation(3, "User created a new account with password.");

                    return RedirectToLocal(returnUrl);// redirecionar para o homeloged in? sim xD
                    
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/RegisterCompanhia
        /// <summary>
        /// Método responsável pelo pedido da pagina de registo de companhia.
        /// </summary>
        /// <param name="returnUrl">O url de retorno que é a pagina de onde veio</param>
        /// <returns>A view de registo de companhia.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterCompanhia(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["Paises"] = new SelectList(_context.Set<Pais>(), "PaisId", "Nome");
            return View();
        }


        //
        // POST: /Account/RegisterCompanhia
        /// <summary>
        /// Método responsável pelo tratamento de dados para o registo de uma companhia.
        /// </summary>
        /// <param name="model"> Os dados para o registo da companhia. </param>
        /// <param name="returnUrl">O url de retorno que é a pagina de onde veio.</param>
        /// <returns>A view de registo caso os dados sejam inválidos, um redrecionamento para a home page caso sejam válidos</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCompanhia(RegisterCompanhiaViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                ContaDeCreditos conta = new ContaDeCreditos();
                Companhia companhia = new Companhia
                {
                    Contact = model.Contact,
                    PaisId = model.PaisId,
                    Morada = model.Morada,
                    Nif = model.Nif,
                    Nome = model.Nome,
                    Email = model.Email, //added -> Tive que acrescentar
                    ContaDeCreditos = conta
                };
                 

                Colaborador user = new Colaborador{ Nome = "Colaborador", Apelido = "Admin", UserName = model.Email, Email = model.Email, IsAdministrador = true, Companhia = companhia};
                var result = await _userManager.CreateAsync(user, model.Password);// cria um user com a pw
                if (result.Succeeded)
                {

                    Notificacao notificacao1 = new Notificacao() { Mensagem = "Bem vindo à plataforma", Tipo = Notificacao.TYPE_INFO, Lida = false, UtilizadorId = user.Id };
                    _context.Notificacao.Add(notificacao1);

                    Notificacao notificacao2 = new Notificacao() { Mensagem = "Confirme o seu email", Tipo = Notificacao.TYPE_WARNING, Lida = false, UtilizadorId = user.Id };
                    _context.Notificacao.Add(notificacao2);

                    await _userManager.AddToRoleAsync(user, Roles.ROLE_COLABORADOR_ADMIN);//atribui a role

                    
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Autenticacao", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                        $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                    await _signInManager.SignInAsync(user, isPersistent: false);//para ele depois fazer login, regista-se e fica logo loged-in
                    _logger.LogInformation(3, "User created a new account with password.");
                    return RedirectToLocal(returnUrl);// redirecionar para o homeloged in? sim xD
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }





        //
        // POST: /Account/LogOff
        /// <summary>
        /// Método responsável pelo fecho de sessão
        /// </summary>
        /// <returns>Um redirecionamento para a home page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

     

        // GET: /Account/ConfirmEmail
        /// <summary>
        /// Método de devolve a página de veriicação de email.
        /// </summary>
        /// <param name="userId"> O id do utilizador a veriica o email.</param>
        /// <param name="code">O code de veriicação do email do utilizador.</param>
        /// <returns>Uma view error caso os dados sejam inválidos, uma view com ação sucedida caso contrario.</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                user.EmailConfirmed = true;
            }
                


            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        /// <summary>
        /// Método responsável por tratar de um pedido da pagina de esquecimento de password.
        /// </summary>
        /// <returns>A view de esquecimento de password.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        /// <summary>
        /// Método responsável pelo tratamento dos dados para um pedido de esquecimento de password.
        /// </summary>
        /// <param name="model">Os dados para o esquecimento de passowrd.</param>
        /// <returns>Caso os dados sejam válidos retorna a view de esquecimento de password, caso contratio a view de confirmação de esquecimento de password.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null )
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }



                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Autenticacao", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                   $"Please reset your password by clicking here: <a href=\"{callbackUrl}\">link</a>");
                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        /// <summary>
        /// Método que devovle a página de confirmação de esquecimento a password.
        /// </summary>
        /// <returns>View de confirmação de esquecimento a password.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        /// <summary>
        /// Metodo que devolve a página de reset password.
        /// </summary>
        /// <param name="code">Código de reset.</param>
        /// <returns>A view reset se o codigo for valido, a view de erro caso contrario</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        /// <summary>
        /// Método que trata dos dados de alteração de password.
        /// </summary>
        /// <param name="model">O modelo de dados para a mudança da password.</param>
        /// <returns>A view de mudança da password se inválido, um redirecionamento para a view de reset password confirmation caso válido </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(AutenticacaoController.ResetPasswordConfirmation), "Autenticacao");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(AutenticacaoController.ResetPasswordConfirmation), "Autenticacao");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        /// <summary>
        /// Método que devovle a view de confirmação da mudança de password.
        /// </summary>
        /// <returns>A view de reset password confirmation.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        /// <summary>
        /// Método de devovle a view para mudar a password.
        /// </summary>
        /// <returns>View de mudança de passwor.</returns>
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Manage/ChangePassword
        /// <summary>
        /// Método que recebe os dados da mudança de password e muda.
        /// </summary>
        /// <param name="model">O modelo de dados a usar.</param>
        /// <returns>Um redirecionamento caso os dados sejam validos, a view de mudança de password caso contrário.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            //TODO: TIago acabar isto
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync(); 
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    var roles = await _userManager.GetRolesAsync(user);

                    foreach (var role in roles)
                    {

                        switch (role.ToString())
                        {
                            case Roles.ROLE_COLABORADOR_ADMIN:
                                return RedirectToAction(nameof(HomeController.ColaboradorLogin), "Home");

                            case Roles.ROLE_COLABORADOR:
                                return RedirectToAction(nameof(HomeController.ColaboradorLogin), "Home");

                            case Roles.ROLE_HELPDESK:
                                return RedirectToAction(nameof(HelpdeskController.Index), "Helpdesk");

                            case Roles.ROLE_CLIENTE:
                                return RedirectToAction(nameof(HomeController.ClienteLogin), "Home");

                        }

                    }
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        /// <summary>
        /// Metodo que devovle uma página de conirmação de reset de password.
        /// </summary>
        /// <param name="email">email a fazer o reset a password.</param>
        /// <returns>A view de confirmação de reset.</returns>
        [HttpGet]
        [Authorize(Roles = Roles.ROLE_HELPDESK)]
        public IActionResult ConfirmReset(String email)
        {
            ViewBag.email = email;
            return View();
        }

        /// <summary>
        /// Método que gera uma nova password a um utilizador.
        /// </summary>
        /// <param name="email">email do utilizador a gerar nova password.</param>
        /// <returns>A view de gerar uma nova password.</returns>
        [HttpPost]
        [Authorize(Roles = Roles.ROLE_HELPDESK)]
        public async Task<IActionResult> GenerateNewPassword(String email)
        {
            String newPassword = CreatePassword(8);
            var user =  await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await _userManager.RemovePasswordAsync(user);

                await _userManager.AddPasswordAsync(user, newPassword);

                    await _emailSender.SendEmailAsync(email, "Reset Password",
                        "A sua password foi alterada por um administrador para " + newPassword);
                    ViewBag.result = 1;
                    return View();
                
                

            }
            ViewBag.result = 0;
            return View();
        }




        #region Helpers
        /// <summary>
        /// Método que adiciona erros
        /// </summary>
        /// <param name="result">Os erros atuais</param>

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        /// <summary>
        /// Método que devovle o utilizador atual.
        /// </summary>
        /// <returns>O utilizador atual.</returns>
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
        /// <summary>
        /// Método que cira um redirecionamento.
        /// </summary>
        /// <param name="returnUrl">o url a redirecionar.</param>
        /// <returns>Um redirecionamento para o url recebido.</returns>
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        /// <summary>
        /// Método que gera uma nova password random
        /// </summary>
        /// <param name="length">o tamanho da password</param>
        /// <returns>A password gerada</returns>
        private string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString() + "_!hA";
        }

        #endregion
    }
}
