using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Data.DTOs.Auth;
using MiMenu_Back.Services;
using FluentValidation;
using FluentValidation.Results;
using MiMenu_Back.Validators.Auth;

namespace MiMenu_Back.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;
        public AuthController(AuthService auth)
        {
            _auth = auth;
        }
        [HttpPost][Route("signup")]
        public async Task<ActionResult> Singup([FromBody] SignupDto signupDto)
        {
            try
            {
                ValidationResult bodyReq = new SignupValidator().Validate(signupDto);
                if (!bodyReq.IsValid) return BadRequest(bodyReq.Errors);

                string res = await _auth.Signup(signupDto);
                return Created(res, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost][Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                ValidationResult bodyReq = new LoginValidator().Validate(loginDto);
                if (!bodyReq.IsValid) return BadRequest(bodyReq.Errors);

                string res = await _auth.Login(loginDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
