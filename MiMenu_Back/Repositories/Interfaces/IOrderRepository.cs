using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> ExistsByUserFood(string? idFood, string idUser);
        Task<bool> ExistsByFoodId(string idFood);
        Task Add(CartItem order);
        Task<CartItem?> GetById(string id);
        Task<List<CartItem>?> GetAllByUserId(string idUser);
        Task Update(CartItem order);
        Task Delete(CartItem order);
    }
}
