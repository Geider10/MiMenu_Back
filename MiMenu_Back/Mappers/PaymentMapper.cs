using MercadoPago.Client.Common;
using MercadoPago.Client.Preference;
using MiMenu_Back.Data.DTOs.CartItem;
using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.DTOs.Payment;
using MiMenu_Back.Data.DTOs.User;
using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class PaymentMapper : IPaymentMapper
    {
        public List<PreferenceItemRequest> ListDtoToListItem(List<CartItemGetAllDto> listDto)
        {
            var listItems = new List<PreferenceItemRequest>();
            foreach (var item in listDto)
            {
                listItems.Add(new PreferenceItemRequest
                {
                    Id = item.IdFood,
                    Title = item.Name,
                    Quantity = item.Quantity,
                    CurrencyId = "ARS",
                    UnitPrice = item.PriceUnit,
                });
            }
            return listItems;
        }
        public PreferencePayerRequest UserToPayer(UserModel user)
        {
            return new PreferencePayerRequest
            {
                Name = user.Name,
                Email = user.Email,
                Phone = new PhoneRequest
                {
                    AreaCode = "+54",
                    Number = user.Phone
                }
            };
        }
        public PaymentModel AddToPayment(StatusPaymentEnum status, string currency, decimal total, string idPublic)
        {
            return new PaymentModel
            {
                Status = status,
                Currency = currency,
                Total = total,
                IdPublic = idPublic,
            };
        }
        public PaymentModel UpdateToPayment(StatusPaymentEnum status, DateTime? dateApproved, string paymentMethod, PaymentModel payment)
        {
            payment.Status = status;
            payment.ApprovedDate = dateApproved;
            payment.PaymentMethod = paymentMethod;
            return payment;
        }
        public PaymentGetDto PaymentToGetDto(PaymentModel payment, string status, string createDate)
        {
            return new PaymentGetDto
            {
                IdPublic = payment.IdPublic,
                Status = status,
                PaymentMethod = payment.PaymentMethod,
                Total = payment.Total,
                CreateDate = createDate
            };
        }
    }
}
