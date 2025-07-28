using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiMenu_Back.Services;
using MiMenu_Back.Utils;
using MiMenu_Back.Data.DTOs.Category;
using MiMenu_Back.Validators.Category;
using Microsoft.IdentityModel.Tokens;
using MiMenu_Back.Data.DTOs;
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
        [HttpPost]
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
                return StatusCode(500, new MainResponse(false, "Internal server error: "+ ex.Message));
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
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MainResponse>> Update (string id, [FromBody] CategoryUpdateDto category)
        {
            try
            {
                var guid = new Guid();
                if (!Guid.TryParse(id, out guid)) return BadRequest("Id must has format Guid");
                if (string.IsNullOrWhiteSpace(category.Name)) return BadRequest("Name is required");

                await _categoryService.Update(id, category);
                return StatusCode(200, new MainResponse(true, "Category updated with success"));
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
        [Authorize(Roles = "admin")]
        [HttpDelete][Route("{id}")]
        public async Task<ActionResult<MainResponse>> Delete (string id)
        {
            try
            {
                var formatId = new Guid();
                if (!Guid.TryParse(id, out formatId)) return BadRequest("Id must has format Guid");

                await _categoryService.Delete(id);
                return StatusCode(200, new MainResponse(true, "Category deleted with success"));
            }
            catch (MainException ex)
            {
                return StatusCode(ex.StatusCode, new MainResponse(false,ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MainResponse(false, "Internal Server Error: " + ex.Message));
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

                await _categoryService.UpdateVisibility(id, visibleDto);
                return StatusCode(200, new MainResponse(true, "Visibility of category updated with success"));
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
