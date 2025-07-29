using Microsoft.EntityFrameworkCore;
using MiMenu_Back.Data;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Repositories.Interfaces;

namespace MiMenu_Back.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDB _appDB;
        public CategoryRepository(AppDB appDB)
        {
            _appDB = appDB;
        }
        public async Task<bool> ExistsByName(string name)
        {
            return await _appDB.Categories.AnyAsync(c => c.Name == name);
        }
        public async Task<bool> ExistsByName(string name, string idIgnore)
        {
            return await _appDB.Categories.AnyAsync(c => c.Name == name && c.Id != Guid.Parse(idIgnore));
        }
        public async Task Add(CategoryModel category)
        {
            _appDB.Categories.Add(category);
            await _appDB.SaveChangesAsync();
        }
        public async Task<List<CategoryModel>> GetAll(string typeCategory, string? sortName, bool? visibility)
        {
            var categories = await _appDB.Categories
                .Where(c => c.Type.ToLower() == typeCategory.ToLower())
                .ToListAsync();

            if(sortName == "asc" && !string.IsNullOrEmpty(sortName))
            {
                categories = categories.OrderBy(c => c.Name).ToList();
            }else if(sortName == "desc" && !string.IsNullOrEmpty(sortName))
            {
                categories = categories.OrderByDescending(c => c.Name).ToList();
            }
            if(visibility.HasValue)
            {
                categories = categories.Where(c => c.Visibility == visibility).ToList();
            }

            return categories;
        }

        public async Task<CategoryModel?> GetById(string id)
        {
            return await _appDB.Categories.FirstOrDefaultAsync(c => c.Id == Guid.Parse(id));
        }

        public async Task Update(CategoryModel category)
        {
            _appDB.Categories.Update(category);
            await _appDB.SaveChangesAsync();
        }
        public async Task Delete (CategoryModel category)
        {
            _appDB.Categories.Remove(category);
            await _appDB.SaveChangesAsync();
        }
    }
}
