using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmail(string email);
        Task Add(UserModel user);
    }
}
