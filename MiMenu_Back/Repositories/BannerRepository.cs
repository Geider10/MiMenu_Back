using Microsoft.EntityFrameworkCore;
using MiMenu_Back.Data;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Repositories.Interfaces;

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
        public async Task<BannerModel?> GetById(string id)
        {
            return await _appDB.Banners.FirstOrDefaultAsync(b => b.Id == Guid.Parse(id));
        }
        public async Task<List<BannerModel>?> GetAll(string? sortPriority, bool? visibility)
        {
            var bannerList = await _appDB.Banners
                .ToListAsync();

            if(sortPriority == "asc" && !string.IsNullOrEmpty(sortPriority))
            {
                bannerList = bannerList.OrderBy(b => b.Priority).ToList();
            }else if(sortPriority == "desc" && !string.IsNullOrEmpty(sortPriority))
            {
                bannerList = bannerList.OrderByDescending(b => b.Priority).ToList();
            }
            if (visibility.HasValue)
            {
                bannerList = bannerList.Where(b => b.Visibility == visibility).ToList();
            }
            return bannerList;
        }
    }
}
