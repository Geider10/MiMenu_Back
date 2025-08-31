namespace MiMenu_Back.Data.DTOs.Order
{
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
}
