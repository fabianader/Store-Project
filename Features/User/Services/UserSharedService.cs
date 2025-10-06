using Microsoft.AspNetCore.Identity;
using StoreProject.Entities;
using StoreProject.Features.User.DTOs;
using StoreProject.Features.User.Mapper;

namespace StoreProject.Features.User.Services
{
    public class UserSharedService : IUserSharedService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserSharedService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return null;

            return UserMapper.MapAppUserToUserDto(user);
        }
    }
}
