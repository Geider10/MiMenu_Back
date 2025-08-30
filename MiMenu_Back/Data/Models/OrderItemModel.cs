namespace MiMenu_Back.Data.Models
{
    public class OrderItemModel
    {
        public Guid Id { get; set; }
        public Guid IdOrder { get; set; }
        public Guid IdFood { get; set; }
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
        public decimal PriceTotal { get; set; }

        public OrderModel Order { get; set; }
        public FoodModel Food { get; set; }
        public OrderItemModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
