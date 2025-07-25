using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> ExistsByUserFood(string idFood, string idUser);
        Task Add(OrderModel order);
        Task<OrderModel?> GetById(string id);
        Task<List<OrderModel>?> GetAllByUserId(string idUser);
    }
}
