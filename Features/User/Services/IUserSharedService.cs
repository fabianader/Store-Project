using StoreProject.Features.User.DTOs;

namespace StoreProject.Features.User.Services
{
    public interface IUserSharedService
    {
        Task<UserDto> GetUserByIdAsync(string id);
    }
}
