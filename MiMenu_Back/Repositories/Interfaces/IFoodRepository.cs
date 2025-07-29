using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IFoodRepository
    {
        Task<bool> ExistsByName(string name);
        Task<bool> ExistsByName(string name, string idIgnore);
        Task<bool> ExistsByCategoryId(string idCategory);
        Task Add(FoodModel food);
        Task<FoodModel?> GetById(string id);
        Task<List<FoodModel>?> GetAll(string? category, string? sortName, bool? visibility);
        Task Update(FoodModel food);
        Task Delete(FoodModel food);
        Task UpdateVisibilityByCategory(string idCategory, bool visible);
    }
}
