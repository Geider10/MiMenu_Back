using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface ICartItemMapper
    {
        CartItem AddToCartItemModel(CartItemAddDto cartItem);
        CartItemGetDto CartItemModelToGet(CartItem cartItem);
        List<CartItemGetDto> CartItemListToGetList(List<CartItem> cartItems);
        CartItem UpdateToCartItemModel(CartItem cartItemModel, CartItemUpdateDto cartItemDto);
    }
}
