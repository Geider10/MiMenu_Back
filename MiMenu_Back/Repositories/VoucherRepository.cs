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
        public async Task<bool> ExistsByName(string name)
        {
            return await _appDB.Vouchers.AnyAsync(v => v.Name == name);
        }
        public async Task<bool> ExistsByName(string name, string idIgnore)
        {
            return await _appDB.Vouchers.AnyAsync(v => v.Name == name && v.Id != Guid.Parse(idIgnore));
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
        public async Task<List<VoucherModel>?> GetAll(string? sortName, bool? visibility)
        {
            var voucherList = await _appDB.Vouchers
                .ToListAsync();
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
        public async Task Update(VoucherModel voucher)
        {
            _appDB.Vouchers.Update(voucher);
            await _appDB.SaveChangesAsync();
        }
        public async Task Delete(VoucherModel voucher)
        {
            _appDB.Vouchers.Remove(voucher);
            await _appDB.SaveChangesAsync();
        }
    }
}
