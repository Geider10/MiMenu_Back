using Microsoft.EntityFrameworkCore;
using MiMenu_Back.Data;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Repositories.Interfaces;

namespace MiMenu_Back.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly AppDB _appDB;
        public VoucherRepository(AppDB appDB)
        {
            _appDB = appDB;
        }
        public async Task<bool> ExistsByNameYCategory(string name, string idCategory)
        {
            return await _appDB.Vouchers.AnyAsync(v => v.Name == name && v.IdCategory == Guid.Parse(idCategory));
        }
        public async Task Add(VoucherModel voucher)
        {
            _appDB.Vouchers.Add(voucher);
            await _appDB.SaveChangesAsync();
        }
        public async Task<VoucherModel?> GetById(string id)
        {
            return await _appDB.Vouchers.FirstOrDefaultAsync(v => v.Id == Guid.Parse(id));
        }
        public async Task<List<VoucherModel>?> GetAll(string? category, string? sortName, bool? visibility)
        {
            var voucherList = await _appDB.Vouchers
                .Include(v => v.Category)
                .ToListAsync();

            if (!string.IsNullOrEmpty(category))
            {
                voucherList = voucherList.Where(v => v.Category.Name.ToLower() == category.ToLower()).ToList();
            }
            if (sortName == "asc" && !string.IsNullOrEmpty(sortName))
            {
                voucherList = voucherList.OrderBy(v => v.Name).ToList();
            }else if(sortName == "desc" && !string.IsNullOrEmpty(sortName))
            {
                voucherList = voucherList.OrderByDescending(v => v.Name).ToList();
            }
            if (visibility.HasValue)
            {
                voucherList = voucherList.Where(v => v.Visibility == visibility).ToList();
            }
            return voucherList;
        }
    }
}
