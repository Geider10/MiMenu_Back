using Microsoft.AspNetCore.Components.Web;
using MiMenu_Back.Data.DTOs;
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
        private readonly IItemVoucherRepository _ivRepo;
        private readonly Util _util;
        public VoucherService(IVoucherRepository voucherRepo, IVoucherMapper voucherMap, Util util, IItemVoucherRepository ivRepo)
        {
            _voucherRepo = voucherRepo;
            _voucherMap = voucherMap;
            _util = util;
            _ivRepo = ivRepo;
        }
        public async Task Add (VoucherAddDto voucherDto)
        {
            bool voucherExists = await _voucherRepo.ExistsByName(voucherDto.Name);
            if (voucherExists) throw new MainException("Voucher already exists with this Name", 400);
            if (voucherDto.Type == "Porciento" && voucherDto.Discount > 100) throw new MainException("If voucher is type Porciento, discount must be between 1 to 100");

            DateOnly dateCurrent = _util.CreateDateCurrent();
            DateOnly dueDate = _util.FormatDateOnly(voucherDto.DueDate);
            int dateValidate = _util.CompareDates(dateCurrent, dueDate);
            if (dateValidate < 0) throw new MainException("DueDate must be equal to or later than DateCurrent", 400);

            var voucherModel = _voucherMap.AddToVoucherModel(voucherDto, dueDate, dateCurrent);
            await _voucherRepo.Add(voucherModel);
        }
        public async Task<VoucherGetByIdDto> GetById (string id)
        {
            var voucherModel = await _voucherRepo.GetById(id);
            if (voucherModel == null) throw new MainException("Voucher no found", 404);

            string dueDate = _util.FormatDateOnly(voucherModel.DueDate);
            string createDate = _util.FormatDateOnly(voucherModel.CreateDate);

            var voucherDto = _voucherMap.ModelToVoucherDto(voucherModel, dueDate, createDate);
            return voucherDto;
        }
        public async Task<List<VoucherGetAllDto>> GetAll (VoucherQueryDto voucherQuery)
        {
            var voucherList = await _voucherRepo.GetAll(voucherQuery.SortName, voucherQuery.Visibility);
            if (voucherList.Count == 0 || voucherList == null) throw new MainException("There are no vouchers", 404);
            if(voucherQuery.Expired.HasValue)
            {
                DateOnly dateCurrent = _util.CreateDateCurrent();
                voucherList = voucherList.FindAll(v =>
                {
                    int dateValidate = _util.CompareDates(dateCurrent, v.DueDate);//DueDate voucher >= DateCurrent user
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
        public async Task Update (string id, VoucherAddDto voucherDto)
        {
            var voucherModel = await _voucherRepo.GetById(id);
            if (voucherModel == null) throw new MainException("Voucher no found", 404);
            bool voucherExists = await _voucherRepo.ExistsByName(voucherDto.Name, id);
            if(voucherExists) throw new MainException("Voucher already exists with this Name", 400);
            if(voucherDto.Type == "Porciento" && voucherDto.Discount > 100) throw new MainException("If voucher is type Porciento, discount must be between 1 to 100");

            DateOnly dueDate= _util.FormatDateOnly(voucherDto.DueDate);
            int dateResult = _util.CompareDates(voucherModel.DueDate, dueDate);
            if (dateResult != 0)
            {
                DateOnly dateCurrent = _util.CreateDateCurrent();
                int dateResult2 = _util.CompareDates(dateCurrent, dueDate);
                if (dateResult2 < 0) throw new MainException("DueDate must be equal to or later than DateCurrent", 400);
            }
            var voucherModelUpdated = _voucherMap.UpdateToVoucherModel(voucherDto, voucherModel, dueDate);
            await _voucherRepo.Update(voucherModelUpdated);
        }
        public async Task UpdateVisibility(string id, VisibilityUpdateDto visibleDto)
        {
            var voucherModel = await _voucherRepo.GetById(id);
            if (voucherModel == null) throw new MainException("Voucher no found", 404);

            voucherModel.Visibility = visibleDto.Visibility;
            await _voucherRepo.Update(voucherModel);
        }
        public async Task Delete (string id)
        {
            var voucherModel = await _voucherRepo.GetById(id);
            if (voucherModel == null) throw new MainException("Voucher no found", 404);
            bool ivExists = await _ivRepo.ExistsByVoucherId(id);
            if (ivExists) throw new MainException("Cannot be deleted because is associated with a user", 400);

            await _voucherRepo.Delete(voucherModel);
        }
    }
}
