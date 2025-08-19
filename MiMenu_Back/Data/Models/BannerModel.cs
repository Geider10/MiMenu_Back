namespace MiMenu_Back.Data.Models
{
    public class BannerModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }    
        public int Priority { get; set; }
        public string? ImgUrl { get; set; }
        public bool Visibility { get; set; }
        public BannerModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
