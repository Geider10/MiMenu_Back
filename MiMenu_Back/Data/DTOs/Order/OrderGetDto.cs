using MiMenu_Back.Data.DTOs.Payment;

namespace MiMenu_Back.Data.DTOs.Order
{
    public class OrderGetDto
    {
        public string IdOrder { get; set; }
        public PaymentGetDto Payment { get; set; }
        public OrderDetailDto OrderDetail { get; set; }
        public List<CartItemGetDto> ItemsDetail { get; set; }
    }
    public class OrderDetailDto
    {
        public string IdPublic { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string RetirementTime { get; set; }
        public string RetirementInstruction { get; set; }
        public string CreateDate { get; set; }
    }

}
