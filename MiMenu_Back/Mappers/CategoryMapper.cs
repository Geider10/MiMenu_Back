using MiMenu_Back.Data.DTOs.Category;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class CategoryMapper : ICategoryMapper
    {
        public CategoryModel GetToCategoryModel(CategoryAddDto attributeDto)
        {
            return new CategoryModel
            {
                Name = attributeDto.Name,
                Type = attributeDto.Type
            };
        }

        public List<CategoryGetDto> CategoryListToGetList(List<CategoryModel> categoryList)
        {
            var dtosList = new List<CategoryGetDto>();
            foreach(var category in categoryList)
            {
                dtosList.Add(new CategoryGetDto
                {
                    Id = category.Id.ToString(),
                    Name = category.Name,
                });
            }

            return dtosList;
        }
    }
}
