using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Data.DTOs.Food;
using MiMenu_Back.Services;
using MiMenu_Back.Utils;
using MiMenu_Back.Validators.Food;
namespace MiMenu_Back.Controllers
{
    [Route("api/food")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly FoodService _foodService;
        public FoodController(FoodService foodService)
        {
            _foodService = foodService;
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<MainResponse>> Add([FromBody] FoodAddDto food)
        {
            try
            {
                ValidationResult bodyReq = new FoodAddValidator().Validate(food);
                if (!bodyReq.IsValid) return BadRequest(bodyReq.Errors);

                await _foodService.Add(food);
                return StatusCode(201, new MainResponse(true, "Food created with success"));
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
        [HttpGet][Route("{id}")]
        public async Task<ActionResult<FoodGetDto>> GetById([FromRoute] string id)
        {
            try
            {
                if (!Guid.TryParse(id, out _)) return BadRequest("Id must has format Guid");

                FoodGetDto foodDto = await _foodService.GetById(id);
                return StatusCode(200, foodDto);
            }
            catch (MainException ex) {
                return StatusCode(ex.StatusCode, new MainResponse(false, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MainResponse(false, "Internal server error: " + ex.Message));
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<FoodGetDto>>> GetAll([FromQuery] FoodQueryDto foodQuery)
        {
            try
            {
                List<FoodGetDto> foodsDtoList = await _foodService.GetAll(foodQuery);
                return StatusCode(200, foodsDtoList);
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
        [Authorize(Roles = "admin")]
        [HttpPut][Route("{id}")]
        public async Task<ActionResult<MainResponse>> Update([FromRoute] string id, [FromBody] FoodAddDto food)
        {
            try
            {
                if (!Guid.TryParse(id, out _)) return BadRequest("Id must has format Guid");
                ValidationResult bodyReq = new FoodAddValidator().Validate(food);
                if (!bodyReq.IsValid) return BadRequest(bodyReq.Errors);

                await _foodService.Update(id, food);
                return StatusCode(200, new MainResponse(true, "Food updated with success"));
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
        [Authorize(Roles = "admin")]
        [HttpDelete][Route("{id}")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            try
            {
                if (!Guid.TryParse(id, out _)) return BadRequest("Id must has format Guid");

                await _foodService.Delete(id);
                return StatusCode(204);
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
        [Authorize(Roles = "admin")]
        [HttpPut][Route("{id}/image")]
        public async Task<ActionResult<MainResponse>> UpdateImg([FromRoute] string id, [FromBody] ImgUpdateDto imgDto)
        {
            try
            {
                if (!Guid.TryParse(id, out _)) return BadRequest("Id must has format Guid");
                if (string.IsNullOrWhiteSpace(imgDto.ImgUrl)) return BadRequest("ImgUrl is required");

                await _foodService.UpdateImg(id, imgDto);
                return StatusCode(200, new MainResponse(true, "Image of food updated with success"));
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
        [Authorize(Roles = "admin")]
        [HttpDelete][Route("{id}/image")]
        public async Task<ActionResult> DeleteImg([FromRoute]string id)
        {
            try
            {
                if (!Guid.TryParse(id, out _)) return BadRequest("Id must has format Guid");

                await _foodService.DeleteImg(id);
                return StatusCode(204);
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
        [Authorize(Roles ="admin")]
        [HttpPut][Route("{id}/visible")]
        public async Task<ActionResult<MainResponse>> UpdateVisibility([FromRoute]string id, [FromBody]VisibilityUpdateDto visibleDto)
        {
            try
            {
                if (!Guid.TryParse(id, out _)) return BadRequest("Id must has format Guid");
                if (visibleDto.Visibility != true && visibleDto.Visibility != false) return BadRequest("Visibility must be true or false");

                await _foodService.UpdateVisibility(id, visibleDto);
                return StatusCode(200, new MainResponse(true, "Visibility of food updated with success"));
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
