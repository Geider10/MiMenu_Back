using MiMenu_Back.Data.DTOs.Banner;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class BannerMapper : IBannerMapper
    {
        public BannerModel AddToBanner(BannerAddDto bannerDto)
        {
            return new BannerModel
            {
                Description = bannerDto.Description,
                Priority = bannerDto.Priority,
                ImgUrl = bannerDto.ImgUrl,
                Visibility = bannerDto.visibility
            };
        }
        public BannerGetOneDto BannerToGetOne(BannerModel bannerModel)
        {
            return new BannerGetOneDto
            {
                Id = bannerModel.Id.ToString(),
                Description = bannerModel.Description,
                Priority = bannerModel.Priority,
                ImgUrl = bannerModel.ImgUrl,
                Visibility = bannerModel.Visibility
            };
        }
        public List<BannerGetAllDto> BannerListToGetList(List<BannerModel> bannerList)
        {
            var dtoList = new List<BannerGetAllDto>();
            foreach (var banner in bannerList)
            {
                dtoList.Add(new BannerGetAllDto
                {
                    Id = banner.Id.ToString(),
                    ImgUrl = banner.ImgUrl
                });
            }
            return dtoList;
        }
    }
}
