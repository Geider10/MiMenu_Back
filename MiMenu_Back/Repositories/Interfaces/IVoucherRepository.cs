using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IVoucherRepository
    {
        Task<bool> ExistsByName(string name);
        Task<bool> ExistsByName(string name, string idIgnore);
        Task Add(VoucherModel voucher);
        Task<VoucherModel?> GetById(string id);
        Task<List<VoucherModel>?> GetAll(string? sortName, bool? visibility);
        Task Update(VoucherModel voucher);
        Task Delete(VoucherModel voucher);
    }
}
