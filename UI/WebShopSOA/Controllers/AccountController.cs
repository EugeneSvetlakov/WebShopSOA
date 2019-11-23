using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebShopSOA.Domain.Entities;
using WebShopSOA.Domain.ViewModels;

namespace WebShopSOA.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, [FromServices] ILogger<AccountController> Logger)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loginResult =
                await _signInManager.PasswordSignInAsync(model.UserName,
                    model.Password, model.RememberMe, false);

            if (!loginResult.Succeeded)
            {
                Logger.LogWarning("Ошибка авторизации пользователя {0}", model.UserName);
                ModelState.AddModelError("", "Ошибка авторизации.");
                return View(model);
            }

            if (Url.IsLocalUrl(model.ReturnUrl)) // если Url локальный
            {
                Logger.LogInformation("Авторизация прошла успешно. Переходим на {0}", model.ReturnUrl);
                return Redirect(model.ReturnUrl); //model.ReturnUrl
            }

            return RedirectToAction("Index", "Home");
        }

        //[HttpPost, ValidateAntiForgeryToken]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterUserViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model, [FromServices] ILogger<AccountController> Logger)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User { UserName = model.UserName, Email = model.Email };

            using (Logger.BeginScope("Регистрация нового пользователя {0}", user.UserName))
            {
                var createUserResult = await _userManager.CreateAsync(user, model.Password);

                if (!createUserResult.Succeeded)
                {
                    Logger.LogWarning("Ошибки регистрации: {0}", string.Join(", ", createUserResult.Errors));
                    foreach (var error in createUserResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        return View(model);
                    }
                }

                Logger.LogInformation("Регистрация прошла успешно.");
                await _userManager.AddToRoleAsync(user, "Users"); // поумолчанию присваиваем роль Users

                await _signInManager.SignInAsync(user, false); // Авторизация под вновь созданным Аккаунтом
            }
            return RedirectToAction("Index", "Home");
        }
    }
}