using System.Threading.Tasks;
using ShoppingV2.Configuration.Dto;

namespace ShoppingV2.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
