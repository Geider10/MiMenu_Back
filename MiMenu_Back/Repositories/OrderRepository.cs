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
        public async Task<bool> ExistsByUserFood(string idFood, string idUser)
        {
            return await _appDB.Orders.AnyAsync(order => order.IdFood == Guid.Parse(idFood) && order.IdUser == Guid.Parse(idUser));
        }
        public async Task Add(OrderModel order)
        {
            _appDB.Orders.Add(order);
            await _appDB.SaveChangesAsync();
        }
    }
}
