namespace MiMenu_Back.Data.DTOs
{
    public class ItemVoucherAddDto
    {
        public string IdUser { get; set; }
        public string IdVoucher { get; set; }
    }
    public class VoucherApplyDto
    {
        public string idItemVoucher { get; set; }
        public string idUser { get; set; }
        public int TotalOrder { get; set; }
    }
    public class VoucherDiscountDto
    {
        public int Discount { get; set; }
        public string TypeDiscount { get; set; }
        public string IdVoucher { get; set; }
    }
}
