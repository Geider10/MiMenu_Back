using MiMenu_Back.Data.DTOs.Banner;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;
using System.Reflection.Metadata;

namespace MiMenu_Back.Services
{
    public class BannerService
    {
        private readonly IBannerRepository _bannerRepo;
        private readonly IBannerMapper _bannerMap;
        public BannerService(IBannerRepository bannerRepo, IBannerMapper bannerMap)
        {
            _bannerRepo = bannerRepo;
            _bannerMap = bannerMap;
        }
        public async Task Add (BannerAddDto bannerDto)
        {
            bool bannerExists = await _bannerRepo.ExistsByPriority(bannerDto.Priority);
            if (bannerExists) throw new MainException("Priority of banner already exists", 400);

            var bannerModel = _bannerMap.AddToBanner(bannerDto);
            await _bannerRepo.Add(bannerModel);
        }
        public async Task<BannerGetOneDto> GetById(string id)
        {
            var bannerModel = await _bannerRepo.GetById(id);
            if (bannerModel == null) throw new MainException("Banner no found", 404);

            var bannerDto = _bannerMap.BannerToGetOne(bannerModel);
            return bannerDto;
        }
        public async Task<List<BannerGetAllDto>> GetAll(BannerQueryDto queryDto)
        {
            var bannerList = await _bannerRepo.GetAll(queryDto.SortPriority, queryDto.Visibility);
            if (bannerList == null || bannerList.Count == 0) throw new MainException("There are no banners", 404);

            var dtoList = _bannerMap.BannerListToGetList(bannerList);
            return dtoList;
        }
    }
}
