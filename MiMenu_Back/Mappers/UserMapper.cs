using MiMenu_Back.Data.DTOs.User;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;

namespace MiMenu_Back.Mappers
{
    public class UserMapper : IUserMapper
    {
        public GetDto MapUserModel(UserModel userModel, string birthDate)
        {
            return new GetDto
            {
                Name = userModel.Name,
                Email = userModel.Email,
                Address = userModel.Address,
                BirthDate = birthDate
            };
        }
    }
}
