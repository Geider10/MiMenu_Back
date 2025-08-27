using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Payment;
using MercadoPago.Resource.Preference;
using Microsoft.AspNetCore.Routing.Constraints;
using MiMenu_Back.Data.DTOs.Payment;
using MiMenu_Back.Data.Enums;
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
        private readonly ICartItemRepository _ciRepo;
        private readonly Util _util;
        public PaymentService(IUserRepository userRepo, IPaymentMapper paymentMap, IPaymentRepository paymentRepo, ICartItemRepository ciRepo, Util util)
        {
            _paymentMap = paymentMap;
            _paymentRepo = paymentRepo;
            _userRepo = userRepo;
            _ciRepo = ciRepo;
            _util = util;
        }
        public async Task<ResponsePreferenceDto> CreatePreference(CreatePreferenceDto preferenceDto)
        {
            var user = await _userRepo.GetById(preferenceDto.IdUser);
            if (user == null) throw new MainException("User no found", 404);

            string idPublic = await AddPayment(preferenceDto.Payment, preferenceDto.IdUser);
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
                //AdditionalInfo = "Discount 4000.00",
                ExternalReference = idPublic,
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
            //validar secret key
            if (messageDto.type == "payment")
            {
                string id = messageDto.data.id;
                var client = new PaymentClient();
                Payment paymentMP = await client.GetAsync(Convert.ToInt64(id));
                if (paymentMP.Status == "approved")
                {
                    await UpdatePayment(paymentMP.ExternalReference, paymentMP.DateApproved);
                    //luego crear order y order items
                }
            }
        }
        private async Task<string> AddPayment(PaymentAddDto payment,string idUser)
        {
            var itemsCart = await _ciRepo.GetAllByUserId(idUser);
            double totalCart = 0;
            foreach (var item in itemsCart)
            {
                var totalProduct = item.Food.Price * item.Quantity;
                totalCart += totalProduct;
            }
            if (totalCart != Convert.ToDouble(payment.Total)) throw new MainException("Total order is incorrect, the value is not expected", 400);
            
            string idPublic = Guid.NewGuid().ToString();
            PaymentStatusEnum status = _util.FormatPaymentStatus(payment.Status);

            var paymentModel = _paymentMap.AddToPayment(status, payment.Currency, payment.Total, idPublic);
            await _paymentRepo.Add(paymentModel);

            return idPublic;
        }
        private async Task UpdatePayment(string idPublic, DateTime? dateApproved)
        {
            var payment = await _paymentRepo.GetByIdPublic(idPublic);
            if (payment == null) throw new MainException("Payment no found", 404);

            var paymentUpdated = _paymentMap.UpdateToPayment(PaymentStatusEnum.Approved, dateApproved, "Mercado Pago", payment);
            await _paymentRepo.Update(paymentUpdated);
            Console.WriteLine("The payment this approved");
        }
    }
}
