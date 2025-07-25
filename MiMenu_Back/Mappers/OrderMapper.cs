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
        public OrderGetDto OrderModelToGet(OrderModel order)
        {
            return new OrderGetDto
            {
                Id = order.Id.ToString(),
                Name = order.Food.Name,
                Description = order.Food.Description,
                ImgUrl = order.Food.ImgUrl,
                Price = order.Food.Price,
                Discount = order.Food.Discount,
                Quantity = order.Quantity,
                PriceTotal = order.PriceTotal
            };
        }
        public List<OrderGetDto> OrderListToGetList(List<OrderModel> orders)
        {
            List<OrderGetDto> orderDtoList = new List<OrderGetDto>();
            
            foreach (var order in orders)
            {
                var orderDto = OrderModelToGet(order);
                orderDtoList.Add(orderDto);
            }
            return orderDtoList;
        }

        public OrderModel UpdateToOrderModel(OrderModel orderModel, OrderUpdateDto orderDto)
        {
            orderModel.Quantity = orderDto.Quantity;
            orderModel.PriceTotal = orderDto.PriceTotal;

            return orderModel;
        }
    }
}
