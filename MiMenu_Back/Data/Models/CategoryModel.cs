namespace MiMenu_Back.Data.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CategoryModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
