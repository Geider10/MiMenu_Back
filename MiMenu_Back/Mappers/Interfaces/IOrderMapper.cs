using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface IOrderMapper
    {
        CartItem GetToOrderModel(OrderAddDto order);
        OrderGetDto OrderModelToGet(CartItem order);
        List<OrderGetDto> OrderListToGetList(List<CartItem> orders);
        CartItem UpdateToOrderModel(CartItem orderModel, OrderUpdateDto orderDto);
    }
}
