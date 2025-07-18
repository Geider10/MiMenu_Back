using MiMenu_Back.Data.DTOs.User;
using MiMenu_Back.Data.Models;
namespace MiMenu_Back.Mappers.Interfaces
{
    public interface IUserMapper
    {
        UserGetDto UserModelToGet(UserModel userModel, string birthDate);
        UserModel UpdateToUserModel(UserModel userModel, UserUpdateDto updateDto, DateOnly? birthDate);
    }
}
