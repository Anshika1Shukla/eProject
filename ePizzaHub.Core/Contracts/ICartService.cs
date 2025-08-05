using ePizzaHub.Models.ApiModels.Request;
using ePizzaHub.Models.ApiModels.Response;


namespace ePizzaHub.Core.Contracts
{
    public interface ICartService
    {
        Task<int> GetCartItemCountAsync(Guid cartId);

        Task<CartResponseModel> GetCartDetailsAsync(Guid cartId);

        Task<bool> AddItemsToCart(AddToCartRequest request);
    }
}
