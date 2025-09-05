using MiMenu_Back.Data.DTOs;
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
            if (bannerExists) throw new MainException("Priority of banner already exists", 409);

            BannerModel bannerModel = _bannerMap.AddToBanner(bannerDto);
            await _bannerRepo.Add(bannerModel);
        }
        public async Task<BannerGetOneDto> GetById(string id)
        {
            BannerModel? bannerModel = await _bannerRepo.GetById(id);
            if (bannerModel == null) throw new MainException("Banner no found", 404);

            BannerGetOneDto bannerDto = _bannerMap.BannerToGetOne(bannerModel);
            return bannerDto;
        }
        public async Task<List<BannerGetAllDto>> GetAll(BannerQueryDto queryDto)
        {
            List<BannerModel>? bannerList = await _bannerRepo.GetAll(queryDto.SortPriority, queryDto.Visibility);
            if (bannerList == null || bannerList.Count == 0) throw new MainException("There are no banners", 404);

            List<BannerGetAllDto> dtoList = _bannerMap.BannerListToGetList(bannerList);
            return dtoList;
        }
        public async Task UpdateVisibility(string id,VisibilityUpdateDto visibleDto)
        {
            BannerModel? bannerModel = await _bannerRepo.GetById(id);
            if (bannerModel == null) throw new MainException("Banner no found", 404);

            bannerModel.Visibility = visibleDto.Visibility;
            await _bannerRepo.Update(bannerModel);
        }
        public async Task UpdateImg(string id, ImgUpdateDto imgDto)
        {
            BannerModel? bannerModel = await _bannerRepo.GetById(id);
            if (bannerModel == null) throw new MainException("Banner no found", 404);

            bannerModel.ImgUrl = imgDto.ImgUrl;
            await _bannerRepo.Update(bannerModel);
        }
        public async Task Update(string id, BannerUpdateDto bannerDto)
        {
            BannerModel? bannerModelA = await _bannerRepo.GetById(id);
            if (bannerModelA == null) throw new MainException("Banner no found", 404);

            BannerModel? bannerModelB = await _bannerRepo.GetByPriority(bannerDto.Priority, id);
            if (bannerModelB != null)
            {
                bannerModelB.Priority = bannerModelA.Priority;
                await _bannerRepo.Update(bannerModelB);
            }
            bannerModelA.Priority = bannerDto.Priority;
            bannerModelA.Description = bannerDto.Description;

            await _bannerRepo.Update(bannerModelA);
        }
        public async Task DeleteImg(string id)
        {
            BannerModel? bannerModel = await _bannerRepo.GetById(id);
            if (bannerModel == null) throw new MainException("Banner no found", 404);

            bannerModel.ImgUrl = null;
            await _bannerRepo.Update(bannerModel);
        }
        public async Task Delete(string id)
        {
            BannerModel? bannerModel = await _bannerRepo.GetById(id);
            if (bannerModel == null) throw new MainException("Banner no found", 404);

            await _bannerRepo.Delete(bannerModel);
        }
    }
}
