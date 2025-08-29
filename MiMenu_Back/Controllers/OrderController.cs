using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Data.DTOs.Order;
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
        public async Task<ActionResult> Add ([FromRoute]string idUser,[FromRoute] string idPayment, [FromBody]OrderAddDto orderDto)
        {
            try
            {
                string idPublic = await _orderService.Add(idUser, idPayment, orderDto);
                return StatusCode(201, new { id = idPublic });
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
