using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IFoodRepository
    {
        Task Add(FoodModel food);
        Task<FoodModel?> GetById(string id);
        Task<List<FoodModel?>> GetAll(string? idCategory, string? sort);
        Task Update(FoodModel food);
    }
}
