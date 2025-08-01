using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Data.DTOs.User;
using MiMenu_Back.Services;
using FluentValidation.Results;
using MiMenu_Back.Validators.User;
using MiMenu_Back.Utils;
using Microsoft.AspNetCore.Authorization;

namespace MiMenu_Back.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize(Roles="client")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet][Route("{id}")]
        public async Task<ActionResult<UserGetDto>> GetById([FromRoute] string id)
        {
            try
            {
                if (!Guid.TryParse(id, out _)) return BadRequest("Id must has format Guid");

                var userDto = await _userService.GetById(id);
                return StatusCode(200, userDto);
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
        [HttpPut][Route("{id}")]
        public async Task<ActionResult<MainResponse>> Update([FromRoute]string id, [FromBody] UserUpdateDto updateDto)
        {
            try
            {
                if (!Guid.TryParse(id, out _)) return BadRequest("Id must has format Guid");
                ValidationResult bodyReq = new UserUpdateValidator().Validate(updateDto);
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
                return StatusCode(500, new MainResponse(false, "Internal server error: " + ex.Message));
            }
        }
        [HttpDelete][Route("{id}")]
        public async Task<ActionResult<MainResponse>> Delete([FromRoute]string id)
        {
            try
            {
                if (!Guid.TryParse(id, out _)) return BadRequest("Id must has format Guid");

                await _userService.Delete(id);
                return StatusCode(200, new MainResponse(true, "User deleted with success"));
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
    }
}
