using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IVoucherRepository
    {
        Task<bool> ExistsByNameYCategory(string name, string idCategory);
        Task Add(VoucherModel voucher);
    }
}
