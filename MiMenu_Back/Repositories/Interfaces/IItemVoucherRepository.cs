using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IItemVoucherRepository
    {
        Task<bool> ExistsByUserYVoucher(string idUser, string idVoucher);
        Task Add(ItemVoucherModel itemVoucher);
    }
}
