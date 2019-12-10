using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;

namespace ShoppingV2.Web.Views
{
    public abstract class ShoppingV2RazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected ShoppingV2RazorPage()
        {
            LocalizationSourceName = ShoppingV2Consts.LocalizationSourceName;
        }
    }
}
