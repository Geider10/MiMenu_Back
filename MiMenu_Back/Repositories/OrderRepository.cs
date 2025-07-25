using Microsoft.EntityFrameworkCore;
using MiMenu_Back.Data;
using MiMenu_Back.Data.DTOs.Order;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Repositories.Interfaces;

namespace MiMenu_Back.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDB _appDB;
        public OrderRepository(AppDB appDB)
        {
            _appDB = appDB;
        }
        public async Task<bool> ExistsByUserFood(string idFood, string idUser)
        {
            return await _appDB.Orders.AnyAsync(o => o.IdFood == Guid.Parse(idFood) && o.IdUser == Guid.Parse(idUser));
        }
        public async Task Add(OrderModel order)
        {
            _appDB.Orders.Add(order);
            await _appDB.SaveChangesAsync();
        }
        public async Task<OrderModel?> GetById(string id)
        {
            return await _appDB.Orders
                .Include(o => o.Food)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

        }
        public async Task<List<OrderModel>?> GetAllByUserId(string idUser)
        {
            var orderList = await _appDB.Orders
                .Include(o => o.Food)
                .Include(o => o.User)
                .Where(o => o.IdUser == Guid.Parse(idUser))
                .ToListAsync();

            return orderList;
        }
        public async Task Update(OrderModel order)
        {
            _appDB.Orders.Update(order);
            await _appDB.SaveChangesAsync();
        }

        public async Task Delete(OrderModel order)
        {
            _appDB.Orders.Remove(order);
            await _appDB.SaveChangesAsync();
        }
    }
}
