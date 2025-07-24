namespace MiMenu_Back.Data.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public Guid IdFood { get; set; }
        public Guid IdUser { get; set; }
        public int Quantity { get; set; }
        public double PriceTotal { get; set; }

        public FoodModel Food { get; set; }
        public UserModel User { get; set; }
        public OrderModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
