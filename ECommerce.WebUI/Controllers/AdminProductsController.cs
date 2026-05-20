using System.Net.Http;
using ECommerce.WebUI.Models;
using ECommerce.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class AdminProductsController : Controller
    {
        private readonly ApiService _apiService;

        public AdminProductsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            IActionResult? redirect = EnsureAdmin();
            if (redirect != null)
            {
                return redirect;
            }

            string? token = HttpContext.Session.GetString("JWToken");

            List<ProductModel>? products =
                await _apiService.GetAsync<List<ProductModel>>("products", token);

            return View(products ?? new List<ProductModel>());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStock(int productId, int stock)
        {
            IActionResult? redirect = EnsureAdmin();
            if (redirect != null)
            {
                return redirect;
            }

            if (stock < 0)
            {
                TempData["Error"] = "Stok 0'dan küçük olamaz.";
                return RedirectToAction(nameof(Index));
            }

            string? token = HttpContext.Session.GetString("JWToken");

            try
            {
                await _apiService.PutAsync<string>(
                    $"products/{productId}/stock",
                    new { stock },
                    token,
                    ensureSuccess: true);

                TempData["Message"] = "Stok güncellendi.";
            }
            catch (HttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            IActionResult? redirect = EnsureAdmin();
            if (redirect != null)
            {
                return redirect;
            }

            string? token = HttpContext.Session.GetString("JWToken");

            try
            {
                await _apiService.PostAsync<string>(
                    "products",
                    request,
                    token,
                    ensureSuccess: true);

                TempData["Message"] = "Ürün eklendi.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductRequest request)
        {
            IActionResult? redirect = EnsureAdmin();
            if (redirect != null)
            {
                return redirect;
            }

            string? token = HttpContext.Session.GetString("JWToken");

            try
            {
                await _apiService.PutAsync<string>(
                    "products",
                    request,
                    token,
                    ensureSuccess: true);

                TempData["Message"] = "Ürün güncellendi.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int productId)
        {
            IActionResult? redirect = EnsureAdmin();
            if (redirect != null)
            {
                return redirect;
            }

            string? token = HttpContext.Session.GetString("JWToken");

            try
            {
                await _apiService.DeleteAsync(
                    $"products/{productId}",
                    token,
                    ensureSuccess: true);

                TempData["Message"] = "Ürün silindi.";
            }
            catch (HttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        private IActionResult? EnsureAdmin()
        {
            string? token = HttpContext.Session.GetString("JWToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Products");
            }

            return null;
        }
    }
}
