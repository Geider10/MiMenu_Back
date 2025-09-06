using MiMenu_Back.Data.DTOs;
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
                Quantity = cartItem.Quantity,
                PriceUnit = cartItem.PriceUnit,
                Food = new FoodDetailDto
                {
                    IdFood = cartItem.Food.Id.ToString(),
                    Name = cartItem.Food.Name,
                    Description = cartItem.Food.Description,
                    ImgUrl = cartItem.Food.ImgUrl,
                    Discount = cartItem.Food.Discount
                }
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
                    Name = item.Food.Name,
                    ImgUrl = item.Food.ImgUrl,
                    Quantity = item.Quantity,
                    PriceUnit = item.PriceUnit,
                });
            }
            return itemsDtoList;
        }
        public List<CartItemGetDto> ItemToListDetails(List<CartItemModel> cartItems)
        {
            List<CartItemGetDto> detailList = new List<CartItemGetDto>();
            foreach (var item in cartItems)
            {
                var ciModel = CartItemModelToGet(item);
                detailList.Add(ciModel);
            }
            return detailList;
        }
        public CartItemModel UpdateToCartItemModel(CartItemModel cartItemModel, CartItemUpdateDto cartItemDto, decimal priceTotal)
        {
            cartItemModel.Quantity = cartItemDto.Quantity;
            cartItemModel.PriceUnit = cartItemDto.PriceUnit;
            cartItemModel.PriceTotal = priceTotal;
            return cartItemModel;
        }
    }
}
