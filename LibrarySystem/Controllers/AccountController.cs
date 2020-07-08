using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibrarySystem.Models;
using LibrarySystem.Models.AccountViewModel;
using LibrarySystem.Models.ManageViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibrarySystem.Controllers
{
    //[Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityDBContext> _userManager;
        private readonly SignInManager<IdentityDBContext> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<IdentityDBContext> userManager, SignInManager<IdentityDBContext> signInManager,
            IEmailSender emailSender, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        [TempData]
        public string ErrorMesssage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string _returnURL = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = _returnURL;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel _model, string _returnUrl = null)
        {
            ViewData["ReturnUrl"] = _returnUrl;
            if (ModelState.IsValid)
            {
                var _result = await _signInManager.PasswordSignInAsync(_model.Email, _model.Password, _model.RememberMe, lockoutOnFailure: false);
                if (_result.Succeeded)
                {
                    _logger.LogInformation("User Loggin In {Date.NOW}");
                    return RedirectToLocal(_returnUrl);
                }
                if (_result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(LoginWith2Fa), new { _returnUrl, _model.RememberMe });
                }
                if (_result.IsLockedOut)
                {
                    _logger.LogWarning("User Account Locket Out");
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
                    return View(_model);
                }
            }
            return View(_model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2Fa(bool rememberMe, string _retunrUrl = null)
        {
            var _user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (_user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var _model = new LoginWith2FaViewModel { RememberMe = rememberMe };
            ViewData["ReturnUrl"] = _retunrUrl;

            return View(_model);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> LoginWith2Fa(LoginWith2FaViewModel _model, bool _rememberMe, string _returnUrl = null)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(_model);
        //    }

        //    var _user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        //    if (_user == null)
        //    {
        //        throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    }

        //    var _authenticationCode = _model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);
        //    var _result = await _signInManager.GetTwoFactorAuthenticationUserAsync(_authenticationCode, _rememberMe, _model.RememberMachine);
        //    if (_result.Succeeded)
        //    {
        //        _logger.LogInformation("User with ID {UserId} logged in with 2 factor.", _user.Id);
        //        return RedirectToAction(nameof(Lockout));
        //    }
        //    else if (_result.IsLockedOut)
        //    {
        //        _logger.LogWarning("User with ID {UserId} account locked out.", _user.Id);
        //        return RedirectToAction(nameof(Lockout));
        //    }
        //    else
        //    {
        //        _logger.LogWarning("Invalid authenticator code entered for user with ID {UserId}.", _user.Id);
        //        ModelState.AddModelError(string.Empty, "Invalid Authenticator Code.");
        //        return View();
        //    }
        //}

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithRecoveryCcode(string _returnUrl = null)
        {
            var _user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (_user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication code");
            }

            ViewData["ReturnUrl"] = _returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel _model, string _returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(_model);
            }

            var _user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (_user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var _recoveryCode = _model.RecoveryCode.Replace(" ", string.Empty);
            var _result = await _signInManager.TwoFactorAuthenticatorSignInAsync(_recoveryCode, true, true);
            if (_result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with a recovery code.", _user.Id);
                return RedirectToAction(_returnUrl);
            }
            if (_result.IsLockedOut)
            {
                _logger.LogWarning("Invalid recovery code entered for user with ID {UserId}", _user.Id);
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return View();
            }

            return View(_model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string _returnUrl = null)
        {
            ViewData["ReturnUrl"] = _returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel _model, string _returnUrl = null)
        {
            ViewData["ReturnUrl"] = _returnUrl;
            if (ModelState.IsValid)
            {
                var _user = new IdentityDBContext { UserName = _model.Email, Email = _model.Email };
                var _result = await _userManager.CreateAsync(_user, _model.Password);
                if (_result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var _code = await _userManager.GenerateEmailConfirmationTokenAsync(_user);
                    //var _callbackUrl = Url.EmailConfirmationLink(_user.Id, _code, Request.Scheme);
                    //await _emailSender.SendEmailAsync(_model.Email, _callbackUrl);
                    await _signInManager.SignInAsync(_user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToLocal(_returnUrl);
                }
                AddErrors(_result);
            }
            return View(_model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public IActionResult Logout(string _provider, string _returnUrl = null)
        //{
        //    var _redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { _returnUrl });
        //    var _properties = _signInManager.ConfigureExternalAuthenticationProperties(_provider, _redirectUrl);
        //    return Challenge(_properties, _provider);
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> ExternalLoginCallback(string _returnUrl = null, string _remoteError = null)
        //{
        //    if (_remoteError != null)
        //    {
        //        ErrorMesssage = $"Error from external provider : {_remoteError}";
        //        return RedirectToAction(nameof(Login));
        //    }
        //    var _info = await _signInManager.GetExternalLoginInfoAsync();
        //    if (_info == null)
        //    {
        //        return RedirectToAction(nameof(Login));
        //    }
        //    // Sign in the user with this external login provider if the user already has a login.
        //    var result = await _signInManager.ExternalLoginSignInAsync(_info.LoginProvider, _info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        //    if (result.Succeeded)
        //    {
        //        _logger.LogInformation("User logged in with {Name} provider.", _info.LoginProvider);
        //        return RedirectToLocal(_returnUrl);
        //    }
        //    if (result.IsLockedOut)
        //    {
        //        return RedirectToAction(nameof(Lockout));
        //    }
        //    else
        //    {
        //        // If the user does not have an account, then ask the user to create an account.
        //        ViewData["ReturnUrl"] = _returnUrl;
        //        ViewData["LoginProvider"] = _info.LoginProvider;
        //        var email = _info.Principal.FindFirst(ClaimTypes.Email);
        //        ExternalLoginsViewModel _model = new ExternalLoginsViewModel();
        //        return View("ExternalLogin",  ConfirmEmail = email );
        //    }
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await _signInManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            throw new ApplicationException("Error loading external login information during confirmation.");
        //        }
        //        var user = new IdentityDBContext { UserName = model.Email, Email = model.Email };
        //        var result = await _userManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await _userManager.AddLoginAsync(user, info);
        //            if (result.Succeeded)
        //            {
        //                await _signInManager.SignInAsync(user, isPersistent: false);
        //                _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewData["ReturnUrl"] = returnUrl;
        //    return View(nameof(LibrarySystem.Models.AccountViewModel.ExternalLoginViewModel), model);
        //}

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                string callbackUrl = Url.Page("Manage/ChangePassword.cshtml", user.Id, code, Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            AddErrors(result);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
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
    }
}
        #endregion