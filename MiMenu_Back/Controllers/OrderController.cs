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
        [HttpGet][Route("user/{idUser}")]
        public async Task<ActionResult<List<OrderGetAllDto>>> GetAllByUserId([FromRoute]string idUser, [FromQuery]OrderQueryDto queryDto)
        {
            try
            {
                if (!Guid.TryParse(idUser, out _)) return BadRequest("Id must has format Guid");

                var generalList = await _orderService.GetAllByUserId(idUser, queryDto);
                return StatusCode(201, generalList);
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
        [HttpGet][Route("{idOrder}/user/{idUser}")]
        public async Task<ActionResult<OrderGeneralDto>> GetById([FromRoute]string idOrder, [FromRoute]string idUser)
        {
            try
            {
                if(!Guid.TryParse(idOrder, out _)) return BadRequest("IdOrder must has format Guid");
                if (!Guid.TryParse(idUser, out _)) return BadRequest("IdUser must has format Guid");

                var orderDto = await _orderService.GetById(idOrder, idUser);
                return StatusCode(200, orderDto);
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
