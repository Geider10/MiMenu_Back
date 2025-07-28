using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Data.DTOs.Food;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;
using System.Runtime.InteropServices;

namespace MiMenu_Back.Services
{
    public class FoodService
    {
        private readonly IFoodMapper _foodMap;
        private readonly IFoodRepository _foodRepo;
        public FoodService(IFoodMapper foodMap, IFoodRepository foodRepo)
        {
            _foodMap = foodMap;
            _foodRepo = foodRepo;
        }
        public async Task Add (FoodAddDto food)
        {
            var foodExists = await _foodRepo.ExistsByName(food.Name);
            if (foodExists) throw new MainException("Name of food already exists", 400);

            var foodModel = _foodMap.AddToFoodModel(food);
            await _foodRepo.Add(foodModel);
        }
        public async Task<FoodGetDto> GetById (string id)
        {
            var foodModel = await _foodRepo.GetById(id);
            if (foodModel == null) throw new MainException("Food no found", 404);

            var foodDto = _foodMap.FoodModelToGet(foodModel);
            return foodDto;
        }
        public async Task<List<FoodGetDto>> GetAll(FoodQueryDto foodQuery)
        {
            var foodsList = await _foodRepo.GetAll(foodQuery.Category, foodQuery.SortName, foodQuery.Visibility);
            if (foodsList.Count == 0 || foodsList == null) throw new MainException("There are no foodsList", 404);

            var foodsDtoList = _foodMap.FoodListToGetList(foodsList);
            return foodsDtoList;
        }
        public async Task Update(string id,FoodAddDto food)
        {
            var foodModel = await _foodRepo.GetById(id);
            if (foodModel == null) throw new MainException("Food no found", 404);

            var foodExists = await _foodRepo.ExistsByName(food.Name, id);
            if (foodExists) throw new MainException("Name of food already exists", 400);

            var foodModelUpdate = _foodMap.UpdateToFoodModel(food, foodModel);
            await _foodRepo.Update(foodModelUpdate);
        }
        public async Task Delete (string id)
        {
            var foodModel = await _foodRepo.GetById(id);
            if (foodModel == null) throw new MainException("Food no found", 404);

            await _foodRepo.Delete(foodModel);
        }
        public async Task UpdateImg(string id, ImgUpdateDto imgDto)
        {
            var foodModel = await _foodRepo.GetById(id);
            if (foodModel == null) throw new MainException("Food no found", 404);

            foodModel.ImgUrl = imgDto.ImgUrl;
            await _foodRepo.Update(foodModel);
        }
        public async Task DeleteImg(string id)
        {
            var foodModel = await _foodRepo.GetById(id);
            if (foodModel == null) throw new MainException("Food no found", 404);

            foodModel.ImgUrl = null;
            await _foodRepo.Update(foodModel);
        }
        public async Task UpdateVisibility(string id, VisibilityUpdateDto visibleDto)
        {
            var foodModel = await _foodRepo.GetById(id);
            if (foodModel == null) throw new MainException("Food no found", 404);

            foodModel.Visibility = visibleDto.Visibility;
            await _foodRepo.Update(foodModel);
        }
    }
}
