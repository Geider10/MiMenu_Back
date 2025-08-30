namespace MiMenu_Back.Data.DTOs.CartItem
{
    public class FoodDetailDto
    {
        public string IdFood { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        public int? Discount { get; set; }
    }
}
