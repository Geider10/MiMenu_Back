using Microsoft.EntityFrameworkCore;
using MiMenu_Back.Data;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories
{
    public class BannerRepository : IBannerRepository
    {
        private readonly AppDB _appDB;
        public BannerRepository(AppDB appDB)
        {
            _appDB = appDB;
        }
        public async Task<bool> ExistsByPriority(int priority)
        {
            return await _appDB.Banners.AnyAsync(b => b.Priority == priority);
        }
        public async Task Add (BannerModel banner)
        {
            _appDB.Banners.Add(banner);
            await _appDB.SaveChangesAsync();
        }
    }
}
