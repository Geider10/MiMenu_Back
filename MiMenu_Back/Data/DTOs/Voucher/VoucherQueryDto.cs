namespace MiMenu_Back.Data.DTOs.Voucher
{
    public class VoucherQueryDto
    {
        public string? Category { get; set; }
        public string? SortName { get; set; }
        public bool? Visibility { get; set; }
        public bool? Expired { get; set; }
    }
}
