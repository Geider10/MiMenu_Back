using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Data.Models;

namespace MiMenu_Back.Mappers.Interfaces
{
    public interface IAuthMapper
    {
        UserModel SignupToUserModel(SignupDto signupDto, string passwordHash, DateOnly? birthDate);
    }
}
