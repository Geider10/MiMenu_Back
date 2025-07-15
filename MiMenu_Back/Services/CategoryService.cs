using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Data.DTOs.Category;
using MiMenu_Back.Mappers.Interfaces;
namespace MiMenu_Back.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly ICategoryMapper _categoryMap;
        public CategoryService(ICategoryRepository categoryRepo, ICategoryMapper categoryMap)
        {
            _categoryRepo = categoryRepo;
            _categoryMap = categoryMap;
        }

        public async Task Add(AttributeDto attributeDto)
        {
            bool categoryExists = await _categoryRepo.ExistsByName(attributeDto.Name);
            if (categoryExists) throw new MainException("Category already exists", 400);

            var categoryModel = _categoryMap.MapAttributeDto(attributeDto);
            await _categoryRepo.Add(categoryModel);
        }
    }
}
