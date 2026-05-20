using System.Net.Http;
using ECommerce.WebUI.Models;
using ECommerce.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
namespace ECommerce.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApiService _apiService;

        public ProductsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(
     string? search,
     int? categoryId)
        {
            string? token =
                HttpContext.Session.GetString("JWToken");

            string url = "products";

            if (!string.IsNullOrEmpty(search))
            {
                url += $"?search={search}";
            }

            var products =
                await _apiService.GetAsync<List<ProductModel>>(
                    url,
                    token);
            products ??= new List<ProductModel>();

            CampaignModel? activeCampaign =
                await _apiService.GetAsync<CampaignModel>("campaigns/active");

            if (!string.IsNullOrEmpty(search))
            {
                products = products
                    .Where(x =>
                        x.Name.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            ViewBag.Search = search;
            ViewBag.ActiveCampaign = activeCampaign;

            return View(products);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            string? token =
                HttpContext.Session.GetString("JWToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            if (quantity < 1)
            {
                quantity = 1;
            }

            AddCartItemRequest request = new()
            {
                ProductId = productId,
                Quantity = quantity
            };

            try
            {
                await _apiService.PostAsync<string>(
                    "cart/items",
                    request,
                    token,
                    ensureSuccess: true);

                TempData["Message"] = "Ürün sepete eklendi.";
            }
            catch (HttpRequestException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}