using MiMenu_Back.Data.DTOs.Food;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;

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
            var foodModel = _foodMap.GetToFoodModel(food);
            await _foodRepo.Add(foodModel);
        }
        public async Task<FoodGetDto> GetById (string id)
        {
            var foodModel = await _foodRepo.GetById(id);
            if (foodModel == null) throw new MainException("Food no found", 404);

            var foodDto = _foodMap.FoodModelToGet(foodModel);
            return foodDto;
        }

    }
}
