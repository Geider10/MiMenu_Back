using Microsoft.EntityFrameworkCore;
using MiMenu_Back.Data;
using MiMenu_Back.Data.Enums;
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
        public async Task<OrderModel?> GetByIdPublic(string idPublic)
        {
            return await _appDB.Orders.FirstOrDefaultAsync(o => o.IdPublic == idPublic);
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
        public async Task AddOrderItem(OrderItemModel orderItem)
        {
            _appDB.OrderItems.Add(orderItem);
            await _appDB.SaveChangesAsync();
        }
        public async Task<List<OrderItemModel>?> GetAllByOrderId(string idOrder)
        {
            var itemsList = await _appDB.OrderItems
                .Include(i => i.Food)
                .Where(i => i.IdOrder == Guid.Parse(idOrder))
                .ToListAsync();
            return itemsList;
        }
        public async Task<List<OrderModel>?> GetAllByUserId(string idUser, string? typeOrder)
        {
            var orderList = await _appDB.Orders
                .Include(o => o.Payment)
                .Where(o => o.IdUser == Guid.Parse(idUser))
                .ToListAsync();

            if (typeOrder == "takeaway" &&!string.IsNullOrEmpty(typeOrder))
            {
                orderList = orderList.FindAll(o => o.Type == TypeOrderEnum.TakeAway);
            }else if(typeOrder == "dinein" && !string.IsNullOrEmpty(typeOrder))
            {
                orderList = orderList.FindAll(o => o.Type == TypeOrderEnum.DineIn);
            }
            return orderList;
        }
    }
}
