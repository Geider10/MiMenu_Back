namespace MiMenu_Back.Data.Models
{
    public class VoucherModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Discount { get; set; }
        public int BuyMinimum { get; set; }
        public bool Visibility { get; set; }
        public DateOnly DueDate { get; set; }
        public DateOnly CreateDate { get; set; }

        public ICollection<ItemVoucherModel> ItemsVoucher { get; set; }
        public VoucherModel()
        {
            Id = Guid.NewGuid();
        }

    }
}
