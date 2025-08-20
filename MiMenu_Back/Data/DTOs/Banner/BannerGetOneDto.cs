namespace MiMenu_Back.Data.DTOs.Banner
{
    public class BannerGetOneDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string? ImgUrl { get; set; }
        public bool Visibility { get; set; }
    }
}
