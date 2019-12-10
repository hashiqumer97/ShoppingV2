using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ShoppingV2.Controllers
{
    public abstract class ShoppingV2ControllerBase: AbpController
    {
        protected ShoppingV2ControllerBase()
        {
            LocalizationSourceName = ShoppingV2Consts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
