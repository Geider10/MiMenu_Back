using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Data.DTOs.Payment;
using MiMenu_Back.Services;
using MiMenu_Back.Utils;

namespace MiMenu_Back.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [Authorize(Roles ="client")]
        [HttpPost][Route("checkout/preferences")]
        public async Task<ActionResult<ResponsePreferenceDto>> CreatePreference([FromBody]CreatePreferenceDto preferenceDto)
        {
            try
            {
                var resPreference = await _paymentService.CreatePreference(preferenceDto);
                return StatusCode(200, resPreference);
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
        [HttpPost][Route("webhook")]//receive msj from server MP
        public async Task<ActionResult> ReceiveNotification([FromBody] MPMessageDto messageDto)
        {
            try
            {
                await _paymentService.ReceiveWebhook(messageDto);
                return StatusCode(200);
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
        [Authorize(Roles ="client")]
        [HttpGet][Route("{id}")]
        public async Task<ActionResult<PaymentGetDto>> GetById([FromRoute]string id)
        {
            try
            {
                if (!Guid.TryParse(id, out _)) return BadRequest("Id must has format Guid");

                var paymentDto = await _paymentService.GetById(id);
                return StatusCode(200, paymentDto);
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
