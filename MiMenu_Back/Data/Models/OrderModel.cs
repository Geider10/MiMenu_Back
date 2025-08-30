using MiMenu_Back.Data.Enums;

namespace MiMenu_Back.Data.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdPayment { get; set; }
        public TypeOrderEnum Type { get; set; }
        public StatusOrderEnum Status { get; set; }
        public TimeOnly RetirementTime { get; set; }
        public string RetirementInstruction { get; set; }
        public string IdPublic { get; set; }
        public DateOnly CreateDate  { get; set; }

        public UserModel User { get; set; }
        public PaymentModel Payment { get; set; }
        public ICollection<OrderItemModel> OrderItems { get; set; }
        public OrderModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
