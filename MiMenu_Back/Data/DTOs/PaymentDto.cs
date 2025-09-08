namespace MiMenu_Back.Data.DTOs
{
    public class CreatePreferenceDto
    {
        public string IdUser { get; set; }
        public OrderAddDto Order { get; set; }
        public PaymentAddDto Payment { get; set; }
        public List<CartItemGetDto> ItemsCart { get; set; }
    }
    public class PaymentAddDto
    {
        public string Currency { get; set; }
        public decimal Total { get; set; }
    }
    public class PaymentGetDto
    {
        public string IdPublic { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Total { get; set; }
        public string CreateDate { get; set; }
    }
    public class ResponsePreferenceDto
    {
        public string IdPreference { get; set; }
        public string InitPoint { get; set; }
    }
    public class WebHookDto
    {
        public string action { get; set; }
        public string api_version { get; set; }
        public DataMP data { get; set; }
        public string date_created { get; set; }
        public long id { get; set; }
        public bool live_mode { get; set; }
        public string type { get; set; }
        public string user_id { get; set; }
    }
    public class DataMP
    {
        public string id { get; set; }
    }
    public class WebhookParamsDto
    {
        public string data_id { get; set; }
        public string type { get; set; }
    }
}
