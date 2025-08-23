using MiMenu_Back.Data.DTOs.Order;

namespace MiMenu_Back.Data.DTOs.Payment
{
    public class CreatePreferenceDto
    {
        public string IdUser { get; set; }
        public List<CartItemGetDto> ItemsCart { get; set; }
    }
}
