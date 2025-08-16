using MiMenu_Back.Data.DTOs.Voucher;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface IVoucherMapper
    {
        VoucherModel AddToVoucherModel(VoucherAddDto voucherDto, DateOnly dueDate, DateOnly createDate);
        VoucherGetByIdDto ModelToVoucherDto(VoucherModel voucher, string dueDate, string createDate);
        List<VoucherGetAllDto> ModelListToDtoList(List<VoucherModel> voucherList);
    }
}
