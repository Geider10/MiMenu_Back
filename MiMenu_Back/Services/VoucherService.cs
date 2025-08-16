using Microsoft.AspNetCore.Components.Web;
using MiMenu_Back.Data.DTOs.Voucher;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;
using System.Diagnostics;
namespace MiMenu_Back.Services
{
    public class VoucherService
    {
        private readonly IVoucherRepository _voucherRepo;
        private readonly IVoucherMapper _voucherMap;
        private readonly Util _util;
        public VoucherService(IVoucherRepository voucherRepo, IVoucherMapper voucherMap, Util util)
        {
            _voucherRepo = voucherRepo;
            _voucherMap = voucherMap;
            _util = util;
        }
        public async Task Add (VoucherAddDto voucherDto)
        {
            bool voucherExists = await _voucherRepo.ExistsByNameYCategory(voucherDto.Name, voucherDto.IdCategory);
            if (voucherExists) throw new MainException("Voucher already exists with this Name and Category", 400);
            if (voucherDto.Type == "Porciento" && voucherDto.Discount > 100) throw new MainException("If voucher is type Porciento, discount must be between 1 to 100");

            DateOnly dueDate = _util.StringToDateOnly(voucherDto.DueDate);
            DateOnly createDate = _util.StringToDateOnly(voucherDto.CreateDate);
            int dateValidate = _util.CompareDate(createDate, dueDate);
            if (dateValidate < 0) throw new MainException("DueDate must be equal to or later than CreateDate", 400);

            var voucherModel = _voucherMap.AddToVoucherModel(voucherDto, dueDate, createDate);
            await _voucherRepo.Add(voucherModel);
        }
        public async Task<VoucherGetByIdDto> GetById (string id)
        {
            var voucherModel = await _voucherRepo.GetById(id);
            if (voucherModel == null) throw new MainException("Voucher no found", 404);

            string dueDate = _util.DateOnlyToString(voucherModel.DueDate);
            string createDate = _util.DateOnlyToString(voucherModel.CreateDate);

            var voucherDto = _voucherMap.ModelToVoucherDto(voucherModel, dueDate, createDate);
            return voucherDto;
        }
        public async Task<List<VoucherGetAllDto>> GetAll (VoucherQueryDto voucherQuery)
        {
            var voucherList = await _voucherRepo.GetAll(voucherQuery.Category, voucherQuery.SortName, voucherQuery.Visibility);
            if (voucherList.Count == 0 || voucherList == null) throw new MainException("There are no vouchers", 404);
            if(voucherQuery.Expired.HasValue)
            {
                DateOnly dateRequest = new DateOnly();
                voucherList = voucherList.FindAll(v =>
                {
                    int dateValidate = _util.CompareDate(dateRequest, v.DueDate);
                    if(voucherQuery.Expired == false)
                    {
                        if (dateValidate >= 0) return true;
                        return false;
                    }
                    if (dateValidate < 0) return true;
                    return false;
                });
            }
            var voucherDtoList = _voucherMap.ModelListToDtoList(voucherList);
            return voucherDtoList;
        }

    }
}
