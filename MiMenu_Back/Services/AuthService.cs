using MiMenu_Back.Data.DTOs.Auth;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;

namespace MiMenu_Back.Services
{
    public class AuthService
    {
        private readonly IAuthMapper _authMap;
        private readonly IUserRepository _userRepo;
        private readonly Util _util;
        public AuthService(IAuthMapper auth, IUserRepository user, Util util)
        {
            _authMap = auth;
            _userRepo = user;
            _util = util;
        }
        public async Task Signup(SignupDto signupDto)
        {
            bool userExists = await _userRepo.ExistsByEmail(signupDto.Email);
            if (userExists) throw new MainException("Email already registered", 400);

            string passwordHash = _util.HashText(signupDto.Password);
            DateOnly? birthDate = _util.FormatToDateOnly(signupDto.BirthDate);

            var userModel = _authMap.SignupToUserModel(signupDto, passwordHash, birthDate);
            await _userRepo.Add(userModel);
        }
        public async Task<string> Login(LoginDto loginDto)
        {
            bool userExists = await _userRepo.ExistsByEmail(loginDto.Email);
            if (!userExists) throw new MainException("Email no registered", 400);

            var userModel = await _userRepo.GetByEmail(loginDto.Email);
            bool passwordMatch = _util.VerifyHashText(loginDto.Password, userModel.Password);
            if (!passwordMatch) throw new MainException("Password incorrect", 400);

            string token = _util.GenerateJWT(userModel.Id.ToString(), userModel.Role);
            return token;
        }
    }
}
