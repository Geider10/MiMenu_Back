using Microsoft.EntityFrameworkCore;
using MiMenu_Back.Data;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Repositories.Interfaces;

namespace MiMenu_Back.Repositories
{
    public class ItemVoucherRepository : IItemVoucherRepository
    {
        private AppDB _appDB;
        public ItemVoucherRepository(AppDB appDB)
        {
            _appDB = appDB;
        }
        public async Task<bool> ExistsByUserYVoucher(string idUser, string idVoucher)
        {
            return await _appDB.ItemsVoucher.AnyAsync(iv => iv.IdUser == Guid.Parse(idUser) && iv.IdVoucher == Guid.Parse(idVoucher));
        }
        public async Task Add(ItemVoucherModel itemVoucher)
        {
            _appDB.ItemsVoucher.Add(itemVoucher);
            await _appDB.SaveChangesAsync();
        }
    }
}
