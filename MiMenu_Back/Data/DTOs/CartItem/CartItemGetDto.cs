namespace MiMenu_Back.Data.DTOs.Order
{
    public class CartItemGetDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public double Price { get; set; }
        public int? Discount { get; set; }
        public int Quantity { get; set; }
        public double PriceTotal { get; set; }
    }
}
