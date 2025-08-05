using ePizzaHub.UI.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace ePizzaHub.UI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardController(IHttpClientFactory httpClientFactory)
        {
           _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ePizzaAPI");

            var items = await client.GetFromJsonAsync<ApiResponseModel<IEnumerable<ItemResponseModel>>>("Item");

            if (items.Success)
            {
                return View(items.Data);
            }
            return View();
        }
    }
}
