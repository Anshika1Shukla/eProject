using ePizzaHub.Core.Contracts;
using ePizzaHub.Core.Mappers;
using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Models.ApiModels.Request;
using ePizzaHub.Models.ApiModels.Response;
using Repositories.Contract;


namespace ePizzaHub.Core.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<bool> AddItemsToCart(AddToCartRequest request)
        {
            var cartDetails = await _cartRepository.GetCartDetailsAsync(request.CartId);

            if (cartDetails == null)
            {
                var inserted = await AddNewCartAsync(request);
                return inserted > 0;
            }
            else
            {
                return await AddItemsToCart(request, cartDetails);
            }

        }

        public async Task<CartResponseModel> GetCartDetailsAsync(Guid cartId)
        {
            var cartDetails = await _cartRepository.GetCartDetailsAsync(cartId);

            if (cartDetails is not null)
            {
                return cartDetails.ConvertToCartResponseModel();
            }

            return new CartResponseModel();
        }

        public async Task<int> GetCartItemCountAsync(Guid cartId)
        {
            return await _cartRepository.GetCartItemQuantityAsync(cartId);
        }





        private async Task<int> AddNewCartAsync(AddToCartRequest request)
        {

            Cart? cartDetails
                 = new Cart()
                 {
                     Id = request.CartId,
                     UserId = request.UserId,
                     CreatedDate = DateTime.UtcNow,
                     IsActive = true
                 };

            CartItem items
                 = new CartItem()
                 {
                     CartId = request.CartId,
                     ItemId = request.ItemId,
                     Quantity = request.Quantity,
                     UnitPrice = request.UnitPrice
                 };



            cartDetails.CartItems.Add(items);
            await _cartRepository.AddAsync(cartDetails);
            return await _cartRepository.CommitAsync();
        }


        public async Task<bool> AddItemsToCart(
            AddToCartRequest request,
            Cart cartDetails)
        {
            CartItem cartItems
                 = cartDetails.CartItems.FirstOrDefault(x => x.ItemId == request.ItemId);

            if (cartItems == null)
            {
                cartItems = new CartItem
                {
                    CartId = request.CartId,
                    ItemId = request.ItemId,
                    Quantity = request.Quantity,
                    UnitPrice = request.UnitPrice
                };

                cartDetails.CartItems.Add(cartItems);
            }
            else
            {
                cartItems.Quantity += request.Quantity;
            }

            _cartRepository.Update(cartDetails);
            int itemAdded = await _cartRepository.CommitAsync();
            return itemAdded > 0;

        }
    }
}
