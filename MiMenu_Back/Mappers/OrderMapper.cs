using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class OrderMapper : IOrderMapper
    {
        public OrderModel GetToOrderModel(OrderAddDto order)
        {
            return new OrderModel
            {
                IdFood = Guid.Parse(order.IdFood),
                IdUser = Guid.Parse(order.IdUser),
                Quantity = order.Quantity,
                PriceTotal = order.PriceTotal
            };
        }
    }
}
