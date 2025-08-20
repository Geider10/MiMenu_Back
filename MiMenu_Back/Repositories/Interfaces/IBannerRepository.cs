using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IBannerRepository
    {
        Task<bool> ExistsByPriority(int priority);
        Task Add(BannerModel banner);
        Task<BannerModel?> GetById(string id);
        Task<List<BannerModel>?> GetAll(string? sortPriority, bool? visibility);
    }
}
