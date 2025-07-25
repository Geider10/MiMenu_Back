using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface IOrderMapper
    {
        OrderModel GetToOrderModel(OrderAddDto order);
        OrderGetDto OrderModelToGet(OrderModel order);
        List<OrderGetDto> OrderListToGetList(List<OrderModel> orders);
    }
}
