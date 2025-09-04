using MiMenu_Back.Data.Enums;

namespace MiMenu_Back.Data.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TypeCategoryEnum Type { get; set; }
        public bool Visibility { get; set; }

        public ICollection<FoodModel> Foods { get; set; }
        public CategoryModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
