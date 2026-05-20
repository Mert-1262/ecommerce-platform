using ECommerce.WebUI.Models;
using ECommerce.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiService _apiService;

        public AuthController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var response =
                    await _apiService.PostAsync<LoginResponse>(
                        "auth/login",
                        request);

                if (response == null || string.IsNullOrEmpty(response.Token))
                {
                    ViewBag.Error =
                        "Email veya şifre hatalı.";

                    return View(request);
                }

                HttpContext.Session.SetString(
                    "JWToken",
                    response.Token);

                HttpContext.Session.SetString(
                    "UserRole",
                    response.Role ?? "User");

                TempData["Success"] =
                    "Giriş başarılı.";

                return RedirectToAction(
                    "Index",
                    "Products");
            }
            catch (Exception)
            {
                ViewBag.Error =
                    "Email veya şifre hatalı.";

                return View(request);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                await _apiService.PostAsync<string>(
                    "auth/register",
                    request);

                TempData["Success"] =
                    "Kayıt başarılı. Giriş yapabilirsiniz.";

                return RedirectToAction("Login");
            }
            catch
            {
                ViewBag.Error =
                    "Kayıt işlemi başarısız.";

                return View(request);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}