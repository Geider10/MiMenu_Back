using Microsoft.EntityFrameworkCore;
using MiMenu_Back.Data;
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
        public async Task Add(OrderModel order)
        {
            _appDB.Orders.Add(order);
            await _appDB.SaveChangesAsync();
        }
        public async Task<OrderModel?> GetById(string id)
        {
            return await _appDB.Orders.FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
        }
        public async Task Update(OrderModel order)
        {
            _appDB.Orders.Update(order);
            await _appDB.SaveChangesAsync();
        }
    }
}
