using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface IFoodMapper
    {
        FoodModel AddToFoodModel(FoodAddDto food);
        FoodGetDto FoodModelToGet(FoodModel food);
        List<FoodGetDto> FoodListToGetList(List<FoodModel> foods);
        FoodModel UpdateToFoodModel(FoodUpdateDto foodDto, FoodModel foodModel);
    }
}
