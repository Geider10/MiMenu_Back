namespace MiMenu_Back.Data.DTOs.Food
{
    public class FoodAddDto
    {
        public string IdCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        public double Price { get; set; }
        public int? Discount { get; set; }
    }
}
