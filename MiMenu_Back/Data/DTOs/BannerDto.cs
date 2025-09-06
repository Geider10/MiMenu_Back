namespace MiMenu_Back.Data.DTOs
{
    public class BannerAddDto
    {
        public string Description { get; set; }
        public int Priority { get; set; }
        public string? ImgUrl { get; set; }
        public bool visibility { get; set; }
    }
    public class BannerGetAllDto
    {
        public string Id { get; set; }
        public string? ImgUrl { get; set; }
    }
    public class BannerGetOneDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string? ImgUrl { get; set; }
        public bool Visibility { get; set; }
    }
    public class BannerQueryDto
    {
        public string? SortPriority { get; set; }
        public bool? Visibility { get; set; }
    }
    public class BannerUpdateDto
    {
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}
