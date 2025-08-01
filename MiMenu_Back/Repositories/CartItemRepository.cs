﻿using Microsoft.EntityFrameworkCore;
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
        public async Task Add(CartItem cartItem)
        {
            _appDB.CartItems.Add(cartItem);
            await _appDB.SaveChangesAsync();
        }
        public async Task<CartItem?> GetById(string id)
        {
            return await _appDB.CartItems
                .Include(o => o.Food)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

        }
        public async Task<List<CartItem>?> GetAllByUserId(string idUser)
        {
            var orderList = await _appDB.CartItems
                .Include(o => o.Food)
                .Include(o => o.User)
                .Where(o => o.IdUser == Guid.Parse(idUser))
                .ToListAsync();

            return orderList;
        }
        public async Task Update(CartItem cartItem)
        {
            _appDB.CartItems.Update(cartItem);
            await _appDB.SaveChangesAsync();
        }

        public async Task Delete(CartItem cartItem)
        {
            _appDB.CartItems.Remove(cartItem);
            await _appDB.SaveChangesAsync();
        }
       
    }
}
