using Microsoft.EntityFrameworkCore;
using MiMenu_Back.Data;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Repositories.Interfaces;

namespace MiMenu_Back.Repositories
{
    public class CategoryRespository : ICategoryRepository
    {
        private readonly AppDB _appDB;
        public CategoryRespository(AppDB appDB)
        {
            _appDB = appDB;
        }
        public async Task<bool> ExistsByName(string name)
        {
            return await _appDB.Categories.AnyAsync(c => c.Name == name);
        }
        public async Task Add(CategoryModel category)
        {
            _appDB.Categories.Add(category);
            await _appDB.SaveChangesAsync();
        }

        public async Task<List<CategoryModel>>? GetAll(string type)
        {
            string typeFormat = type.ToLower().Trim();
            if (typeFormat != "comida" && typeFormat != "cupón") return null;

            var categories = await _appDB.Categories
                .Where(c => c.Type.ToLower() == typeFormat)
                .ToListAsync();

            return categories;
        }
    }
}
