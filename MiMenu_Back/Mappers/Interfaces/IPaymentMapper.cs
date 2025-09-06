using MercadoPago.Client.Preference;
using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface IPaymentMapper
    {
        List<PreferenceItemRequest> ListDtoToListItem(List<CartItemGetDto> listDto);
        PreferencePayerRequest UserToPayer(UserModel userModel);
        PaymentModel AddToPayment(StatusPaymentEnum status, string currency, decimal total, string idPublic);
        PaymentModel UpdateToPayment(StatusPaymentEnum status, DateTime? dateApproved, string paymentMethod, PaymentModel payment);
        PaymentGetDto PaymentToGetDto(PaymentModel payment, string status, string createDate);
    }
}
