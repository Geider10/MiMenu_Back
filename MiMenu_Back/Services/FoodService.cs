using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Data.DTOs.Shared;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;

namespace MiMenu_Back.Services
{
    public class FoodService
    {
        private readonly IFoodMapper _foodMap;
        private readonly IFoodRepository _foodRepo;
        private readonly ICartItemRepository _cartItemRepo;
        public FoodService(IFoodMapper foodMap, IFoodRepository foodRepo, ICartItemRepository orderRepo)
        {
            _foodMap = foodMap;
            _foodRepo = foodRepo;
            _cartItemRepo = orderRepo;
        }
        public async Task Add (FoodAddDto food)
        {
            bool foodExists = await _foodRepo.ExistsByName(food.Name);
            if (foodExists) throw new MainException("Name of food already exists", 409);
            if (food.Discount > 100 || food.Discount >= 0 && food.Discount <=4) throw new MainException("Discount must be between 5 and 100",422);

            FoodModel foodModel = _foodMap.AddToFoodModel(food);
            await _foodRepo.Add(foodModel);
        }
        public async Task<FoodGetDto> GetById (string id)
        {
            FoodModel? foodModel = await _foodRepo.GetById(id);
            if (foodModel == null) throw new MainException("Food no found", 404);

            FoodGetDto foodDto = _foodMap.FoodModelToGet(foodModel);
            return foodDto;
        }
        public async Task<List<FoodGetDto>> GetAll(FoodQueryDto foodQuery)
        {
            List<FoodModel>? foodsList = await _foodRepo.GetAll(foodQuery.Category, foodQuery.SortName, foodQuery.Visibility);
            if (foodsList.Count == 0 || foodsList == null) throw new MainException("There are no foodsList", 404);

            List<FoodGetDto> foodsDtoList = _foodMap.FoodListToGetList(foodsList);
            return foodsDtoList;
        }
        public async Task Update(string id,FoodUpdateDto food)
        {
            FoodModel? foodModel = await _foodRepo.GetById(id);
            if (foodModel == null) throw new MainException("Food no found", 404);

            bool foodExists = await _foodRepo.ExistsByName(food.Name, id);
            if (foodExists) throw new MainException("Name of food already exists", 409);

            FoodModel foodModelUpdate = _foodMap.UpdateToFoodModel(food, foodModel);
            await _foodRepo.Update(foodModelUpdate);
        }
        public async Task Delete (string id)
        {
            FoodModel? foodModel = await _foodRepo.GetById(id);
            if (foodModel == null) throw new MainException("Food no found", 404);

            bool ciExists = await _cartItemRepo.ExistsByFoodId(id);
            if(ciExists) throw new MainException("Cannot be deleted because is associated with a cart", 422);

            await _foodRepo.Delete(foodModel);
        }
        public async Task UpdateImg(string id, ImgUpdateDto imgDto)
        {
            FoodModel? foodModel = await _foodRepo.GetById(id);
            if (foodModel == null) throw new MainException("Food no found", 404);

            foodModel.ImgUrl = imgDto.ImgUrl;
            await _foodRepo.Update(foodModel);
        }
        public async Task DeleteImg(string id)
        {
            FoodModel? foodModel = await _foodRepo.GetById(id);
            if (foodModel == null) throw new MainException("Food no found", 404);

            foodModel.ImgUrl = null;
            await _foodRepo.Update(foodModel);
        }
        public async Task UpdateVisibility(string id, VisibilityUpdateDto visibleDto)
        {
            FoodModel? foodModel = await _foodRepo.GetById(id);
            if (foodModel == null) throw new MainException("Food no found", 404);

            foodModel.Visibility = visibleDto.Visibility;
            await _foodRepo.Update(foodModel);
        }
    }
}
