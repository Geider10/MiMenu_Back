using MiMenu_Back.Data.DTOs.Auth;
using MiMenu_Back.Data.Models;
using MiMenu_Back.Mappers.Interfaces;
namespace MiMenu_Back.Mappers
{
    public class AuthMapper : IAuthMapper
    {
        public UserModel SignupToUserModel(SignupDto signupDto, string passwordHash, DateOnly? birthDate)
        {
            return new UserModel
            {
                Name = signupDto.Name,
                Email = signupDto.Email,
                Password = passwordHash,
                Address = signupDto.Address,
                BirthDate = birthDate
            };
        }
    }
}
