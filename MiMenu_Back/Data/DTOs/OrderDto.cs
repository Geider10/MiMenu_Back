namespace MiMenu_Back.Data.DTOs
{
    public class OrderAddDto
    {
        public string Type { get; set; }
        public string RetirementTime { get; set; }
        public string? RetirementInstruction { get; set; }
    }
    public class OrderGetAllDto
    {
        public string IdOrder { get; set; }
        public int QuantityItems { get; set; }
        public decimal PriceTotal { get; set; }
        public OrderGeneralDto OrderGeneral { get; set; }
    }
    public class OrderGeneralDto
    {
        public string Type { get; set; }
        public string Status { get; set; }
        public string RetirementTime { get; set; }
        public string CreateDate { get; set; }
    }
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
    public class OrderQueryDto
    {
        public string? TypeOrder { get; set; }
        public int? OldMonth { get; set; }
    }
}
