using MiMenu_Back.Data.DTOs.CartItem;
using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.DTOs.Payment;
using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Utils;

namespace MiMenu_Back.Mappers
{
    public class OrderMapper : IOrderMapper
    {
        public OrderModel AddToOrder(string idUser, string idPayment, string idPublic, TypeOrderEnum type, StatusOrderEnum status , TimeOnly retirementTime, string retirementInstruction, DateOnly createDate)
        {
            return new OrderModel
            {
                IdUser = Guid.Parse(idUser),
                IdPayment = Guid.Parse(idPayment),
                Type = type,
                Status = status,
                RetirementTime = retirementTime,
                RetirementInstruction = retirementInstruction,
                IdPublic = idPublic,
                CreateDate = createDate
            };
        }
        public OrderItemModel AddToOrderItem(string idOrder, string idFood, int quantity, decimal priceUnit, decimal priceTotal)
        {
            return new OrderItemModel
            {
                IdOrder = Guid.Parse(idOrder),
                IdFood = Guid.Parse(idFood),
                Quantity = quantity,
                PriceUnit = priceUnit,
                PriceTotal = priceTotal
            };
        }

        public List<CartItemGetDto> ItemToListDetails(List<OrderItemModel> itemsList)
        {
            List<CartItemGetDto> detailList = new List<CartItemGetDto>();
            foreach(var item in itemsList)
            {
                detailList.Add(new CartItemGetDto
                {
                    IdItem = item.Id.ToString(),
                    Quantity = item.Quantity,
                    PriceUnit = item.PriceUnit,
                    Food = new FoodDetailDto
                    {
                        IdFood = item.Food.Id.ToString(),
                        Name = item.Food.Name,
                        Description = item.Food.Description,
                        ImgUrl = item.Food.ImgUrl,
                        Discount = item.Food.Discount
                    }
                });
            }
            return detailList;
        }
        public List<OrderGetAllDto> ItemToListGeneral(List<OrderModel> itemList, Util util)
        {
            List<OrderGetAllDto> generalList = new List<OrderGetAllDto>();
            foreach (var order in itemList)
            {
                int quantityItems = 0;
                foreach(var item in order.OrderItems)
                {
                    quantityItems += item.Quantity;
                }
                generalList.Add(new OrderGetAllDto
                {
                    IdOrder = order.Id.ToString(),
                    QuantityItems = quantityItems,
                    PriceTotal = order.Payment.Total,
                    OrderGeneral = new OrderGeneralDto
                    {
                        Type = util.FormatTypeOrder(order.Type),
                        Status = util.FormatStatusOrder(order.Status),
                        RetirementTime = util.FormatTimeOnly(order.RetirementTime),
                        CreateDate = util.FormatDateOnly(order.CreateDate)
                    }
                });
            }
            return generalList;
        }
        public OrderGetDto OrderToOrderDto(OrderModel order, Util util)
        {
            return new OrderGetDto
            {
                IdOrder = order.Id.ToString(),
                Payment = new PaymentGetDto
                {
                    IdPublic = order.Payment.IdPublic,
                    Status = util.FormatStatusPayment(order.Payment.Status),
                    PaymentMethod = order.Payment.PaymentMethod,
                    Total = order.Payment.Total,
                    CreateDate = util.FormatDateTime(order.Payment.CreateDate),
                },
                OrderDetail = new OrderDetailDto
                {
                    IdPublic = order.IdPublic,
                    Type = util.FormatTypeOrder(order.Type),
                    Status = util.FormatStatusOrder(order.Status),
                    RetirementTime = util.FormatTimeOnly(order.RetirementTime),
                    RetirementInstruction = order.RetirementInstruction,
                    CreateDate = util.FormatDateOnly(order.CreateDate)
                },
                ItemsDetail = ItemToListDetails(order.OrderItems.ToList())
            };
        }
    }
}
