using StoreProject.Entities;
using StoreProject.Features.User.DTOs;
using StoreProject.Common;
using StoreProject.Features.User.Model;

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

        public static UserPanelProfileModel MapUserDtoToUserPanelProfileModel(UserDto userDto)
        {
            return new UserPanelProfileModel()
            {
                UserName = userDto.UserName,
                FullName = userDto.FullName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                RegisterDate = userDto.RegisterDate,
                ProfilePicture = userDto.ProfilePicture
            };
        }

        public static ApplicationUser MapUserPanelEditDtoToAppUser(UserPanelEditDto userPanelEditDto, ApplicationUser user)
        {
            user.UserName = userPanelEditDto.UserName;
            user.FullName = userPanelEditDto.FullName;
            user.Email = userPanelEditDto.Email;
            user.PhoneNumber = userPanelEditDto.PhoneNumber;
            user.ProfilePicture = userPanelEditDto.ProfilePictureName;

            return user;
        }

        public static UserPanelEditProfileModel MapUserDtoToUserPanelEditProfileModel(UserDto userDto)
        {
            return new UserPanelEditProfileModel()
            {
                UserName = userDto.UserName,
                FullName = userDto.FullName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                ProfilePictureName = userDto.ProfilePicture,
            };
        }

        public static UserPanelEditDto MapUserPanelEditProfileModelToUserPanelEditDto(UserPanelEditProfileModel model)
        {
            return new UserPanelEditDto()
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                ProfilePictureName = model.ProfilePictureName
            };
        }

	}
}
