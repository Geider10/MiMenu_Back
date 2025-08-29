using Microsoft.EntityFrameworkCore;
using MiMenu_Back.Data;
using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Repositories.Interfaces;

namespace MiMenu_Back.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly AppDB _appDB;
        public CartItemRepository(AppDB appDB)
        {
            _appDB = appDB;
        }
        public async Task<bool> ExistsByUserFood(string? idFood, string idUser)
        {
            if (!string.IsNullOrEmpty(idFood))
            {
                return await _appDB.CartItems.AnyAsync(o => o.IdFood == Guid.Parse(idFood) && o.IdUser == Guid.Parse(idUser));
            }
            return false;
        }
        public async Task<bool> ExistsByFoodId(string idFood)
        {
            return await _appDB.CartItems.AnyAsync(o => o.IdFood == Guid.Parse(idFood));
        }
        public async Task Add(CartItemModel cartItem)
        {
            _appDB.CartItems.Add(cartItem);
            await _appDB.SaveChangesAsync();
        }
        public async Task<CartItemModel?> GetById(string id)
        {
            return await _appDB.CartItems
                .Include(o => o.Food)
                .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id) && o.Food.Visibility == true);
        }
        public async Task<List<CartItemModel>?> GetAllByUserId(string idUser)
        {
            var orderList = await _appDB.CartItems
                .Include(o => o.Food)
                .Where(o => o.IdUser == Guid.Parse(idUser) && o.Food.Visibility == true)
                .ToListAsync();
            return orderList;
        }
        public async Task Update(CartItemModel cartItem)
        {
            _appDB.CartItems.Update(cartItem);
            await _appDB.SaveChangesAsync();
        }
        public async Task Delete(CartItemModel cartItem)
        {
            _appDB.CartItems.Remove(cartItem);
            await _appDB.SaveChangesAsync();
        }
       
    }
}
