using MiMenu_Back.Data.DTOs.Auth;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;

namespace MiMenu_Back.Services
{
    public class AuthService
    {
        private readonly IAuthMapper _auth;
        private readonly IUserRepository _user;
        private readonly Util _util;
        public AuthService(IAuthMapper auth, IUserRepository user, Util util)
        {
            _auth = auth;
            _user = user;
            _util = util;
        }
        public async Task<string> Signup(SignupDto signupDto)
        {
            bool userExists = await _user.ExistsByEmail(signupDto.Email);
            if (userExists) throw new Exception("Email already registered");

            string passwordHash = _util.HashText(signupDto.Password);
            DateOnly? birthDate = _util.FormatToDateOnly(signupDto.BirthDate);

            var user = _auth.MapSignupDTO(signupDto, passwordHash, birthDate);
            await _user.Add(user);

            return "User registered";
        }
    }
}
