using MiMenu_Back.Data.DTOs.CartItem;
using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;
using System.Data;
using System.Globalization;
namespace MiMenu_Back.Services
{
    public class CartItemService
    {
        private readonly ICartItemMapper _cartItemMap;
        private readonly ICartItemRepository _cartItemRepo;
        public CartItemService(ICartItemMapper cartItemMap, ICartItemRepository cartItemRepo)
        {
            _cartItemMap = cartItemMap;
            _cartItemRepo = cartItemRepo;
        }
        public async Task Add(CartItemAddDto cartItemDto)
        {
            bool cartItemExists = await _cartItemRepo.ExistsByUserFood(cartItemDto.IdFood, cartItemDto.IdUser);
            if (cartItemExists) throw new MainException("CartItem already exists with this FoodId and UserId", 409);

            decimal priceTotal = cartItemDto.PriceUnit * cartItemDto.Quantity;
            var cartItemModel = _cartItemMap.AddToCartItemModel(cartItemDto, priceTotal);
            await _cartItemRepo.Add(cartItemModel);
        }
        public async Task<CartItemGetDto> GetById(string idCartItem, string idUser)
        {
            var cartItemModel = await _cartItemRepo.GetById(idCartItem);
            if (cartItemModel == null) throw new MainException("CartItem no found", 404);
            if (cartItemModel.IdUser != Guid.Parse(idUser)) throw new MainException("CartItem must be from user", 403);

            var cartItemDto = _cartItemMap.CartItemModelToGet(cartItemModel);
            return cartItemDto;
        }
        public async Task<List<CartItemGetAllDto>> GetAllByUserId(string idUser)
        {
            var cartItemList = await _cartItemRepo.GetAllByUserId(idUser);
            if (cartItemList == null || cartItemList.Count == 0) throw new MainException("There are no cartItem for this user", 404);

            var dtoList = _cartItemMap.ItemsToListDto(cartItemList);
            return dtoList;
        }
        public async Task<List<CartItemGetDto>> GetDetailByUserId(string idUser)
        {
            var cartItemList = await _cartItemRepo.GetAllByUserId(idUser);
            if (cartItemList == null || cartItemList.Count == 0) throw new MainException("There are no cartItem for this user", 404);

            var detailList = _cartItemMap.ItemToListDetails(cartItemList);
            return detailList;
        }
        public async Task Update(string idCartItem, string idUser, CartItemUpdateDto cartItemDto)
        {
            var cartItemModel = await _cartItemRepo.GetById(idCartItem);
            if (cartItemModel == null) throw new MainException("CartItem no found", 404);
            if (cartItemModel.IdUser != Guid.Parse(idUser)) throw new MainException("CartItem must be from user", 403);

            decimal priceTotal = cartItemDto.PriceUnit * cartItemDto.Quantity;
            var cartItemUpdate = _cartItemMap.UpdateToCartItemModel(cartItemModel, cartItemDto, priceTotal);
            await _cartItemRepo.Update(cartItemUpdate);
        }
        public async Task Delete(string idCartItem, string idUser)
        {
            var cartItemModel = await _cartItemRepo.GetById(idCartItem);
            if (cartItemModel == null) throw new MainException("CartItem no found", 404);
            if (cartItemModel.IdUser != Guid.Parse(idUser)) throw new MainException("CartItem must be from user", 403);

            await _cartItemRepo.Delete(cartItemModel);
        }
        public async Task DeleteAllByUserId(string idUser)
        {
            var cartItemList = await _cartItemRepo.GetAllByUserId(idUser);
            if (cartItemList == null || cartItemList.Count == 0) throw new MainException("There are no cartItem for this user", 404);

            foreach(var item in cartItemList)
            {
                await _cartItemRepo.Delete(item);
            }

        }
    }
}
