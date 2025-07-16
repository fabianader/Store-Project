using StoreProject.Entities;
using StoreProject.Features.User.DTOs;
using StoreProject.Common;

namespace StoreProject.Features.User.Mapper
{
    public class UserMapper
    {
        public static ApplicationUser MapUserRegisterDtoToAppUser(UserRegisterDto userRegisterDto)
        {
            return new ApplicationUser()
            {
                UserName = userRegisterDto.UserName,
                FullName = userRegisterDto.FullName,
                Email = userRegisterDto.Email,
                PhoneNumber = userRegisterDto.PhoneNumber,
                IsDeleted = false,
                ProfilePicture = "user.png",
                RegisterDate = DateTime.Now
            };
        }
        public static UserDto MapAppUserToUserDto(ApplicationUser user)
        {
            return new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ProfilePicture = user.ProfilePicture,
                RegisterDate = user.RegisterDate,
                IsDeleted = user.IsDeleted
            };
        }

        public static ApplicationUser MapEditUserToAppUser(UserEditDto editUserDto, ApplicationUser user)
        {
            user.UserName = editUserDto.UserName;
            user.FullName = editUserDto.FullName;
            user.Email = editUserDto.Email;
            user.PhoneNumber = editUserDto.PhoneNumber;
            user.IsDeleted = editUserDto.IsDeleted;

            return user;
        }
    }
}
