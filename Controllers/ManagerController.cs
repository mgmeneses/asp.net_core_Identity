using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using loginIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using loginIdentity.Models.ManagerViewModel;

namespace loginIdentity.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;   
        public ManagerController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Nao foi possivel carregar o usuário com o ID '{_userManager.GetUserId(User)}'");
            }

            var model = new IndexViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Index(IndexViewModel model)
        
        {
            if (!ModelState.IsValid)
            {
                return View(model);
                
            }
            
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Nao foi possivel carregar o usuário com o ID '{_userManager.GetUserId(User)}'");
            }

            //Validando se o E-mail foi alterado
            var email = user.Email;

            if (email !=model.Email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                
                if (setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Erro inesperado ao atribuir um email ao usuário com o ID '{user.Id}'");
                }
            }

            //Validando se o Telefone foi alterado
            var phoneNumber = user.PhoneNumber;

            if (phoneNumber !=model.PhoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Erro inesperado ao atribuir um telefone ao usuário com o ID '{user.Id}'");
                }
            }

            StatusMessage = "Seu Perfil foi atualizado";

            return RedirectToAction(nameof(Index));

        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ApplicationException($"Não foi possível carregar os dados do usuário com Id {_userManager.GetUserId(User)}.");
            }

            var model = new ChangePasswordViewModel {StatusMessage = StatusMessage};

            return View(model);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);                
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ApplicationException($"Não foi possível carregar os dados do usuário com Id {_userManager.GetUserId(User)}.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "Sua Senha foi alterada com sucesso!";
            return RedirectToAction(nameof(ChangePassword));
        }
    }
}