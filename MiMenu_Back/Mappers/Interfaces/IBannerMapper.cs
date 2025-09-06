using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface IBannerMapper
    {
        BannerModel AddToBanner(BannerAddDto bannerDto);
        BannerGetOneDto BannerToGetOne(BannerModel bannerModel);
        List<BannerGetAllDto> BannerListToGetList(List<BannerModel> bannerList);
    }
}
