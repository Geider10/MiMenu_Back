using MiMenu_Back.Data.DTOs.Voucher;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IItemVoucherRepository
    {
        Task<bool> ExistsByUserYVoucher(string idUser, string idVoucher);
        Task Add(ItemVoucherModel itemVoucher);
        Task<List<ItemVoucherModel>?> GetAllByUserId(string idUser);
    }
}
