using System.Net.Http;
using ECommerce.WebUI.Models;
using ECommerce.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class AdminOrdersController : Controller
    {
        private readonly ApiService _apiService;

        public AdminOrdersController(ApiService apiService)
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

            List<AdminOrderModel>? orders =
                await _apiService.GetAsync<List<AdminOrderModel>>(
                    "orders/admin/all",
                    token);

            return View(orders ?? new List<AdminOrderModel>());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int orderId, string orderStatus)
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
                    $"orders/admin/{orderId}/status",
                    new { orderStatus },
                    token,
                    ensureSuccess: true);

                TempData["Message"] = "Sipariş ve kargo durumu güncellendi.";
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
