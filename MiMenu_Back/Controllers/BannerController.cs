using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Data.DTOs.Banner;
using MiMenu_Back.Services;
using MiMenu_Back.Utils;
using MiMenu_Back.Validators.Banner;

namespace MiMenu_Back.Controllers
{
    [Route("api/banner")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly BannerService _bannerService;
        public BannerController(BannerService bannerService)
        {
            _bannerService = bannerService;
        }
        [Authorize(Roles="admin")]
        [HttpPost]
        public async Task<ActionResult<MainResponse>> Add([FromBody] BannerAddDto bannerDto)
        {
            try
            {
                ValidationResult bodyReq = new BannerAddValidator().Validate(bannerDto);
                if (!bodyReq.IsValid) return BadRequest(bodyReq.Errors);

                await _bannerService.Add(bannerDto);
                return StatusCode(201, new MainResponse(true, "Banner created with success"));
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
