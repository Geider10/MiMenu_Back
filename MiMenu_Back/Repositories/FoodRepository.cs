using Microsoft.EntityFrameworkCore;
using MiMenu_Back.Data;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Services;
using System.Runtime.InteropServices;

namespace MiMenu_Back.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly AppDB _appDB;
        public FoodRepository(AppDB appDB)
        {
            _appDB = appDB;
        }
        public async Task<bool> ExistsByName(string name)
        {
            return await _appDB.Foods.AnyAsync(f => f.Name == name);
        }
        public async Task<bool> ExistsByName(string name, string idIgnore)
        {
            return await _appDB.Foods.AnyAsync(f => f.Name == name && f.Id != Guid.Parse(idIgnore));
        }
        public async Task<bool> ExistsByCategoryId(string idCategory)
        {
            return await _appDB.Foods.AnyAsync(f => f.IdCategory == Guid.Parse(idCategory));
        }
        public async Task Add(FoodModel food)
        {
            _appDB.Foods.Add(food);
            await _appDB.SaveChangesAsync();
        }
        public async Task<FoodModel?> GetById(string id)
        {
            return await _appDB.Foods
                .Include(f => f.Category)
                .FirstOrDefaultAsync(f => f.Id == Guid.Parse(id));   
        }
        public async Task<List<FoodModel>?> GetAll(string? category, string? sortName, bool? visibility)
        {
            List<FoodModel>? foods = await _appDB.Foods
                .Include(f => f.Category)
                .ToListAsync();

            if (!string.IsNullOrEmpty(category))
            {
                foods = foods.Where(f => f.Category.Name.ToLower() == category.ToLower()).ToList();
            }
            if(sortName == "asc" && !string.IsNullOrEmpty(sortName))
            {
                foods = foods.OrderBy(f => f.Name).ToList();
            }else if(sortName == "desc" && !string.IsNullOrEmpty(sortName))
            {
                foods = foods.OrderByDescending(f => f.Name).ToList();
            }
            if(visibility.HasValue)
            {
                foods = foods.Where(f => f.Visibility == visibility).ToList();
            }

            return foods;
        }
        public async Task Update(FoodModel food)
        {
            _appDB.Foods.Update(food);
            await _appDB.SaveChangesAsync();
        }
        public async Task Delete(FoodModel food)
        {
            _appDB.Foods.Remove(food);
            await _appDB.SaveChangesAsync();
        }
        public async Task<List<FoodModel>?> GetAllByCategory(string idCategory)
        {
            List<FoodModel>? foodList = await _appDB.Foods
                .Where(f => f.IdCategory == Guid.Parse(idCategory))
                .ToListAsync();
            return foodList;
        }
    }
}
