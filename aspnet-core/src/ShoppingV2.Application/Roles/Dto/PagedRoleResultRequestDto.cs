using Abp.Application.Services.Dto;

namespace ShoppingV2.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

