namespace MiMenu_Back.Data.Models
{
    public class CartItemModel
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdFood { get; set; }
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
        public decimal PriceTotal { get; set; }

        public FoodModel Food { get; set; }
        public UserModel User { get; set; }
        public CartItemModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
