using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace ePizzaHub.UI.Controllers
{
    [Route("Cart")]
    public class CartController : BaseController
    {



        Guid CartId
        {
            get
            {
                Guid id;
                string cartId = Request.Cookies["CartId"];
                if (cartId == null)
                {
                    id = Guid.NewGuid();
                    Response.Cookies.Append(
                        "CartId", id.ToString(),
                        new CookieOptions()
                        {
                            Expires = DateTime.Now.AddDays(2)
                        });
                }
                else
                {
                    id = Guid.Parse(cartId);
                }
                return id;
            }
        }




        [HttpGet("Index")]
        public IActionResult Index()
        {

            // create api to get current cart details

            return View();
        }


        public async Task<IActionResult> AddToCart(int itemId, decimal unitPrice,int quantity)
        {




            return View();
        }
    }
}
