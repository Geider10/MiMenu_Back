using Microsoft.EntityFrameworkCore;
using MiMenu_Back.Data;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Repositories.Interfaces;
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
        public async Task<List<FoodModel?>> GetAll(string? idCategory, string? sort)
        {
            var foods = await _appDB.Foods
                .Include(f => f.Category)
                .ToListAsync();

            if (!string.IsNullOrEmpty(idCategory))
            {
                foods = foods.Where(f => f.IdCategory == Guid.Parse(idCategory)).ToList();
            }
            if(sort == "asc")
            {
                foods = foods.OrderBy(f => f.Name).ToList();
            }else if(sort == "desc")
            {
                foods = foods.OrderByDescending(f => f.Name).ToList();
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
    }
}
