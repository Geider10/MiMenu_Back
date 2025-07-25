using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;
using System.Data;
using System.Globalization;
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
        public async Task<List<OrderGetDto>> GetAllByUserId(string idUser)
        {
            var orderList = await _orderRepo.GetAllByUserId(idUser);
            if (orderList == null || orderList.Count == 0) throw new MainException("There are no order for this user", 404);

            var dtoList = _orderMap.OrderListToGetList(orderList);
            return dtoList;
        }
        public async Task Update(string idOrder, string idUser, OrderUpdateDto orderDto)
        {
            var orderModel = await _orderRepo.GetById(idOrder);
            if (orderModel == null) throw new MainException("Order no found", 404);

            if (orderModel.IdUser != Guid.Parse(idUser)) throw new MainException("Order must be from user", 403);
            var orderUpdate = _orderMap.UpdateToOrderModel(orderModel, orderDto);
            await _orderRepo.Update(orderUpdate);
        }
    }
}
