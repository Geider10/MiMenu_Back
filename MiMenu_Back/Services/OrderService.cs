using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;
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
            bool orderExists = await _orderRepo.ExistsByUserFood(order.IdFood, order.IdUser);
            if (orderExists) throw new MainException("Order already exists with this idFood and idUser", 400);

            var orderModel = _orderMap.GetToOrderModel(order);
            await _orderRepo.Add(orderModel);
        }
    }
}
