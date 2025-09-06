using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class FoodMapper : IFoodMapper
    {
        public FoodModel AddToFoodModel(FoodAddDto food)
        {
            return new FoodModel
            {
                IdCategory = Guid.Parse(food.IdCategory),
                Name = food.Name,
                Description = food.Description,
                ImgUrl = food.ImgUrl,
                Price = food.Price,
                Discount = food.Discount,
                Visibility = food.Visibility
            };
        }
        public FoodGetDto FoodModelToGet(FoodModel food)
        {
            return new FoodGetDto
            {
                Id = food.Id.ToString(),
                Category = food.Category.Name,
                Name = food.Name,
                Description = food.Description,
                ImgUrl = food.ImgUrl,
                Price = food.Price,
                Discount = food.Discount,
                Visibility = food.Visibility
            };
        }
        public List<FoodGetDto> FoodListToGetList(List<FoodModel> foods)
        {
            var foodDtoList = new List<FoodGetDto>();

            foreach (var food in foods)
            {
                var foodDto = FoodModelToGet(food);
                foodDtoList.Add(foodDto);
            }
            return foodDtoList;
        }
        public FoodModel UpdateToFoodModel(FoodUpdateDto foodDto, FoodModel foodModel)
        {
            foodModel.IdCategory = Guid.Parse(foodDto.IdCategory);
            foodModel.Name = foodDto.Name;
            foodModel.Description = foodDto.Description;
            foodModel.Price = foodDto.Price;
            foodModel.Discount = foodDto.Discount;

            return foodModel;
        }
    }
}
