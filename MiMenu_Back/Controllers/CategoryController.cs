using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Services;
using MiMenu_Back.Utils;
using MiMenu_Back.Data.DTOs.Category;
using MiMenu_Back.Validators.Category;
namespace MiMenu_Back.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [Authorize(Roles = "admin")]
        [HttpPost][Route("")]
        public async Task<ActionResult<MainResponse>> Add([FromBody] CategoryAddDto attributeDto)
        {
            try
            {
                ValidationResult bodyReq = new CategoryAddValidator().Validate(attributeDto);
                if (!bodyReq.IsValid) return BadRequest(bodyReq.Errors);

                await _categoryService.Add(attributeDto);
                return StatusCode(201, new MainResponse(true, "Category created with success"));
            }
            catch (MainException ex)
            {
                return StatusCode(ex.StatusCode, new MainResponse(false, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MainResponse(false, "Internal Server Error: "+ ex.Message));
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<CategoryGetDto>>> GetAll([FromQuery] CategoryQueryDto queryParams)
        {
            try
            {
                ValidationResult queryReq = new CategoryQueryValidator().Validate(queryParams);
                if (!queryReq.IsValid) return BadRequest(queryReq.Errors);

                var categoryGetList = await _categoryService.GetAll(queryParams);
                return StatusCode(200, categoryGetList);
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
