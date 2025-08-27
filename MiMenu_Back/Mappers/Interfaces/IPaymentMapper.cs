using MercadoPago.Client.Preference;
using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.DTOs.Payment;
using MiMenu_Back.Data.DTOs.User;
using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface IPaymentMapper
    {
        List<PreferenceItemRequest> ListDtoToListItem(List<CartItemGetDto> listDto);
        PreferencePayerRequest UserToPayer(UserModel userModel);
        PaymentModel AddToPayment(PaymentStatusEnum status, string currency, decimal total, string idPublic);
        PaymentModel UpdateToPayment(PaymentStatusEnum status, DateTime? dateApproved, string paymentMethod, PaymentModel payment);
        PaymentGetDto PaymentToGetDto(PaymentModel payment, string status, string createDate);
    }
}
