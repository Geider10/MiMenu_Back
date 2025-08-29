using MiMenu_Back.Data.DTOs.CartItem;
using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface ICartItemMapper
    {
        CartItemModel AddToCartItemModel(CartItemAddDto cartItem, decimal priceTotal);
        CartItemGetDto CartItemModelToGet(CartItemModel cartItem);
        List<CartItemGetAllDto> ItemsToListDto(List<CartItemModel> cartItems);
        CartItemModel UpdateToCartItemModel(CartItemModel cartItemModel, CartItemUpdateDto cartItemDto);
    }
}
