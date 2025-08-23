using MercadoPago.Client.Common;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using MiMenu_Back.Data.DTOs.Payment;

namespace MiMenu_Back.Services
{
    public class PaymentService
    {
        public async Task<ResponsePreferenceDto> CreatePreference(CreatePreferenceDto preferenceDto)
        {
            var itemsPreference = new List<PreferenceItemRequest>();
            foreach (var item in preferenceDto.ItemsCart)
            {
                itemsPreference.Add(new PreferenceItemRequest
                {
                    Id = item.Id,
                    Title = item.Name,
                    Description = item.Description,
                    Quantity = item.Quantity,
                    CurrencyId = "ARS",
                    UnitPrice = (decimal)item.Price
                });
            }
            var request = new PreferenceRequest
            {
                Items = itemsPreference,
                Payer = new PreferencePayerRequest
                {
                    Name = "Pepito",
                    Email = "pepe@gmail.com",
                    Phone = new PhoneRequest
                    {
                        AreaCode = "+54",
                        Number = "1111222233"
                    }

                },
                //AdditionalInfo = "Discount 4000.00"
            };
            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);

            return new ResponsePreferenceDto
            {
                IdPreference = preference.Id,
                InitPoint = preference.InitPoint
            };
        }
    }
}
