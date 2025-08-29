using MiMenu_Back.Data.DTOs.CartItem;
using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class CartItemMapper : ICartItemMapper
    {
        public CartItemModel AddToCartItemModel(CartItemAddDto cartItem, decimal priceTotal)
        {
            return new CartItemModel
            {
                IdFood = Guid.Parse(cartItem.IdFood),
                IdUser = Guid.Parse(cartItem.IdUser),
                Quantity = cartItem.Quantity,
                PriceUnit = cartItem.PriceUnit,
                PriceTotal = priceTotal
            };
        }
        public CartItemGetDto CartItemModelToGet(CartItemModel cartItem)
        {
            return new CartItemGetDto
            {
                IdItem = cartItem.Id.ToString(),
                IdFood = cartItem.Food.Id.ToString(),
                Quantity = cartItem.Quantity,
                PriceUnit = cartItem.PriceUnit
            };
        }
        public List<CartItemGetAllDto> ItemsToListDto(List<CartItemModel> cartItems)
        {
            List<CartItemGetAllDto> itemsDtoList = new List<CartItemGetAllDto>();
            
            foreach (var item in cartItems)
            {
                itemsDtoList.Add(new CartItemGetAllDto
                {
                    IdItem = item.Id.ToString(),
                    IdFood = item.Food.Id.ToString(),
                    Name = item.Food.Name,
                    ImgUrl = item.Food.ImgUrl,
                    Quantity = item.Quantity,
                    PriceUnit = item.PriceUnit,
                });
            }
            return itemsDtoList;
        }
        public CartItemModel UpdateToCartItemModel(CartItemModel cartItemModel, CartItemUpdateDto cartItemDto)
        {
            cartItemModel.Quantity = cartItemDto.Quantity;
            cartItemModel.PriceUnit = cartItemDto.PriceUnit;

            return cartItemModel;
        }
    }
}
