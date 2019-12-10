using System.Collections.Generic;
using ShoppingV2.Roles.Dto;
using ShoppingV2.Users.Dto;

namespace ShoppingV2.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
