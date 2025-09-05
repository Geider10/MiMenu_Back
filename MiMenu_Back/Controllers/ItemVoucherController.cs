using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Data.DTOs.ItemVoucher;
using MiMenu_Back.Data.DTOs.Voucher;
using MiMenu_Back.Services;
using MiMenu_Back.Utils;
using MiMenu_Back.Validators.Voucher;

namespace MiMenu_Back.Controllers
{
    [Route("api/itemVoucher")]
    [ApiController]
    [Authorize(Roles="client")]
    public class ItemVoucherController : ControllerBase
    {
        private readonly ItemVoucherService _ivService;
        public ItemVoucherController(ItemVoucherService ivService)
        {
            _ivService = ivService;
        }
        [HttpPost]
        public async Task<ActionResult<MainResponse>> Add([FromBody]ItemVoucherAddDto ivDto)
        {
            try
            {
                if (!Guid.TryParse(ivDto.IdUser, out _)) return BadRequest("IdUser must has format Guid");
                if (!Guid.TryParse(ivDto.IdVoucher, out _)) return BadRequest("IdVoucher must has format Guid");

                await _ivService.Add(ivDto);
                return StatusCode(201, new MainResponse(true, "ItemVoucher added with success"));
            }
            catch (MainException ex)
            {
                return StatusCode(ex.StatusCode, new MainResponse(false, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MainResponse(false, "Internal server error: " + ex.Message));
            }
        }
        [HttpGet][Route("user/{idUser}")]
        public async Task<ActionResult<List<VoucherGetAllDto>>> GetAllByUser([FromRoute]string idUser)
        {
            try
            {
                if (!Guid.TryParse(idUser, out _)) return BadRequest("IdUser must has format Guid");

                List<VoucherGetAllDto> ivDtoList = await _ivService.GetAllByUser(idUser);
                return StatusCode(200, ivDtoList);
            }
            catch (MainException ex)
            {
                return StatusCode(ex.StatusCode, new MainResponse(false, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MainResponse(false, "Internal server error: " + ex.Message));
            }
        }
        [HttpPost][Route("apply-voucher")]
        public async Task<ActionResult<VoucherDiscountDto>> ApplyVoucher([FromBody]VoucherApplyDto voucherDto)
        {
            try
            {
                ValidationResult bodyReq = new VoucherApplyValidator().Validate(voucherDto);
                if (!bodyReq.IsValid) return BadRequest(bodyReq.Errors);

                VoucherDiscountDto voucherDiscount = await _ivService.ApplyVoucher(voucherDto);
                return StatusCode(200, voucherDiscount);
            }
            catch (MainException ex)
            {
                return StatusCode(ex.StatusCode, new MainResponse(false, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MainResponse(false, "Internal server error: " + ex.Message));
            }
        }
    }
}
