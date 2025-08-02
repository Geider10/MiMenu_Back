using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class CartItemMapper : ICartItemMapper
    {
        public CartItem AddToCartItemModel(CartItemAddDto cartItem)
        {
            return new CartItem
            {
                IdFood = Guid.Parse(cartItem.IdFood),
                IdUser = Guid.Parse(cartItem.IdUser),
                Quantity = cartItem.Quantity,
                PriceTotal = cartItem.PriceTotal
            };
        }
        public CartItemGetDto CartItemModelToGet(CartItem cartItem)
        {
            return new CartItemGetDto
            {
                Id = cartItem.Id.ToString(),
                Name = cartItem.Food.Name,
                Description = cartItem.Food.Description,
                ImgUrl = cartItem.Food.ImgUrl,
                Price = cartItem.Food.Price,
                Discount = cartItem.Food.Discount,
                Quantity = cartItem.Quantity,
                PriceTotal = cartItem.PriceTotal
            };
        }
        public List<CartItemGetDto> CartItemListToGetList(List<CartItem> cartItems)
        {
            List<CartItemGetDto> itemsDtoList = new List<CartItemGetDto>();
            
            foreach (var item in cartItems)
            {
                var itemDto = CartItemModelToGet(item);
                itemsDtoList.Add(itemDto);
            }
            return itemsDtoList;
        }

        public CartItem UpdateToCartItemModel(CartItem cartItemModel, CartItemUpdateDto cartItemDto)
        {
            cartItemModel.Quantity = cartItemDto.Quantity;
            cartItemModel.PriceTotal = cartItemDto.PriceTotal;

            return cartItemModel;
        }
    }
}
