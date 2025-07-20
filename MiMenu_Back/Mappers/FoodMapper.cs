using MiMenu_Back.Data.DTOs.Food;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class FoodMapper : IFoodMapper
    {
        public FoodModel GetToFoodModel(FoodAddDto food)
        {
            return new FoodModel
            {
                IdCategory = Guid.Parse(food.IdCategory),
                Name = food.Name,
                Description = food.Description,
                ImgUrl = food.ImgUrl,
                Price = food.Price,
                Discount = food.Discount
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
                Discount = food.Discount
            };
        }
    }
}
