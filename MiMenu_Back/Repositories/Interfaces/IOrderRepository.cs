using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> ExistsByUserFood(string idFood, string idUser);
        Task Add(OrderModel order);
    }
}
