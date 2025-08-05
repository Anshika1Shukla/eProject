using ePizzaHub.Infrastructure.Models;
using ePizzaHub.Models.ApiModels.Response;

namespace ePizzaHub.Core.Mappers
{
    public static class CartMappingExtension
    {

        public static CartResponseModel ConvertToCartResponseModel(
            this Cart cartDetails)
        {
            CartResponseModel cartData
                 = new CartResponseModel()
                 {
                     Id = cartDetails.Id,
                     UserId = cartDetails.UserId,
                     CreatedDate = cartDetails.CreatedDate,
                 };

            cartData.Items = cartDetails
                              .CartItems
                              .Select(x => new CartItemResponseModel
                              {
                                  Id = x.Id,
                                  ItemId = x.ItemId,
                                  ImageUrl = x.Item.ImageUrl,
                                  ItemName = x.Item.Name,
                                  Quantity = x.Quantity,
                                  UnitPrice = x.UnitPrice
                              })
                              .ToList();


            cartData.Total = cartData.Items.Sum(x => x.Quantity * x.UnitPrice);
            cartData.Tax = Math.Round(cartData.Total * 0.05m, 2);
            cartData.GrandTotal = cartData.Total + cartData.Tax;
            return cartData;
        }
    }
}
