using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AirUberProjeto.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using AirUberProjeto.Models;
using AirUberProjeto.Models.AccountViewModels;
using AirUberProjeto.Models.AutenticacaoViewModels;
using AirUberProjeto.Services;
using MailKit.Net.Smtp;
using MimeKit;

namespace AirUberProjeto.Controllers
{
    [Authorize]
    public class AutenticacaoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly AirUberDbContext _context;

        public AutenticacaoController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory,
            AirUberDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<AutenticacaoController>();
            _context = context;

     

            var res = _userManager.FindByEmailAsync("helpdesk@airuber.com");

            if (res.Result == null)
            {

                Helpdesk h = new Helpdesk
                {
                    Nome = "Helpdesk",
                    Apelido = "Helpdesk",
                    Email = "helpdesk@airuber.com",
                    UserName = "helpdesk@airuber.com"
                };

                var result = _userManager.CreateAsync(h, "ost:43636/Acc").Result;
                if (result.Succeeded)
                {


                    _userManager.AddToRoleAsync(h, Roles.ROLE_HELPDESK).Wait();

                }

            }

        }

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
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

                    foreach (var r in roles)
                    {

                        if (r.ToString() == Roles.ROLE_COLABORADOR_ADMIN)
                        {
                            return RedirectToAction(nameof(HomeController.ColaboradorLogin), "Home");

                        }

                        if (r.ToString() == Roles.ROLE_HELPDESK)
                        {
                            return RedirectToAction(nameof(HelpdeskController.Index), "Helpdesk");

                        }

                        if (r.ToString() == Roles.ROLE_CLIENTE)
                        {
                            return RedirectToAction(nameof(HomeController.ClienteLogin), "Home");

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
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        //
        // GET: /Account/RegisterCliente
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterCliente(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCliente(RegisterClienteViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new Cliente { UserName = model.Email, Email = model.Email, Jetcash = 0};
                var result = await _userManager.CreateAsync(user, model.Password);// cria um user com a pw
                if (result.Succeeded)
                {


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
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCompanhia(RegisterCompanhiaViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {

                Companhia c = new Companhia
                {
                    Contact = model.Contact,
                    PaisId = model.PaisId,
                    Morada = model.Morada,
                    Nif = model.Nif,
                    Nome = model.Nome,
                    Jetcash = 0

                };

                

                var user = new Colaborador{ UserName = model.Email, Email = model.Email, IsAdministrador = true, Companhia = c};
                var result = await _userManager.CreateAsync(user, model.Password);// cria um user com a pw
                if (result.Succeeded)
                {


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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

     

        // GET: /Account/ConfirmEmail
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
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
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
                    return View("Register");
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
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
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
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async void ChangePassword(ChangePasswordViewModel model)
        {
            //TODO: TIago acabar isto
            if (!ModelState.IsValid)
            {
                //return View(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                  //  return RedirectToAction(nameof(Index), new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                //return View(model);
            }
           // return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }



        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

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

        #endregion
    }
}
