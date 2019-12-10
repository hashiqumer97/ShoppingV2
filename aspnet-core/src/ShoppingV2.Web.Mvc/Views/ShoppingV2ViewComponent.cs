using Abp.AspNetCore.Mvc.ViewComponents;

namespace ShoppingV2.Web.Views
{
    public abstract class ShoppingV2ViewComponent : AbpViewComponent
    {
        protected ShoppingV2ViewComponent()
        {
            LocalizationSourceName = ShoppingV2Consts.LocalizationSourceName;
        }
    }
}
