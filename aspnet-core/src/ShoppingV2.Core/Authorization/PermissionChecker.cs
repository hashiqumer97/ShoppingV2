using Abp.Authorization;
using ShoppingV2.Authorization.Roles;
using ShoppingV2.Authorization.Users;

namespace ShoppingV2.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
