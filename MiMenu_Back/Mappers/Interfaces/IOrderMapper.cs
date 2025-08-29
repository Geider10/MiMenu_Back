using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface IOrderMapper
    {
        OrderModel AddToOrder(string idUser, string idPayment, string idPublic, TypeOrderEnum type, StatusOrderEnum status, TimeOnly retirementTime, string retirementInstruction, DateOnly createDate);
    }
}
