using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Data.DTOs.Category;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;
namespace MiMenu_Back.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly ICategoryMapper _categoryMap;
        private readonly IFoodRepository _foodRepo;
        public CategoryService(ICategoryRepository categoryRepo, ICategoryMapper categoryMap, IFoodRepository foodRepo)
        {
            _categoryRepo = categoryRepo;
            _categoryMap = categoryMap;
            _foodRepo = foodRepo;
        }

        public async Task Add(CategoryAddDto categoryDto)
        {
            bool categoryExists = await _categoryRepo.ExistsByName(categoryDto.Name);
            if (categoryExists) throw new MainException("Name of category already exists", 400);

            var categoryModel = _categoryMap.AddToCategoryModel(categoryDto);
            await _categoryRepo.Add(categoryModel);
        }
        public async Task<List<CategoryGetDto>> GetAll(CategoryQueryDto queryParams)
        {
            var categoriesList = await _categoryRepo.GetAll(queryParams.TypeCategory, queryParams.SortName, queryParams.Visibility);
            if (categoriesList.Count == 0 || categoriesList == null) throw new MainException("There are no categories from" + queryParams.TypeCategory, 404);

            var categoriesGetList = _categoryMap.CategoryListToGetList(categoriesList);
            return categoriesGetList;
        }
        public async Task Update (string id, CategoryUpdateDto category)
        {
            var categoryModel = await _categoryRepo.GetById(id);
            if (categoryModel == null) throw new MainException("Category no found", 404);

            bool categoryExists = await _categoryRepo.ExistsByName(category.Name,id);
            if (categoryExists) throw new MainException("Name of category already exists", 400);

            categoryModel.Name = category.Name;
            await _categoryRepo.Update(categoryModel);
        }
        public async Task Delete(string id)
        {
            var categoryModel = await _categoryRepo.GetById(id);
            if (categoryModel == null) throw new MainException("Category no found", 404);

            bool foodExists = await _foodRepo.ExistsByCategoryId(id);
            if (foodExists) throw new MainException("Cannot be deleted because is associated with a food", 400);

            await _categoryRepo.Delete(categoryModel);
        }
        public async Task UpdateVisibility(string id, VisibilityUpdateDto visibleDto)
        {
            var categoryModel = await _categoryRepo.GetById(id);
            if (categoryModel == null) throw new MainException("Category no found", 404);

            if(visibleDto.Visibility == true)
            {
                categoryModel.Visibility = true;
                await _categoryRepo.Update(categoryModel);
            }
            else
            {
                await _foodRepo.UpdateVisibilityByCategory(id, visibleDto.Visibility);
                categoryModel.Visibility = false;
                await _categoryRepo.Update(categoryModel);
            }
        }
    }
}
