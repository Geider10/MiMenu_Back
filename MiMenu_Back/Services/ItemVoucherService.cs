using MiMenu_Back.Data.DTOs.ItemVoucher;
using MiMenu_Back.Data.DTOs.Voucher;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;

namespace MiMenu_Back.Services
{
    public class ItemVoucherService
    {
        private readonly IItemVoucherRepository _ivRepo;
        private readonly Util _util;
        public ItemVoucherService(IItemVoucherRepository ivRepo, Util util)
        {
            _ivRepo = ivRepo;
            _util = util;
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
        public async Task<List<VoucherGetAllDto>> GetAllByUser(string idUser)
        {
            var ivList = await _ivRepo.GetAllByUserId(idUser);
            if (ivList == null || ivList.Count == 0) throw new MainException("There are no voucher from user", 400);

            DateOnly dateCurrent = _util.CreateDateCurrent();
            ivList = ivList.FindAll(iv =>
            {
                int dateValidate = _util.CompareDate(dateCurrent, iv.Voucher.DueDate);
                if (dateValidate >= 0) return true;
                return false;
            });
            if (ivList == null || ivList.Count == 0) throw new MainException("There are no voucher from user", 400);

            List<VoucherGetAllDto> ivDtoList = new List<VoucherGetAllDto>();
            foreach(var iv in ivList)
            {
                ivDtoList.Add(new VoucherGetAllDto
                {
                    Id = iv.Id.ToString(),//itemVoucher to food model
                    Name = iv.Voucher.Name,
                    BuyMinimum = iv.Voucher.BuyMinimum,
                    DueDate = iv.Voucher.DueDate.ToString("dd-MM-yyyy")
                });
            }
            return ivDtoList;
        }
    }
}
