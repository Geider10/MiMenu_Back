namespace MiMenu_Back.Data.DTOs.Voucher
{
    public class VoucherGetByIdDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Discount { get; set; }
        public decimal BuyMinimum { get; set; }
        public bool Visibility { get; set; }
        public string DueDate { get; set; }
        public string CreateDate { get; set; }
    }
}
