using MiMenu_Back.Data.DTOs.Category;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class CategoryMapper : ICategoryMapper
    {
        public CategoryModel MapAttributeDto(AttributeDto attributeDto)
        {
            return new CategoryModel
            {
                Name = attributeDto.Name
            };
        }
    }
}
