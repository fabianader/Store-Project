using StoreProject.Common;
using System.Reflection.PortableExecutable;

namespace StoreProject.Features.User.DTOs
{
    public class UserFilterDto : BasePagination
    {
        public List<UserDto> Users { get; set; }
        public UserFilterParamsDto UserFilterParams { get; set; }
    }

    public class UserFilterParamsDto
    {
        public int PageId { get; set; }
        public int Take { get; set; }

        public string UserName { get; set; }
        public List<string> Roles { get; set; }
        public DateTime? RegisterDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
