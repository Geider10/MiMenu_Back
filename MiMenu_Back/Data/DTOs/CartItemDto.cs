namespace MiMenu_Back.Data.DTOs
{
    public class CartItemAddDto
    {
        public string IdUser { get; set; }
        public string IdFood { get; set; }
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
    }
    public class CartItemGetAllDto
    {
        public string IdItem { get; set; }
        public string Name { get; set; }
        public string? ImgUrl { get; set; }
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
    }
    public class CartItemGetDto
    {
        public string IdItem { get; set; }
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
        public FoodDetailDto Food { get; set; }
    }
    public class CartItemUpdateDto
    {
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
    }
    
}
