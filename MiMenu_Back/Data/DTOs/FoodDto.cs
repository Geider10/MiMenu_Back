namespace MiMenu_Back.Data.DTOs
{
    public class FoodAddDto
    {
        public string IdCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        public decimal Price { get; set; }
        public int? Discount { get; set; }
        public bool Visibility { get; set; }
    }
    public class FoodUpdateDto
    {
        public string IdCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? Discount { get; set; }
    }
    public class FoodGetDto
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        public decimal Price { get; set; }
        public int? Discount { get; set; }
        public bool Visibility { get; set; }
    }
    public class FoodQueryDto
    {
        public string? Category { get; set; }
        public string? SortName { get; set; }
        public bool? Visibility { get; set; }
    }
    public class FoodDetailDto
    {
        public string IdFood { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        public int? Discount { get; set; }
    }
}
