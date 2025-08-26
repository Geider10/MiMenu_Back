using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Payment;
using MercadoPago.Resource.Preference;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MiMenu_Back.Data.DTOs.Payment;
using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;

namespace MiMenu_Back.Services
{
    public class PaymentService
    {
        private readonly IPaymentMapper _paymentMap;
        private readonly IPaymentRepository _paymentRepo;
        private readonly IUserRepository _userRepo;
        public PaymentService(IUserRepository userRepo, IPaymentMapper paymentMap, IPaymentRepository paymentRepo)
        {
            _paymentMap = paymentMap;
            _paymentRepo = paymentRepo;
            _userRepo = userRepo;
        }
        public async Task<ResponsePreferenceDto> CreatePreference(CreatePreferenceDto preferenceDto)
        {
            var user = await _userRepo.GetById(preferenceDto.IdUser);
            if (user == null) throw new MainException("User no found", 404);

            var payment = await AddPayment(preferenceDto.IdUser);
            var itemsPreference = _paymentMap.ListDtoToListItem(preferenceDto.ItemsCart);
            var payerReference = _paymentMap.UserToPayer(user);
            var request = new PreferenceRequest
            {
                Items = itemsPreference,
                Payer = payerReference,
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
                ExternalReference = payment.Entity.Id.ToString(),
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
            //Buscar el pagoMP por id, en el servidor de MP
            //si el pagoMP esta aprobado,
            //Buscar la transaccion por id y actualizar el estado a parobado
            //luego crear order y order items
            if (messageDto.type == "payment")
            {
                string id = messageDto.data.id;
                var client = new PaymentClient();
                Payment paymentMP = await client.GetAsync(Convert.ToInt64(id));
                if (paymentMP.Status == "approved")
                {
                    //var payment = _paymentRepo.
                }
            }
        }
        private async Task<EntityEntry<PaymentModel>> AddPayment(string idUser)
        {
            var payment = new PaymentModel
            {
                IdUser = Guid.Parse(idUser),
                Status = PaymentStatusEnum.Pending,
                PaymentMethod = "Mercado Pago",
                Currency = "ARS",
                CreateDate = DateTime.Now,
                IdPublicMP = "123123123"
            };
            var pay = await _paymentRepo.Add(payment);
            return pay;
        }
    }
}
