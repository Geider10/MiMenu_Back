using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IFoodRepository
    {
        Task Add(FoodModel food);
    }
}
