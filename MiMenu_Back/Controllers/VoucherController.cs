using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Data.DTOs.Voucher;
using MiMenu_Back.Services;
using MiMenu_Back.Utils;
using MiMenu_Back.Validators.Voucher;

namespace MiMenu_Back.Controllers
{
    [Route("api/voucher")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly VoucherService _voucherService;
        public VoucherController(VoucherService voucherService)
        {
            _voucherService = voucherService;
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<MainResponse>> Add([FromBody]VoucherAddDto voucherDto)
        {
            try
            {
                ValidationResult bodyReq = new VoucherAddValidator().Validate(voucherDto);
                if (!bodyReq.IsValid) return BadRequest(bodyReq.Errors);

                await _voucherService.Add(voucherDto);
                return StatusCode(201, new MainResponse(true, "Voucher created with success"));
            }
            catch (MainException ex)
            {
                return StatusCode(ex.StatusCode, new MainResponse(false, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MainResponse(false, "Internal server error " + ex.Message));
            }
        }
    }
}
