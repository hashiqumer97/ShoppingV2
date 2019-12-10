using System.Collections.Generic;
using ShoppingV2.Roles.Dto;

namespace ShoppingV2.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}