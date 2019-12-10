using System.Threading.Tasks;
using Abp.Application.Services;
using ShoppingV2.Sessions.Dto;

namespace ShoppingV2.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
