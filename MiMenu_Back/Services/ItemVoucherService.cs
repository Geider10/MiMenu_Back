using MiMenu_Back.Data.DTOs.ItemVoucher;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;

namespace MiMenu_Back.Services
{
    public class ItemVoucherService
    {
        private readonly IItemVoucherRepository _ivRepo;
        public ItemVoucherService(IItemVoucherRepository ivRepo)
        {
            _ivRepo = ivRepo;
        }
        public async Task Add(ItemVoucherAddDto ivDto)
        {
            bool ivExists = await _ivRepo.ExistsByUserYVoucher(ivDto.IdUser, ivDto.IdVoucher);
            if (ivExists) throw new MainException("ItemVoucher already exists with this UserId and VoucherId", 400);

            var ivModel = new ItemVoucherModel
            {
                IdUser = Guid.Parse(ivDto.IdUser),
                IdVoucher = Guid.Parse(ivDto.IdVoucher)
            };
            await _ivRepo.Add(ivModel);
        }
    }
}
