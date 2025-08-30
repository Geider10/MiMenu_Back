using MiMenu_Back.Data.DTOs.CartItem;
using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

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
    }
}
