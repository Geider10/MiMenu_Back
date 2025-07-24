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
            if (orderExists) throw new MainException("Order already exists with this Food and User", 400);

            var orderModel = _orderMap.GetToOrderModel(order);
            await _orderRepo.Add(orderModel);
        }
        public async Task<OrderGetDto> GetById(string idOrder, string idUser)
        {
            var orderModel = await _orderRepo.GetById(idOrder);
            if (orderModel == null) throw new MainException("Order no found", 404);

            if (orderModel.IdUser != Guid.Parse(idUser)) throw new MainException("Order must be from user", 403);
            var orderDto = _orderMap.OrderModelToGet(orderModel);
            return orderDto;
        }
    }
}
