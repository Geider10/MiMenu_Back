using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<bool> ExistsByName(string name);
        Task Add(CategoryModel category);
        Task<List<CategoryModel>>? GetAll(string type);
    }
}
