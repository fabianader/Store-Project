using StoreProject.Common;
using StoreProject.Features.User.DTOs;

namespace StoreProject.Features.User.Services
{
    public interface IUserService
    {
        Task<OperationResult> UserRegister(UserRegisterDto userRegisterDto);
        Task<OperationResult> SixDigitCodeCheck(SixDigitCodeCheckDto sixDigitCodeCheckDto, string forAction);
        Task<OperationResult> UserLogin(UserLoginDto userLoginDto);
        Task<OperationResult> UserForgetPassword(UserForgetPasswordDto userForgetPasswordDto);
        Task<OperationResult> UserResetPassword(UserResetPasswordDto userResetPasswordDto);
        bool IsAny(params string[] items);
        string Encrypt(string token);
        string Decrypt(string token);
    }
}
