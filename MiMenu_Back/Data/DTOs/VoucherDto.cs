namespace MiMenu_Back.Data.DTOs
{
    public class VoucherAddDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Discount { get; set; }
        public decimal BuyMinimum { get; set; }
        public bool Visibility { get; set; }
        public string DueDate { get; set; }
    }
    public class VoucherUpdateDto
    {
        public string Name { get; set; }
        public int Discount { get; set; }
        public decimal BuyMinimum { get; set; }
        public string DueDate { get; set; }
    }
    public class VoucherGetAllDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal BuyMinimum { get; set; }
        public string DueDate { get; set; }
    }
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
    public class VoucherQueryDto
    {
        public string? SortName { get; set; }
        public bool? Visibility { get; set; }
        public bool? Expired { get; set; }
    }
}
