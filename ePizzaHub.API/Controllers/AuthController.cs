using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.API.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
