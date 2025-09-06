using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface ICartItemRepository
    {
        Task<bool> ExistsByUserFood(string? idFood, string idUser);
        Task<bool> ExistsByFoodId(string idFood);
        Task Add(CartItemModel cartItem);
        Task<CartItemModel?> GetById(string id);
        Task<List<CartItemModel>?> GetAllByUserId(string idUser);
        Task Update(CartItemModel cartItem);
        Task Delete(CartItemModel cartItem);
    }
}
