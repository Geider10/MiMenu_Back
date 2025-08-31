using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Utils;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface IOrderMapper
    {
        OrderModel AddToOrder(string idUser, string idPayment, string idPublic, TypeOrderEnum type, StatusOrderEnum status, TimeOnly retirementTime, string retirementInstruction, DateOnly createDate);
        OrderItemModel AddToOrderItem(string idOrder, string idFood, int quantity, decimal priceUnit, decimal priceTotal);
        List<CartItemGetDto> ItemToListDetails(List<OrderItemModel> itemsList);
        List<OrderGetAllDto> ItemToListGeneral(List<OrderModel> itemList, Util util);
    }
}
