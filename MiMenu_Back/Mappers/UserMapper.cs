using MiMenu_Back.Data.DTOs;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class UserMapper : IUserMapper
    {
        public UserGetDto UserModelToGet(UserModel userModel, string? birthDate)
        {
            return new UserGetDto
            {
                Name = userModel.Name,
                Email = userModel.Email,
                Phone = userModel.Phone,
                BirthDate = birthDate
            };
        }
        public UserModel UpdateToUserModel(UserModel userModel, UserUpdateDto updateDto, DateOnly? birthDate)
        {
            userModel.Name = updateDto.Name;
            userModel.Phone = updateDto.Phone;
            userModel.BirthDate = birthDate;

            return userModel;
        }
    }
}
