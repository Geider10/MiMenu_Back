using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Data.DTOs.Auth;
using MiMenu_Back.Services;
using FluentValidation;
using FluentValidation.Results;
using MiMenu_Back.Utils;
using MiMenu_Back.Validators;
namespace MiMenu_Back.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService auth)
        {
            _authService = auth;
        }
        [HttpPost][Route("signup")]
        public async Task<ActionResult<MainResponse>> Signup([FromBody] SignupDto signupDto)
        {
            try
            {
                ValidationResult bodyReq = new SignupValidator().Validate(signupDto);
                if (!bodyReq.IsValid) return BadRequest(bodyReq.Errors);

                await _authService.Signup(signupDto);
                return StatusCode(201, new MainResponse(true, "User registered with success"));
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
        [HttpPost][Route("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                ValidationResult bodyReq = new LoginValidator().Validate(loginDto);
                if (!bodyReq.IsValid) return BadRequest(bodyReq.Errors);

                string token = await _authService.Login(loginDto);
                return StatusCode(200, token);
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
