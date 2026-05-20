using ECommerce.WebUI.Models;
using ECommerce.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class AdminCampaignsController : Controller
    {
        private readonly ApiService _apiService;

        public AdminCampaignsController(ApiService apiService)
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

            List<CampaignModel>? campaigns =
                await _apiService.GetAsync<List<CampaignModel>>("campaigns", token);

            return View(campaigns ?? new List<CampaignModel>());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCampaignRequest request)
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
                    "campaigns",
                    request,
                    token,
                    ensureSuccess: true);

                TempData["Message"] = "Kampanya eklendi.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Toggle(int campaignId, bool isActive)
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
                    $"campaigns/{campaignId}/status",
                    new { IsActive = isActive },
                    token,
                    ensureSuccess: true);

                TempData["Message"] = "Kampanya durumu güncellendi.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int campaignId)
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
                    $"campaigns/{campaignId}",
                    token,
                    ensureSuccess: true);

                TempData["Message"] = "Kampanya silindi.";
            }
            catch (Exception ex)
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
