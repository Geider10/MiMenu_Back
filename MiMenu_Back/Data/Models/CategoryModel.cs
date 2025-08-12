namespace MiMenu_Back.Data.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Visibility { get; set; }

        public ICollection<FoodModel> Foods { get; set; }
        public ICollection<VoucherModel> Vouchers { get; set; }
        public CategoryModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
