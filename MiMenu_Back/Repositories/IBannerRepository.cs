using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories
{
    public interface IBannerRepository
    {
        Task<bool> ExistsByPriority(int priority);
        Task Add(BannerModel banner);
    }
}
