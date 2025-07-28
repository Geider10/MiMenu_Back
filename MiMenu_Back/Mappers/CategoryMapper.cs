using MiMenu_Back.Data.DTOs.Category;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class CategoryMapper : ICategoryMapper
    {
        public CategoryModel AddToCategoryModel(CategoryAddDto attributeDto)
        {
            return new CategoryModel
            {
                Name = attributeDto.Name,
                Type = attributeDto.Type,
                Visibility = attributeDto.Visibility
            };
        }
        public List<CategoryGetDto> CategoryListToGetList(List<CategoryModel> categoryList)
        {
            var dtoList = new List<CategoryGetDto>();

            foreach(var category in categoryList)
            {
                dtoList.Add(new CategoryGetDto
                {
                    Id = category.Id.ToString(),
                    Name = category.Name,
                    Visibility = category.Visibility
                });
            }
            return dtoList;
        }
    }
}
