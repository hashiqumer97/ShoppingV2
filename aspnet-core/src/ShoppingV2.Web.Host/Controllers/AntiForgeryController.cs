using Microsoft.AspNetCore.Antiforgery;
using ShoppingV2.Controllers;

namespace ShoppingV2.Web.Host.Controllers
{
    public class AntiForgeryController : ShoppingV2ControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
