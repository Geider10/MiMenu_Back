using MiMenu_Back.Data.DTOs.User;
using MiMenu_Back.Data.Models;
namespace MiMenu_Back.Mappers.Interfaces
{
    public interface IUserMapper
    {
        GetDto MapUserModel(UserModel userModel, string birthDate);
        UserModel MapUpdateDto(UserModel userModel, UpdateDto updateDto, DateOnly? birthDate);
    }
}
