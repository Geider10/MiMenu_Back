using MiMenu_Back.Data.DTOs.User;
using MiMenu_Back.Mappers.Interfaces;
using MiMenu_Back.Repositories.Interfaces;
using MiMenu_Back.Utils;

namespace MiMenu_Back.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserMapper _userMap;
        private readonly Util _util;
        public UserService(IUserRepository userRepo, IUserMapper userMap, Util util)
        {
            _userRepo = userRepo;
            _userMap = userMap;
            _util = util;  
        }
        public async Task<GetDto> GetById(string id)
        {
            var userModel = await _userRepo.GetById(id);
            if (userModel == null) throw new Exception("User no found");

            string birthDate = _util.FormatToString(userModel.BirthDate);
            return _userMap.MapUserModel(userModel, birthDate);
        }
    }
}
