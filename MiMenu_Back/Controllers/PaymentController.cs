using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Services;
using MiMenu_Back.Utils;
using MiMenu_Back.Validators;

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
                ValidationResult result = new CreatePreferenceValidator().Validate(preferenceDto);
                if (!result.IsValid) return BadRequest(result.Errors);

                ResponsePreferenceDto resPreference = await _paymentService.CreatePreference(preferenceDto);
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
        //api very private
        [HttpPost][Route("webhook")]
        public async Task<ActionResult> ReceiveWebhook([FromBody] WebHookDto webhookDto)
        {
            try
            {
                ValidationResult result = new WebhookValidator().Validate(webhookDto);
                if (!result.IsValid) return BadRequest(result.Errors);

                await _paymentService.ReceiveWebhook(webhookDto);
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

                PaymentGetDto paymentDto = await _paymentService.GetById(id);
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
