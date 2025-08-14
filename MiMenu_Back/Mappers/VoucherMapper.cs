using MiMenu_Back.Data.DTOs.Voucher;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class VoucherMapper : IVoucherMapper
    {
        public VoucherModel AddToVoucherModel(VoucherAddDto voucherDto, DateOnly dueDate, DateOnly createDate)
        {
            return new VoucherModel
            {
                IdCategory = Guid.Parse(voucherDto.IdCategory),
                Name = voucherDto.Name,
                Type = voucherDto.Type,
                Discount = voucherDto.Discount,
                BuyMinimum = voucherDto.BuyMinimum,
                Visibility = voucherDto.Visibility,
                DueDate = dueDate,
                CreateDate = createDate
            };
        }
    }
}
