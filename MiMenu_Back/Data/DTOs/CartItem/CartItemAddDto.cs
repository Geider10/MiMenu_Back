namespace MiMenu_Back.Data.DTOs.Order
{
    public class CartItemAddDto
    {
        public string IdUser { get; set; }
        public string IdFood { get; set; }
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
    }
}
