namespace MiMenu_Back.Data.DTOs.CartItem
{
    public class CartItemGetAllDto
    {
        public string IdItem { get; set; }
        public string Name { get; set; }
        public string? ImgUrl { get; set; }
        public int Quantity { get; set; }
        public decimal PriceUnit{ get; set; }
    }
}
