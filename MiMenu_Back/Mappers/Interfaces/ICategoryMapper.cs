using MiMenu_Back.Data.DTOs.Category;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface ICategoryMapper
    {
        CategoryModel GetToCategoryModel(CategoryAddDto attributeDto);
        List<CategoryGetDto> CategoryListToGetList(List<CategoryModel> categoryList);
    }
}
