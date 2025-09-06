using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface IVoucherMapper
    {
        VoucherModel AddToVoucherModel(VoucherAddDto voucherDto, TypeVoucherEnum type, DateOnly dueDate, DateOnly createDate);
        VoucherGetByIdDto ModelToVoucherDto(VoucherModel voucher, string type, string dueDate, string createDate);
        List<VoucherGetAllDto> ModelListToDtoList(List<VoucherModel> voucherList);
        VoucherModel UpdateToVoucherModel(VoucherUpdateDto voucherDto, VoucherModel voucher, DateOnly dueDate);
    }
}
