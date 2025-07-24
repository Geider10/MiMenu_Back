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
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
