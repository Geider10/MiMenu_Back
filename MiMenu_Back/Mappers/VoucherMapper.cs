using MiMenu_Back.Data.DTOs.Voucher;
using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class VoucherMapper : IVoucherMapper
    {
        public VoucherModel AddToVoucherModel(VoucherAddDto voucherDto, TypeVoucherEnum type, DateOnly dueDate, DateOnly createDate)
        {
            return new VoucherModel
            {
                Name = voucherDto.Name,
                Type = type,
                Discount = voucherDto.Discount,
                BuyMinimum = voucherDto.BuyMinimum,
                Visibility = voucherDto.Visibility,
                DueDate = dueDate,
                CreateDate = createDate
            };
        }
        public VoucherGetByIdDto ModelToVoucherDto(VoucherModel voucher, string type, string dueDate, string createDate)
        {
            return new VoucherGetByIdDto
            {
                Id = voucher.Id.ToString(),
                Name = voucher.Name,
                Type = type,
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
        public VoucherModel UpdateToVoucherModel(VoucherUpdateDto voucherDto,VoucherModel voucher, DateOnly dueDate)
        {
            voucher.Name = voucherDto.Name;
            voucher.Discount = voucherDto.Discount;
            voucher.BuyMinimum = voucherDto.BuyMinimum;
            voucher.DueDate = dueDate;
            return voucher;
        }
    }
}
