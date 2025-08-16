namespace MiMenu_Back.Data.Models
{
    public class ItemVoucherModel
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdVoucher { get; set; }

        public UserModel User { get; set; }
        public VoucherModel Voucher { get; set; }
        public ItemVoucherModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
