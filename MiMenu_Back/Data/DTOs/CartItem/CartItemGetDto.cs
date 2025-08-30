using MiMenu_Back.Data.DTOs.CartItem;

namespace MiMenu_Back.Data.DTOs.Order
{
    public class CartItemGetDto
    {
        public string IdItem { get; set; }
        public int Quantity { get; set; }
        public decimal PriceUnit { get; set; }
        public FoodDetailDto Food { get; set; }
    }
}
