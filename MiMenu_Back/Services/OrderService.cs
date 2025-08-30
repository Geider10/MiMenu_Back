using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Data.Enums;
using MiMenu_Back.Utils;
using MiMenu_Back.Mappers.Interfaces;
namespace MiMenu_Back.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderMapper _orderMap;
        private readonly Util _util;
        public OrderService(IOrderRepository orderRepo, IOrderMapper orderMap, Util util)
        {
            _orderRepo = orderRepo;
            _orderMap = orderMap;
            _util = util;
        }
        public async Task AddOrder (string idUser,string idPayment, OrderAddDto orderDto, List<CartItemGetDto> itemsDto)
        {
            TypeOrderEnum typeOrder = _util.FormatTypeOrder(orderDto.Type);
            TimeOnly retirementTime = _util.FormatTimeOnly(orderDto.RetirementTime);
            string idPublic = Guid.NewGuid().ToString();
            DateOnly createDate = _util.CreateDateCurrent();

            var orderModel = _orderMap.AddToOrder(idUser, idPayment, idPublic, typeOrder, StatusOrderEnum.Pending, retirementTime, orderDto.RetirementInstruction, createDate);
            await _orderRepo.Add(orderModel);
            await AddOrderItem(idPublic, itemsDto);
        }
        public async Task UpdateStatus (string id)
        {
            var orderModel = await _orderRepo.GetById(id);
            if (orderModel == null) throw new MainException("Order no found", 404);

            StatusOrderEnum statusNext = _util.NextStatusOrder(orderModel.Status);
            orderModel.Status = statusNext;

            await _orderRepo.Update(orderModel);
        }
        private async Task AddOrderItem (string idPublic, List<CartItemGetDto> itemsDto)
        {
            var orderModel = await _orderRepo.GetByIdPublic(idPublic);
            if (orderModel == null) throw new MainException("Order no found", 404);

            foreach(var item in itemsDto)
            {
                decimal priceTotal = item.PriceUnit * item.Quantity;
                var itemModel = _orderMap.AddToOrderItem(orderModel.Id.ToString(), item.Food.IdFood, item.Quantity, item.PriceUnit, priceTotal);
                await _orderRepo.AddOrderItem(itemModel);
            }
        }
    }
}
