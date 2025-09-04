namespace MiMenu_Back.Data.DTOs.Voucher
{
    public class VoucherUpdateDto
    {
        public string Name { get; set; }
        public int Discount { get; set; }
        public decimal BuyMinimum { get; set; }
        public string DueDate { get; set; }
    }
}
