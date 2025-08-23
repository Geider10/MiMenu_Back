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
                //validar datos de entrada
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
    }
}
