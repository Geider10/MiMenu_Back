using MercadoPago.Client.Common;
using MercadoPago.Client.Preference;
using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.DTOs.User;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class PaymentMapper : IPaymentMapper
    {
        public List<PreferenceItemRequest> ListDtoToListItem(List<CartItemGetDto> listDto)
        {
            var listItems = new List<PreferenceItemRequest>();
            foreach (var item in listDto)
            {
                listItems.Add(new PreferenceItemRequest
                {
                    Id = item.Id,
                    Title = item.Name,
                    Description = item.Description,
                    Quantity = item.Quantity,
                    CurrencyId = "ARS",
                    UnitPrice = (decimal)item.Price
                });
            }
            return listItems;
        }
        public PreferencePayerRequest UserToPayer(UserGetDto user)
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
    }
}
