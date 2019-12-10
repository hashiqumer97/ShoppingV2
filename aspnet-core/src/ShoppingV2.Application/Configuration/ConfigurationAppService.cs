using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ShoppingV2.Configuration.Dto;

namespace ShoppingV2.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ShoppingV2AppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
