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
        
    }
}
