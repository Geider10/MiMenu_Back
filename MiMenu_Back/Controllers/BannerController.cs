using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Data.DTOs;
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
        [Authorize(Roles="admin")]
        [HttpGet][Route("{id}")]
        public async Task<ActionResult<BannerGetOneDto>> GetById([FromRoute]string id)
        {
            try
            {
                if (!Guid.TryParse(id, out _)) return BadRequest("Id must has format Guid");

                var bannerDto = await _bannerService.GetById(id);
                return StatusCode(200, bannerDto);
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
        [HttpGet]
        public async Task<ActionResult<List<BannerGetAllDto>>> GetAll([FromQuery] BannerQueryDto queryDto)
        {
            try
            {
                var dtoList = await _bannerService.GetAll(queryDto);
                return StatusCode(200, dtoList);
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
        [Authorize(Roles="admin")]
        [HttpPut][Route("{id}/visible")]
        public async Task<ActionResult<MainResponse>> UpdateVisibility([FromRoute]string id, [FromBody] VisibilityUpdateDto visibleDto)
        {
            try
            {
                if (!Guid.TryParse(id, out _)) return BadRequest("Id must has format Guid");
                if (visibleDto.Visibility != true && visibleDto.Visibility != false) return BadRequest("Visibility of banner must be true or false");

                await _bannerService.UpdateVisibility(id, visibleDto);
                return StatusCode(200, new MainResponse(true, "Visibility of banner updated with success"));
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
