using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost][Route("")]
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
                return StatusCode(500, new MainResponse(false, "Internal Server Error: " + ex.Message));
            }
        }
        [HttpGet][Route("{id}")]
        public async Task<ActionResult<FoodGetDto>> GetById([FromRoute]string id)
        {
            try
            {
                var guid = Guid.NewGuid();
                if (!Guid.TryParse(id, out guid)) return BadRequest("Id must has format Guid");

                var foodDto = await _foodService.GetById(id);
                return StatusCode(200, foodDto);
            }
            catch(MainException ex){
                return StatusCode(ex.StatusCode, new MainResponse(false, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MainResponse(false, "Internal Server Error: " + ex.Message));
            }
        }
    }
}
