namespace MiMenu_Back.Data.DTOs.Order
{
    public class OrderAddDto
    {
        public string IdUser { get; set; }
        public string IdFood { get; set; }
        public int Quantity { get; set; }
        public double PriceTotal { get; set; }
    }
}
