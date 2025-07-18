using MiMenu_Back.Data.DTOs.User;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class UserMapper : IUserMapper
    {
        public UserGetDto UserModelToGet(UserModel userModel, string birthDate)
        {
            return new UserGetDto
            {
                Name = userModel.Name,
                Email = userModel.Email,
                Address = userModel.Address,
                BirthDate = birthDate
            };
        }
        public UserModel UpdateToUserModel(UserModel userModel, UserUpdateDto updateDto, DateOnly? birthDate)
        {
            userModel.Name = updateDto.Name;
            userModel.Address = updateDto.Address;
            userModel.BirthDate = birthDate;

            return userModel;
        }
    }
}
