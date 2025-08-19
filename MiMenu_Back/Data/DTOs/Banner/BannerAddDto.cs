using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiMenu_Back.Data.DTOs.Banner
{
    public class BannerAddDto
    {
        public string Description { get; set; }
        public int Priority { get; set; }
        public string? ImgUrl { get; set; }
        public bool visibility { get; set; }
    }   
}
