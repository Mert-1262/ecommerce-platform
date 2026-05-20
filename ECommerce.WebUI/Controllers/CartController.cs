using System.Net.Http;
using ECommerce.WebUI.Models;
using ECommerce.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly ApiService _apiService;

        public CartController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            IActionResult? loginRedirect = RedirectIfNotLoggedIn();
            if (loginRedirect != null)
            {
                return loginRedirect;
            }

            string? token = HttpContext.Session.GetString("JWToken");

            CartModel? cart =
                await _apiService.GetAsync<CartModel>("cart", token);

            return View(cart ?? new CartModel());
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int cartItemId)
        {
            IActionResult? loginRedirect = RedirectIfNotLoggedIn();
            if (loginRedirect != null)
            {
                return loginRedirect;
            }

            string? token = HttpContext.Session.GetString("JWToken");

            try
            {
                await _apiService.DeleteAsync(
                    $"cart/items/{cartItemId}",
                    token,
                    ensureSuccess: true);

                TempData["Message"] = "Ürün sepetten silindi.";
            }
            catch (HttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            IActionResult? loginRedirect = RedirectIfNotLoggedIn();
            if (loginRedirect != null)
            {
                return loginRedirect;
            }

            if (quantity < 1)
            {
                return await Remove(cartItemId);
            }

            string? token = HttpContext.Session.GetString("JWToken");

            try
            {
                await _apiService.PutAsync<string>(
                    $"cart/items/{cartItemId}",
                    new { quantity },
                    token,
                    ensureSuccess: true);
            }
            catch (HttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutRequest request)
        {
            IActionResult? loginRedirect = RedirectIfNotLoggedIn();
            if (loginRedirect != null)
            {
                return loginRedirect;
            }

            string? token = HttpContext.Session.GetString("JWToken");

            try
            {
                await _apiService.PostAsync<string>(
                    "orders/checkout",
                    new
                    {
                        paymentMethod = request.PaymentMethod,
                        cargoCompany = request.CargoCompany
                    },
                    token,
                    ensureSuccess: true);

                TempData["Message"] =
                    $"Siparişiniz oluşturuldu. Ödeme: {GetPaymentLabel(request.PaymentMethod)}, Kargo: {GetCargoLabel(request.CargoCompany)}.";
                return RedirectToAction("Index", "Orders");
            }
            catch (HttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        private static string GetPaymentLabel(string paymentMethod) =>
            paymentMethod switch
            {
                "Havale" => "Havale",
                _ => "Kredi Kartı"
            };

        private static string GetCargoLabel(string cargoCompany) =>
            cargoCompany switch
            {
                "Yurtici" => "Yurtiçi Kargo",
                _ => "Aras Kargo"
            };

        private IActionResult? RedirectIfNotLoggedIn()
        {
            string? token = HttpContext.Session.GetString("JWToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            return null;
        }
    }
}
