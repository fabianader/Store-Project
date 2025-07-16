using StoreProject.Common;
using StoreProject.Features.Admin.DTOs;
using StoreProject.Features.User.DTOs;

namespace StoreProject.Features.Admin.Services
{
    public interface IUserManagementService
    {
        bool IsAny(string[] items);
        List<UserDto> GetAllUsers();
        List<string> GetAllRolesName();
        int GetAllRolesCount();
        UserFilterDto GetUsersByFilter(UserFilterParamsDto userFilterParamsDto);
        Task<UserDto> GetUserByIdAsync(string id);
        Task<List<string>> GetUserRolesAsync(string id);
        Task<OperationResult> CreateUserAsync(UserCreateDto userCreateDto);
        Task<OperationResult> EditUserAsync(UserEditDto userEditDto);
    }
}
