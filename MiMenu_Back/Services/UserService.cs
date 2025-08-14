using MiMenu_Back.Data.DTOs.User;
using MiMenu_Back.Data.Models;
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
        public async Task<UserGetDto> GetById(string id)
        {
            var userModel = await _userRepo.GetById(id);
            if (userModel == null) throw new MainException("User no found", 404);

            string? birthDate = _util.DateOnlyToString(userModel.BirthDate);
            var userDto = _userMap.UserModelToGet(userModel, birthDate);
            return userDto;
        }
        public async Task Update(string id,UserUpdateDto updateDto)
        {
            var userModel = await _userRepo.GetById(id);
            if(userModel == null) throw new MainException("User no found", 404);

            DateOnly? birthDate = _util.StringToDateOnly(updateDto.BirthDate);
            var userUpdated = _userMap.UpdateToUserModel(userModel, updateDto, birthDate);
            await _userRepo.Update(userUpdated);
        }
        public async Task Delete(string id)
        {
            var userModel = await _userRepo.GetById(id);
            if (userModel == null) throw new MainException("User no found", 404);

            await _userRepo.Delete(userModel);
        }
    }
}
