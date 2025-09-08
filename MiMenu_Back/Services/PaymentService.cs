using MercadoPago.Client.Payment;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Payment;
using MercadoPago.Resource.Preference;
using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;
using Newtonsoft.Json;
using System.Security.Cryptography.Xml;
namespace MiMenu_Back.Services
{
    public class PaymentService
    {
        private readonly IPaymentMapper _paymentMap;
        private readonly IPaymentRepository _paymentRepo;
        private readonly IUserRepository _userRepo;
        private readonly ICartItemRepository _ciRepo;
        private readonly Util _util;
        private readonly OrderService _orderService;
        private readonly CartItemService _ciService;
        public PaymentService(IUserRepository userRepo, IPaymentMapper paymentMap, IPaymentRepository paymentRepo, ICartItemRepository ciRepo, Util util, OrderService orderService, CartItemService ciService)
        {
            _paymentMap = paymentMap;
            _paymentRepo = paymentRepo;
            _userRepo = userRepo;
            _ciRepo = ciRepo;
            _util = util;
            _orderService = orderService;
            _ciService = ciService;
        }
        public async Task<ResponsePreferenceDto> CreatePreference(CreatePreferenceDto preferenceDto)
        {
            UserModel? user = await _userRepo.GetById(preferenceDto.IdUser);
            if (user == null) throw new MainException("User no found", 404);

            string idPublic = await AddPayment(preferenceDto.Payment, preferenceDto.IdUser);
            var itemsPreference = _paymentMap.ListDtoToListItem(preferenceDto.ItemsCart);
            var payerReference = _paymentMap.UserToPayer(user);
            Dictionary<string, object> metaDataPreference = new Dictionary<string, object>
            {
                {"idUser", preferenceDto.IdUser },
                {"order", JsonConvert.SerializeObject(preferenceDto.Order)},
                {"itemsCart", JsonConvert.SerializeObject(preferenceDto.ItemsCart)}
            };
            PreferenceRequest request = new PreferenceRequest
            {
                Items = itemsPreference,
                Payer = payerReference,
                PaymentMethods = new PreferencePaymentMethodsRequest
                {
                    ExcludedPaymentTypes = [ 
                        new PreferencePaymentTypeRequest{ Id = "ticket"},
                        new PreferencePaymentTypeRequest{ Id = "credit_card"},
                        new PreferencePaymentTypeRequest{ Id = "debit_card"},
                        new PreferencePaymentTypeRequest{ Id = "prepaid_card"}
                        ],
                    DefaultPaymentMethodId = "account_money",
                },
                //AdditionalInfo = "Discount 4000.00",
                ExternalReference = idPublic,
                Metadata = metaDataPreference
            };
            PreferenceClient client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);
            return new ResponsePreferenceDto
            {
                IdPreference = preference.Id,
                InitPoint = preference.InitPoint
            };
        }
        public async Task ReceiveWebhook (WebHookDto webhookDto, WebhookParamsDto webhookParams, string xRequest, string xSignature)
        {
            ValidateSecretKey(xRequest, xSignature, webhookParams.data_id);
            if (webhookDto.type == "payment")
            {
                string id = webhookDto.data.id;
                PaymentClient client = new PaymentClient();
                Payment paymentMP = await client.GetAsync(Convert.ToInt64(id));
                if (paymentMP.Status == "approved")
                {
                    string idPayment = await UpdatePayment(paymentMP.ExternalReference, paymentMP.DateApproved);
                    string idUser = (string)paymentMP.Metadata["id_user"];
                    OrderAddDto orderDto = JsonConvert.DeserializeObject<OrderAddDto>(paymentMP.Metadata["order"].ToString());
                    List<CartItemGetDto> itemsCart = JsonConvert.DeserializeObject<List<CartItemGetDto>>(paymentMP.Metadata["items_cart"].ToString());

                    await _orderService.AddOrder(idUser,idPayment,orderDto,itemsCart);
                    await _ciService.DeleteAllByUserId(idUser);
                }
            }
        }
        private async Task<string> AddPayment(PaymentAddDto payment,string idUser)
        {
            List<CartItemModel>? itemsCart = await _ciRepo.GetAllByUserId(idUser);
            decimal totalCart = 0;
            foreach (CartItemModel item in itemsCart)
            {
                decimal totalProduct = item.PriceUnit * item.Quantity;
                totalCart += totalProduct;
            }
            if (totalCart != payment.Total) throw new MainException("Total order is incorrect, the value is not expected", 422);
            
            string idPublic = Guid.NewGuid().ToString();
            PaymentModel paymentModel = _paymentMap.AddToPayment(StatusPaymentEnum.Pending, payment.Currency, payment.Total, idPublic);
            await _paymentRepo.Add(paymentModel);

            return idPublic;
        }
        private async Task<string> UpdatePayment(string idPublic, DateTime? dateApproved)
        {
            PaymentModel? paymentModel = await _paymentRepo.GetByIdPublic(idPublic);
            if (paymentModel == null) throw new MainException("Payment no found", 404);
            if (paymentModel.Status == StatusPaymentEnum.Approved) throw new MainException("Payment already approved", 409);

            PaymentModel paymentUpdated = _paymentMap.UpdateToPayment(StatusPaymentEnum.Approved, dateApproved, "Mercado Pago", paymentModel);
            await _paymentRepo.Update(paymentUpdated);

            return paymentModel.Id.ToString();
        }
        public async Task<PaymentGetDto> GetById(string id)
        {
            PaymentModel? paymentModel = await _paymentRepo.GetById(id);
            if (paymentModel == null) throw new MainException("Payment no found", 404);

            string status = _util.FormatStatusPayment(paymentModel.Status);
            string createDate = _util.FormatDateTime(paymentModel.CreateDate);
            PaymentGetDto paymentDto = _paymentMap.PaymentToGetDto(paymentModel, status, createDate);
            return paymentDto;
        }
        private void ValidateSecretKey(string xRequest, string xSignature, string dataId)
        {
            string[] signatureSplit = xSignature.Split(",");
            string ts = signatureSplit[0].Substring(3);
            string keyMP = signatureSplit[1].Substring(3);
            string template = "id:"+dataId+";request-id:"+xRequest+";ts:"+ts+";";

            string encryptedSignature = _util.GenerateCounterKey(template);

            if (encryptedSignature != keyMP) throw new MainException("HMAC verification failed", 422);
        }
    }
}
