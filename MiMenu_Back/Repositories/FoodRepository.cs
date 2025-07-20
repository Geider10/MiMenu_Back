using Microsoft.EntityFrameworkCore;
using MiMenu_Back.Data;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Repositories.Interfaces;

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
    }
}
