namespace MiMenu_Back.Data.DTOs.Payment
{
    public class PaymentGetDto
    {
        public string IdPublic { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Total { get; set; }
        public string CreateDate { get; set; }
    }
}
