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
            var userDto = _userMap.MapUserModel(userModel, birthDate);
            return userDto;
        }
        public async Task<string> Update(string id,UpdateDto updateDto)
        {
            var userModel = await _userRepo.GetById(id);
            if(userModel == null) throw new Exception("User no found");

            DateOnly? birthDate = _util.FormatToDateOnly(updateDto.BirthDate);
            var userUpdated = _userMap.MapUpdateDto(userModel, updateDto, birthDate);
            await _userRepo.Update(userUpdated);

            return "User updated";
        }
    }
}
