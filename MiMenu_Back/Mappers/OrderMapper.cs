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
    }
}
