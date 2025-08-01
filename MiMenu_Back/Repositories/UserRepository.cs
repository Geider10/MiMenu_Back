using MiMenu_Back.Data.Models;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Data;
using Microsoft.EntityFrameworkCore;

namespace MiMenu_Back.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDB _appDB;
        public UserRepository(AppDB appDB)
        {
            _appDB = appDB;
        }
        public async Task<bool> ExistsByEmail(string email)
        {
            return await _appDB.Users.AnyAsync(u => u.Email == email);
        }
        public async Task Add(UserModel user)
        {
            _appDB.Users.Add(user);
            await _appDB.SaveChangesAsync();
        }
        public async Task<UserModel?> GetByEmail(string email)
        {
            return await _appDB.Users
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<UserModel?> GetById(string id)
        {
            return await _appDB.Users
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Id == Guid.Parse(id));
        }
        public async Task Update(UserModel user)
        {
            _appDB.Users.Update(user);
            await _appDB.SaveChangesAsync();
        }
        public async Task Delete(UserModel user)
        {
            _appDB.Users.Remove(user);
            await _appDB.SaveChangesAsync();
        }
    }
}
