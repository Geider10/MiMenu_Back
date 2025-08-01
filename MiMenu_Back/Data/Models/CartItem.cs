namespace MiMenu_Back.Data.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public Guid? IdFood { get; set; }
        public int Quantity { get; set; }
        public double PriceTotal { get; set; }

        public FoodModel Food { get; set; }
        public UserModel User { get; set; }
        public CartItem()
        {
            Id = Guid.NewGuid();
        }
    }
}
