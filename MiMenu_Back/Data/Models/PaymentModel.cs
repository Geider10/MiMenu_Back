using MiMenu_Back.Data.Enums;

namespace MiMenu_Back.Data.Models
{
    public class PaymentModel
    {
        public Guid Id { get; set; }
        public PaymentStatusEnum Status { get; set; }
        public string? PaymentMethod { get; set; }
        public string Currency { get; set; }
        public decimal Total { get; set; }
        public string IdPublic { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public PaymentModel()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }
    }
}
