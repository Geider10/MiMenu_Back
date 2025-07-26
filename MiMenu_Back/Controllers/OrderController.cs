using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Services;
using MiMenu_Back.Utils;
using MiMenu_Back.Validators.Order;

namespace MiMenu_Back.Controllers
{
    [Route("api/order")]
    [ApiController]
    [Authorize(Roles="client")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderSer;
        public OrderController(OrderService orderSer)
        {
            _orderSer = orderSer;
        }
        [HttpPost]
        public async Task<ActionResult<MainResponse>> Add([FromBody]OrderAddDto order)
        {
            try
            {
                ValidationResult bodyReq = new OrderAddValidator().Validate(order);
                if (!bodyReq.IsValid) return BadRequest(bodyReq.Errors);

                await _orderSer.Add(order);
                return StatusCode(201, new MainResponse(true, "Order created with success"));
            }
            catch(MainException ex)
            {
                return StatusCode(ex.StatusCode, new MainResponse(false, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MainResponse(false, "Internal server error: " + ex.Message));
            }
        }
        [HttpGet][Route("{idOrder}/user/{idUser}")]
        public async Task<ActionResult<OrderGetDto>> GetById([FromRoute]string idOrder,[FromRoute]string idUser)
        {
            try
            {
                if (!Guid.TryParse(idOrder, out _)) return BadRequest("IdOrder must has format Guid");
                if (!Guid.TryParse(idUser, out _)) return BadRequest("IdUser must has format Guid");

                var orderDto = await _orderSer.GetById(idOrder, idUser);
                return StatusCode(200, orderDto);
            }
            catch(MainException ex)
            {
                return StatusCode(ex.StatusCode, new MainResponse(false, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MainResponse(false, "Internal server error: " + ex.Message));
            }
        }
        [HttpGet][Route("user/{idUser}")]
        public async Task<ActionResult<List<OrderGetDto>>> GetAllByUserId([FromRoute]string idUser)
        {
            try
            {
                if (!Guid.TryParse(idUser, out _)) return BadRequest("IdUser must has format Guid");

                var dtoList = await _orderSer.GetAllByUserId(idUser);
                return StatusCode(200, dtoList);
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
        [HttpPut][Route("{idOrder}/user/{idUser}")]
        public async Task<ActionResult<MainResponse>> Update([FromRoute]string idOrder,[FromRoute]string idUser, [FromBody]OrderUpdateDto orderDto)
        {
            try
            {
                if (!Guid.TryParse(idOrder, out _)) return BadRequest("IdOrder must has format Guid");
                if (!Guid.TryParse(idUser, out _)) return BadRequest("IdUser must has format Guid");
                ValidationResult bodyReq = new OrderUpdateValidator().Validate(orderDto);
                if (!bodyReq.IsValid) return BadRequest(bodyReq.Errors);

                await _orderSer.Update(idOrder, idUser, orderDto);
                return StatusCode(200, new MainResponse(true, "Order updated with success"));
            }
            catch(MainException ex)
            {
                return StatusCode(ex.StatusCode, new MainResponse(false, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MainResponse(false, "Internal server error: " + ex.Message));
            }
        }
        [HttpDelete][Route("{idOrder}/user/{idUser}")]
        public async Task<ActionResult<MainResponse>> Delete([FromRoute]string idOrder, [FromRoute]string idUser)
        {
            try
            {
                if (!Guid.TryParse(idOrder, out _)) return BadRequest("IdOrder must has format Guid");
                if (!Guid.TryParse(idUser, out _)) return BadRequest("IdUser must has format Guid");

                await _orderSer.Delete(idOrder, idUser);
                return StatusCode(200, new MainResponse(true, "Order deleted with success"));
            }
            catch(MainException ex)
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
