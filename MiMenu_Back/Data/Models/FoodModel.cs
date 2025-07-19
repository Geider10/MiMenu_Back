namespace MiMenu_Back.Data.Models
{
    public class FoodModel
    {
        public Guid Id { get; set; }
        public Guid IdCategory { get; set; }
        public string Name { get; set; }
        public string  Description { get; set; }
        public string ImgUrl { get; set; }
        public double Price { get; set; }
        public int? Discount { get; set; }

        public CategoryModel Category { get; set; }
        public FoodModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
