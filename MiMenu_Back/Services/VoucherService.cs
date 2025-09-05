using Microsoft.AspNetCore.Components.Web;
using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Data.DTOs.Voucher;
using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;
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
            if (voucherExists) throw new MainException("Voucher already exists with this Name", 409);
            TypeVoucherEnum typeVoucher = _util.FormatTypeVoucher(voucherDto.Type);
            if (typeVoucher == TypeVoucherEnum.Percentage && voucherDto.Discount > 100) throw new MainException("If voucher is typePercentage, discount must be between 1 to 100",422);

            DateOnly dateCurrent = _util.CreateDateCurrent();
            DateOnly dueDate = _util.FormatDateOnly(voucherDto.DueDate);
            int dateValidate = _util.CompareDates(dateCurrent, dueDate);
            if (dateValidate < 0) throw new MainException("DueDate must be equal to or later than DateCurrent", 422);

            VoucherModel voucherModel = _voucherMap.AddToVoucherModel(voucherDto, typeVoucher, dueDate, dateCurrent);
            await _voucherRepo.Add(voucherModel);
        }
        public async Task<VoucherGetByIdDto> GetById (string id)
        {
            VoucherModel? voucherModel = await _voucherRepo.GetById(id);
            if (voucherModel == null) throw new MainException("Voucher no found", 404);

            string typeVoucher = _util.FormatTypeVoucher(voucherModel.Type);
            string dueDate = _util.FormatDateOnly(voucherModel.DueDate);
            string createDate = _util.FormatDateOnly(voucherModel.CreateDate);

            VoucherGetByIdDto voucherDto = _voucherMap.ModelToVoucherDto(voucherModel, typeVoucher,dueDate, createDate);
            return voucherDto;
        }
        public async Task<List<VoucherGetAllDto>> GetAll (VoucherQueryDto voucherQuery)
        {
            List<VoucherModel>? voucherList = await _voucherRepo.GetAll(voucherQuery.SortName, voucherQuery.Visibility);
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
            List<VoucherGetAllDto> voucherDtoList = _voucherMap.ModelListToDtoList(voucherList);
            return voucherDtoList;
        }
        public async Task Update (string id, VoucherUpdateDto voucherDto)
        {
            VoucherModel? voucherModel = await _voucherRepo.GetById(id);
            if (voucherModel == null) throw new MainException("Voucher no found", 404);
            bool voucherExists = await _voucherRepo.ExistsByName(voucherDto.Name, id);
            if(voucherExists) throw new MainException("Voucher already exists with this Name", 409);

            DateOnly dueDate = _util.FormatDateOnly(voucherDto.DueDate);
            int dateResult = _util.CompareDates(voucherModel.DueDate, dueDate);
            if (dateResult != 0)
            {
                DateOnly dateCurrent = _util.CreateDateCurrent();
                int dateResult2 = _util.CompareDates(dateCurrent, dueDate);
                if (dateResult2 < 0) throw new MainException("DueDate must be equal to or later than DateCurrent", 422);
            }
            VoucherModel voucherModelUpdated = _voucherMap.UpdateToVoucherModel(voucherDto, voucherModel, dueDate);
            await _voucherRepo.Update(voucherModelUpdated);
        }
        public async Task UpdateVisibility(string id, VisibilityUpdateDto visibleDto)
        {
            VoucherModel? voucherModel = await _voucherRepo.GetById(id);
            if (voucherModel == null) throw new MainException("Voucher no found", 404);

            voucherModel.Visibility = visibleDto.Visibility;
            await _voucherRepo.Update(voucherModel);
        }
        public async Task Delete (string id)
        {
            VoucherModel? voucherModel = await _voucherRepo.GetById(id);
            if (voucherModel == null) throw new MainException("Voucher no found", 404);
            bool ivExists = await _ivRepo.ExistsByVoucherId(id);
            if (ivExists) throw new MainException("Cannot be deleted because is associated with a user", 422);

            await _voucherRepo.Delete(voucherModel);
        }
    }
}
