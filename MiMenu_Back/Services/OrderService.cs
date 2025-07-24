using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories.Interfaces;

namespace MiMenu_Back.Services
{
    public class OrderService
    {
        private readonly IOrderMapper _orderMap;
        private readonly IOrderRepository _orderRepo;
        public OrderService(IOrderMapper orderMap, IOrderRepository orderRepo)
        {
            _orderMap = orderMap;
            _orderRepo = orderRepo;
        }
        public async Task Add(OrderAddDto order)
        {
            var orderModel = _orderMap.GetToOrderModel(order);
            await _orderRepo.Add(orderModel);
        }
    }
}
