using MiMenu_Back.Data.Enums;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<bool> ExistsByName(string name);
        Task<bool> ExistsByName(string name, string idIgnore);
        Task Add(CategoryModel category);
        Task<List<CategoryModel>> GetAll(TypeCategoryEnum typeCategory, string? sortName, bool? visibility);
        Task<CategoryModel?> GetById(string id);
        Task Update(CategoryModel category);
        Task Delete(CategoryModel category);
    }
}
