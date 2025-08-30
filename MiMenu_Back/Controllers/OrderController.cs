using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.DTOs.Payment;
using MiMenu_Back.Services;
using MiMenu_Back.Utils;

namespace MiMenu_Back.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        [Authorize(Roles ="client")]
        [HttpPost][Route("user/{idUser}/payment/{idPayment}")]
        public async Task<ActionResult> Add ([FromRoute]string idUser,[FromRoute] string idPayment, [FromBody]CreatePreferenceDto orderDto)
        {
            try
            {
                await _orderService.AddOrder(idUser, idPayment, orderDto.Order, orderDto.ItemsCart);
                return StatusCode(201, "Order and OrderItems added with success");
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
        //protected by local jwt
        [HttpPut][Route("{id}/status")]
        public async Task<ActionResult<MainResponse>> UpdateStatus([FromRoute]string id)
        {
            try
            {
                if (!Guid.TryParse(id, out _)) return BadRequest("Id must has format Guid");

                await _orderService.UpdateStatus(id);
                return StatusCode(200, new MainResponse(true, "Status from order updated with success"));
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
