using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using ShoppingV2.Controllers;

namespace ShoppingV2.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ShoppingV2ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
