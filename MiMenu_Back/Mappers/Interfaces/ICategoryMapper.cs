using MiMenu_Back.Data.DTOs.Category;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface ICategoryMapper
    {
        CategoryModel GetDto(CategoryAddDto attributeDto);
        List<CategoryGetDto> CategoryModelList(List<CategoryModel> categoryList);
    }
}
