using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Data.DTOs.User;
using MiMenu_Back.Services;
using FluentValidation.Results;
using MiMenu_Back.Validators.User;
using MiMenu_Back.Utils;

namespace MiMenu_Back.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet][Route("{id}")]
        public async Task<ActionResult<GetDto>> GetById([FromRoute] string id)
        {
            try
            {
                var userDto = await _userService.GetById(id);
                return StatusCode(200, userDto);
            }
            catch (MainException ex)
            {
                return StatusCode(ex.StatusCode, new MainResponse(false, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MainResponse(false, "Internal Server Error: " + ex.Message));
            }
        }
        [HttpPut][Route("{id}")]
        public async Task<ActionResult<MainResponse>> Update([FromRoute]string id, [FromBody] UpdateDto updateDto)
        {
            try
            {
                ValidationResult bodyReq = new UpdateValidator().Validate(updateDto);
                if (!bodyReq.IsValid) return BadRequest(bodyReq.Errors);

                await _userService.Update(id, updateDto);
                return StatusCode(200, new MainResponse(true, "User updated with success"));
            }
            catch (MainException ex)
            {
                return StatusCode(ex.StatusCode, new MainResponse(false, ex.Message));  
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MainResponse(false, "Internal Server Error: " + ex.Message));
            }
        }
        [HttpDelete][Route("{id}")]
        public async Task<ActionResult<MainResponse>> Delete([FromRoute]string id)
        {
            try
            {
                await _userService.Delete(id);
                return StatusCode(200, new MainResponse(true, "User deleted with success"));
            }
            catch (MainException ex)
            {
                return StatusCode(ex.StatusCode, new MainResponse(false, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MainResponse(false, "Internal Server Error: " + ex.Message));
            }
        }
    }
}
