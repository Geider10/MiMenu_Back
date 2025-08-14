namespace MiMenu_Back.Data.DTOs.Voucher
{
    public class VoucherAddDto
    {
        public string IdCategory { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Discount { get; set; }
        public int BuyMinimum { get; set; }
        public bool Visibility { get; set; }
        public string DueDate { get; set; }
        public string CreateDate { get; set; }
    }
}
