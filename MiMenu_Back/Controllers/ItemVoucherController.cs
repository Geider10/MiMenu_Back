using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Data.DTOs.ItemVoucher;
using MiMenu_Back.Services;
using MiMenu_Back.Utils;

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
        public async Task<ActionResult> Add([FromBody]ItemVoucherAddDto ivDto)
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
    }
}
