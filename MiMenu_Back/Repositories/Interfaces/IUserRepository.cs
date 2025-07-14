using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmail(string email);
        Task Add(UserModel user);
        Task<UserModel> GetByEmail(string email);
        Task<UserModel> GetById(string id);
        Task Update(UserModel user);
    }
}
