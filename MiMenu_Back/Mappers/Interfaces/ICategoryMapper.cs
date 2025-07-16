using MiMenu_Back.Data.DTOs.Category;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface ICategoryMapper
    {
        CategoryModel MapAttributeDto(AttributeDto attributeDto);
        List<AttributeDto> MapCategoryModelList(List<CategoryModel> categoryList);
    }
}
