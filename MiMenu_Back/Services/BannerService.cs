using MiMenu_Back.Data.DTOs.Banner;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Repositories;
using MiMenu_Back.Utils;

namespace MiMenu_Back.Services
{
    public class BannerService
    {
        private readonly IBannerRepository _bannerRepo;
        public BannerService(IBannerRepository bannerRepo)
        {
            _bannerRepo = bannerRepo;
        }
        public async Task Add (BannerAddDto bannerDto)
        {
            bool bannerExists = await _bannerRepo.ExistsByPriority(bannerDto.Priority);
            if (bannerExists) throw new MainException("Priority of banner already exists", 400);

            var bannerModel = new BannerModel
            {
                Description = bannerDto.Description,
                Priority = bannerDto.Priority,
                ImgUrl = bannerDto.ImgUrl,
                Visibility = bannerDto.visibility
            };
            await _bannerRepo.Add(bannerModel);
        }
    }
}
