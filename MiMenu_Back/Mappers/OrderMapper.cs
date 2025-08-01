using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class OrderMapper : IOrderMapper
    {
        public CartItem GetToOrderModel(OrderAddDto order)
        {
            return new CartItem
            {
                IdFood = Guid.Parse(order.IdFood),
                IdUser = Guid.Parse(order.IdUser),
                Quantity = order.Quantity,
                PriceTotal = order.PriceTotal
            };
        }
        public OrderGetDto OrderModelToGet(CartItem order)
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
        public List<OrderGetDto> OrderListToGetList(List<CartItem> orders)
        {
            List<OrderGetDto> orderDtoList = new List<OrderGetDto>();
            
            foreach (var order in orders)
            {
                var orderDto = OrderModelToGet(order);
                orderDtoList.Add(orderDto);
            }
            return orderDtoList;
        }

        public CartItem UpdateToOrderModel(CartItem orderModel, OrderUpdateDto orderDto)
        {
            orderModel.Quantity = orderDto.Quantity;
            orderModel.PriceTotal = orderDto.PriceTotal;

            return orderModel;
        }
    }
}
