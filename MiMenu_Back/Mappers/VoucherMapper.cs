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
        public VoucherGetByIdDto ModelToVoucherDto(VoucherModel voucher, string dueDate, string createDate)
        {
            return new VoucherGetByIdDto
            {
                Name = voucher.Name,
                Type = voucher.Type,
                Discount = voucher.Discount,
                BuyMinimum = voucher.BuyMinimum,
                Visibility = voucher.Visibility,
                DueDate = dueDate,
                CreateDate = createDate
            };
        }
        public List<VoucherGetAllDto> ModelListToDtoList(List<VoucherModel> voucherList)
        {
            var voucherDtoList = new List<VoucherGetAllDto>();
            foreach (var item in voucherList)
            {
                voucherDtoList.Add(new VoucherGetAllDto
                {
                    Id = item.Id.ToString(),
                    Name = item.Name,
                    BuyMinimum = item.BuyMinimum,
                    DueDate = item.DueDate.ToString("dd-MM-yyyy")
                });
            }
            return voucherDtoList;
        }
        public VoucherModel UpdateToVoucherModel(VoucherAddDto voucherDto,VoucherModel voucher, DateOnly dueDate)
        {
            voucher.IdCategory = Guid.Parse(voucherDto.IdCategory);
            voucher.Name = voucherDto.Name;
            voucher.Type = voucherDto.Type;
            voucher.Discount = voucherDto.Discount;
            voucher.BuyMinimum = voucherDto.BuyMinimum;
            voucher.DueDate = dueDate;

            return voucher;
        }
    }
}
