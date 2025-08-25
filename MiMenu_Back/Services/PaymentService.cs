using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Payment;
using MercadoPago.Resource.Preference;
using MiMenu_Back.Data.DTOs.Payment;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Utils;

namespace MiMenu_Back.Services
{
    public class PaymentService
    {
        private readonly UserService _userService;
        private readonly IPaymentMapper _paymentMap;
        public PaymentService(UserService userService, IPaymentMapper paymentMap)
        {
            _userService = userService;
            _paymentMap = paymentMap;
        }
        public async Task<ResponsePreferenceDto> CreatePreference(CreatePreferenceDto preferenceDto)
        {
            var user = await _userService.GetById(preferenceDto.IdUser);
            if (user == null) throw new MainException("User no found", 404);

            var itemsPreference = _paymentMap.ListDtoToListItem(preferenceDto.ItemsCart);
            var payer = _paymentMap.UserToPayer(user);
            //create payment, invocar funcion desde service.
            var request = new PreferenceRequest
            {
                Items = itemsPreference,
                Payer = payer,
                PaymentMethods = new PreferencePaymentMethodsRequest
                {
                    ExcludedPaymentTypes = [ 
                        new PreferencePaymentTypeRequest{ Id = "ticket"},
                        new PreferencePaymentTypeRequest{ Id = "credit_card"},
                        new PreferencePaymentTypeRequest{ Id = "debit_card"},
                        ],
                    DefaultPaymentMethodId = "account_money",
                },
                AdditionalInfo = "Discount 4000.00",
                ExternalReference = "Id del pago",
                StatementDescriptor = "MercadoPago"
            };
            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);
            return new ResponsePreferenceDto
            {
                IdPreference = preference.Id,
                InitPoint = preference.InitPoint
            };
        }
        public async Task ReceiveNotification (MPMessageDto messageDto)
        {
            if(messageDto.type == "payment")
            {
                string id = messageDto.data.id;
                var client = new PaymentClient();
                Payment payment = await client.GetAsync(Convert.ToInt64(id));
                if (payment.Status == "approved")
                {
                    //Console.WriteLine("El pago: " + payment.Id + " esta aprobado");
                }
            }
            //Buscar el pagoMP por id, en el servidor de MP
            //si el pagoMP esta aprobado,
                //Buscar la transaccion por id y actualizar el estado a parobado
                //luego crear order y order items
        }
    }
}
