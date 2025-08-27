namespace MiMenu_Back.Data.DTOs.Payment
{
    public class PaymentAddDto
    {
        public string Status { get; set; }
        public string Currency { get; set; }
        public decimal Total { get; set; }
    }
}
