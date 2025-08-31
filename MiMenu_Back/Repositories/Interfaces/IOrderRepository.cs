using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task Add(OrderModel order);
        Task<OrderModel?> GetByIdPublic(string idPublic);
        Task<OrderModel?> GetById(string id);
        Task Update(OrderModel order);
        Task AddOrderItem(OrderItemModel orderItem);
        Task<List<OrderItemModel>?> GetAllByOrderId(string idOrder);
        Task<List<OrderModel>?> GetAllByUserId(string idUser, string? typeOrder);
    }
}
