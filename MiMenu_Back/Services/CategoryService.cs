using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Data.DTOs.Category;
using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;
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
        private readonly Util _util;
        public CategoryService(ICategoryRepository categoryRepo, ICategoryMapper categoryMap, IFoodRepository foodRepo, Util util)
        {
            _categoryRepo = categoryRepo;
            _categoryMap = categoryMap;
            _foodRepo = foodRepo;
            _util = util;
        }
        public async Task Add(CategoryAddDto categoryDto)
        {
            bool categoryExists = await _categoryRepo.ExistsByName(categoryDto.Name);
            if (categoryExists) throw new MainException("Name of category already exists", 409);
            TypeCategoryEnum typeCategory = _util.FormatTypeCategory(categoryDto.Type);

            CategoryModel categoryModel = _categoryMap.AddToCategoryModel(categoryDto, typeCategory);
            await _categoryRepo.Add(categoryModel);
        }
        public async Task<List<CategoryGetDto>> GetAll(CategoryQueryDto queryParams)
        {
            TypeCategoryEnum typeCategory = _util.FormatTypeCategory(queryParams.TypeCategory);
            List<CategoryModel> categoriesList = await _categoryRepo.GetAll(typeCategory, queryParams.SortName, queryParams.Visibility);
            if (categoriesList.Count == 0 || categoriesList == null) throw new MainException("There are no categories from: " + queryParams.TypeCategory, 404);

            List<CategoryGetDto> dtoList = _categoryMap.CategoryListToGetList(categoriesList);
            return dtoList;
        }
        public async Task Update (string id, CategoryUpdateDto category)
        {
            CategoryModel? categoryModel = await _categoryRepo.GetById(id);
            if (categoryModel == null) throw new MainException("Category no found", 404);

            bool categoryExists = await _categoryRepo.ExistsByName(category.Name,id);
            if (categoryExists) throw new MainException("Name of category already exists", 409);

            categoryModel.Name = category.Name;
            await _categoryRepo.Update(categoryModel);
        }
        public async Task Delete(string id)
        {
            CategoryModel? categoryModel = await _categoryRepo.GetById(id);
            if (categoryModel == null) throw new MainException("Category no found", 404);

            if(categoryModel.Type == TypeCategoryEnum.Food)
            {
                bool foodExists = await _foodRepo.ExistsByCategoryId(id);
                if (foodExists) throw new MainException("Cannot be deleted because is associated with a food", 422);
            }
            await _categoryRepo.Delete(categoryModel);
        }
        public async Task UpdateVisibility(string id, VisibilityUpdateDto visibleDto)
        {
            CategoryModel? categoryModel = await _categoryRepo.GetById(id);
            if (categoryModel == null) throw new MainException("Category no found", 404);

            if(visibleDto.Visibility == false)
            {
                if(categoryModel.Type == TypeCategoryEnum.Food)
                {
                    List<FoodModel>? foodList = await _foodRepo.GetAllByCategory(id);
                    foreach (FoodModel f in foodList)
                    {
                        f.Visibility = visibleDto.Visibility;
                        await _foodRepo.Update(f);
                    }
                }
            }
            categoryModel.Visibility = visibleDto.Visibility;
            await _categoryRepo.Update(categoryModel);
        }
    }
}
