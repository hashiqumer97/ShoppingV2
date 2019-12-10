using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ShoppingV2.MultiTenancy.Dto;

namespace ShoppingV2.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

