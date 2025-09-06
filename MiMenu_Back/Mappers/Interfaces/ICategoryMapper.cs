using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface ICategoryMapper
    {
        CategoryModel AddToCategoryModel(CategoryAddDto attributeDto, TypeCategoryEnum type);
        List<CategoryGetDto> CategoryListToGetList(List<CategoryModel> categoryList);
    }
}
