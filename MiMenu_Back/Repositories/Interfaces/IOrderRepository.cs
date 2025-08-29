using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task Add(OrderModel order);
        Task<OrderModel?> GetById(string id);
        Task Update(OrderModel order);
    }
}
