using MiMenu_Back.Data.Enums;

namespace MiMenu_Back.Data.Models
{
    public class PaymentModel
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public PaymentStatusEnum Status { get; set; }
        public string PaymentMethod { get; set; }
        public string Currency { get; set; }
        public double? PaymentTotal { get; set; }
        public string? IdPublicMP { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public UserModel User { get; set; }
        public PaymentModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
