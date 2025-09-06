using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Services;
using MiMenu_Back.Utils;
using MiMenu_Back.Validators;

namespace MiMenu_Back.Controllers
{
    [Route("api/cartItem")]
    [ApiController]
    [Authorize(Roles="client")]
    public class CartItemController : ControllerBase
    {
        private readonly CartItemService _ciService;
        public CartItemController(CartItemService ciService)
        {
            _ciService = ciService;
        }
        [HttpPost]
        public async Task<ActionResult<MainResponse>> Add([FromBody]CartItemAddDto cartItem)
        {
            try
            {
                ValidationResult bodyReq = new CartItemAddValidator().Validate(cartItem);
                if (!bodyReq.IsValid) return BadRequest(bodyReq.Errors);

                await _ciService.Add(cartItem);
                return StatusCode(201, new MainResponse(true, "CartItem created with success"));
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
        [HttpGet][Route("{idCartItem}/user/{idUser}")]
        public async Task<ActionResult<CartItemGetDto>> GetById([FromRoute]string idCartItem,[FromRoute]string idUser)
        {
            try
            {
                if (!Guid.TryParse(idCartItem, out _)) return BadRequest("IdCartItem must has format Guid");
                if (!Guid.TryParse(idUser, out _)) return BadRequest("IdUser must has format Guid");

                CartItemGetDto cartItemDto = await _ciService.GetById(idCartItem, idUser);
                return StatusCode(200, cartItemDto);
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
        public async Task<ActionResult<List<CartItemGetAllDto>>> GetAllByUserId([FromRoute]string idUser)
        {
            try
            {
                if (!Guid.TryParse(idUser, out _)) return BadRequest("IdUser must has format Guid");

                List<CartItemGetAllDto> dtoList = await _ciService.GetAllByUserId(idUser);
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
        [HttpGet][Route("user/{idUser}/detail")]
        public async Task<ActionResult<List<CartItemGetDto>>> GetDetailByUserId([FromRoute]string idUser)
        {
            try
            {
                if (!Guid.TryParse(idUser, out _)) return BadRequest("IdUser must has format Guid");

                List<CartItemGetDto> detailList = await _ciService.GetDetailByUserId(idUser);
                return StatusCode(200, detailList);
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
        [HttpPut][Route("{idCartItem}/user/{idUser}")]
        public async Task<ActionResult<MainResponse>> Update([FromRoute]string idCartItem,[FromRoute]string idUser, [FromBody]CartItemUpdateDto orderDto)
        {
            try
            {
                if (!Guid.TryParse(idCartItem, out _)) return BadRequest("IdCartItem must has format Guid");
                if (!Guid.TryParse(idUser, out _)) return BadRequest("IdUser must has format Guid");
                ValidationResult bodyReq = new CartItemUpdateValidator().Validate(orderDto);
                if (!bodyReq.IsValid) return BadRequest(bodyReq.Errors);

                await _ciService.Update(idCartItem, idUser, orderDto);
                return StatusCode(200, new MainResponse(true, "CartItem updated with success"));
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
        [HttpDelete][Route("{idCartItem}/user/{idUser}")]
        public async Task<ActionResult> Delete([FromRoute]string idCartItem, [FromRoute]string idUser)
        {
            try
            {
                if (!Guid.TryParse(idCartItem, out _)) return BadRequest("IdCartItem must has format Guid");
                if (!Guid.TryParse(idUser, out _)) return BadRequest("IdUser must has format Guid");

                await _ciService.Delete(idCartItem, idUser);
                return StatusCode(204);
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
