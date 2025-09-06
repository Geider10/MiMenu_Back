using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IItemVoucherRepository
    {
        Task<bool> ExistsByUserYVoucher(string idUser, string idVoucher);
        Task<bool> ExistsByVoucherId(string idVoucher);
        Task Add(ItemVoucherModel itemVoucher);
        Task<List<ItemVoucherModel>?> GetAllByUserId(string idUser);
        Task<ItemVoucherModel?> GetById(string idIV); 
    }
}
