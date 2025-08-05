using ePizzaHub.Core.Contracts;
using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Models.ApiModels.Request;
using ePizzaHub.Models.ApiModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contract;

namespace ePizzaHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase

    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) {
            _cartService = cartService;
        }
        [HttpGet]
        [Route("get-item-count")]
        public async Task<IActionResult> GetCartItemCount(Guid guid)
        {
            var itemCount = await _cartService.GetCartItemCountAsync(guid);
            //ApiResponseModel<int> responseModel
            //    = new ApiResponseModel<int>(true, itemCount, "Record Feacted");

            return Ok(itemCount);
        }

        [HttpGet]
        [Route("get-cart-details")]
        public async Task<IActionResult> GetCartDetailsAsync(Guid cartId)
        { 

            var cartDetails = await _cartService.GetCartDetailsAsync(cartId);
            return Ok(cartDetails);
        }


        [HttpPost]
        [Route("add-item-to-cart")]
        public async Task<IActionResult> AddItemToCart(AddToCartRequest request)
        {
            var cartDetails = await _cartService.AddItemsToCart(request);

            return Ok(cartDetails);
        }


        [HttpPut]
        [Route("delete-item")]
        public async Task<IActionResult> DeleteItem()
        {
            return Ok();
        }

        [HttpPut]
        [Route("update-item")]
        public async Task<IActionResult> UpdateItem()
        {
            return Ok();
        }

        [HttpPut]
        [Route("update-cart-user")]
        public async Task<IActionResult> UpdateCartUser()
        {
            return Ok();
        }

    }
}
