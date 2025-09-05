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

            OrderModel orderModel = _orderMap.AddToOrder(idUser, idPayment, idPublic, typeOrder, StatusOrderEnum.Pending, retirementTime, orderDto.RetirementInstruction, createDate);
            await _orderRepo.Add(orderModel);
            await AddOrderItem(idPublic, itemsDto);
        }
        public async Task UpdateStatus (string id)
        {
            OrderModel? orderModel = await _orderRepo.GetById(id);
            if (orderModel == null) throw new MainException("Order no found", 404);

            StatusOrderEnum statusNext = _util.NextStatusOrder(orderModel.Status);
            orderModel.Status = statusNext;

            await _orderRepo.Update(orderModel);
        }
        public async Task<List<OrderGetAllDto>> GetAllByUserId(string idUser, OrderQueryDto queryDto)
        {
            List<OrderModel>? ordersList = await _orderRepo.GetAllByUserId(idUser,queryDto.TypeOrder);
            if (ordersList == null || ordersList.Count == 0) throw new MainException("There are no orders for this user", 404);
            foreach(OrderModel order in ordersList)
            {
                List<OrderItemModel> itemsList = await GetAllByOrderId(order.Id.ToString());
                order.OrderItems = itemsList;
            }

            if (queryDto.OldMonth.HasValue)
            {
                DateOnly oldDate = _util.CreateDateOld((int)queryDto.OldMonth);
                ordersList = ordersList.FindAll(o =>
                {
                    int dateValidate = _util.CompareDates(oldDate, o.CreateDate);//CreateDate order >= OldDate user
                    if (dateValidate >= 0) return true;
                    return false;
                });
            }
            List<OrderGetAllDto> generalList = _orderMap.ItemToListGeneral(ordersList, _util);
            return generalList;
        }
        public async Task<OrderGetDto> GetById(string idOrder,string idUser)
        {
            OrderModel? orderModel = await _orderRepo.GetById(idOrder);
            if (orderModel == null) throw new MainException("Order no found", 404);
            if (orderModel.IdUser.ToString() != idUser) throw new MainException("Order must be the user", 403);

            List<OrderItemModel> itemsList = await GetAllByOrderId(idOrder);
            orderModel.OrderItems = itemsList;
            OrderGetDto orderDto = _orderMap.OrderToOrderDto(orderModel, _util);
            return orderDto;
        }
        private async Task AddOrderItem (string idPublic, List<CartItemGetDto> itemsDto)
        {
            OrderModel? orderModel = await _orderRepo.GetByIdPublic(idPublic);
            if (orderModel == null) throw new MainException("Order no found", 404);

            foreach(CartItemGetDto item in itemsDto)
            {
                decimal priceTotal = item.PriceUnit * item.Quantity;
                OrderItemModel itemModel = _orderMap.AddToOrderItem(orderModel.Id.ToString(), item.Food.IdFood, item.Quantity, item.PriceUnit, priceTotal);
                await _orderRepo.AddOrderItem(itemModel);
            }
        }
        private async Task<List<OrderItemModel>> GetAllByOrderId(string idOrder)
        {
            List<OrderItemModel>? orderItemList = await _orderRepo.GetAllByOrderId(idOrder);
            if (orderItemList == null || orderItemList.Count == 0) throw new MainException("There are no items in this order", 404);

            return orderItemList;
        }


    }
}
