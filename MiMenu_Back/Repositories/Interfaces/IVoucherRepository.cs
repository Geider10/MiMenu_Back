using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IVoucherRepository
    {
        Task<bool> ExistsByNameYCategory(string name, string idCategory);
        Task<bool> ExistsByNameYCategory(string name, string idCategory, string idIgnore);
        Task Add(VoucherModel voucher);
        Task<VoucherModel?> GetById(string id);
        Task<List<VoucherModel>?> GetAll(string? category, string? sortName, bool? visibility);
        Task Update(VoucherModel voucher);
        Task Delete(VoucherModel voucher);
    }
}
