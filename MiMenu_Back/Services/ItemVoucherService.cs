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
                int dateValidate = _util.CompareDates(dateCurrent, iv.Voucher.DueDate);
                if (dateValidate >= 0) return true;
                return false;
            });
            if (ivList == null || ivList.Count == 0) throw new MainException("There are no voucher from user", 400);

            List<VoucherGetAllDto> ivDtoList = new List<VoucherGetAllDto>();
            foreach(var iv in ivList)
            {
                ivDtoList.Add(new VoucherGetAllDto
                {
                    Id = iv.Id.ToString(),//itemVoucher to order
                    Name = iv.Voucher.Name,
                    BuyMinimum = iv.Voucher.BuyMinimum,
                    DueDate = _util.FormatDateOnly(iv.Voucher.DueDate)
                });
            }
            return ivDtoList;
        }
        public async Task<VoucherDiscountDto> ApplyVoucher(VoucherApplyDto voucherDto)
        {
            var ivModel = await _ivRepo.GetById(voucherDto.idItemVoucher);
            if (ivModel == null) throw new MainException("ItemVoucher no found", 404);
            if (ivModel.IdUser != Guid.Parse(voucherDto.idUser)) throw new MainException("ItemVoucher must be from User", 400);

            if (voucherDto.TotalOrder < ivModel.Voucher.BuyMinimum) throw new MainException("TotalOrder must be equal or greater than BuyMinimum from Voucher", 400);
            DateOnly dateCurrent = _util.CreateDateCurrent();
            int dateValidate = _util.CompareDates(dateCurrent, ivModel.Voucher.DueDate);
            if (dateValidate < 0) throw new MainException("Voucher is expired");

            var discount = ivModel.Voucher.Discount;
            if (ivModel.Voucher.Type == Data.Enums.TypeVoucherEnum.Percentage)
            {
                int calculateDiscount = (voucherDto.TotalOrder * discount) / 100;
                discount = calculateDiscount;
            }
            return new VoucherDiscountDto
            {
                Discount = discount,
                TypeDiscount = _util.FormatTypeVoucher(ivModel.Voucher.Type),
                IdVoucher = ivModel.Voucher.Id.ToString()
                //totalUpdated = totalOrder - discount
            };
        }
    }
}
