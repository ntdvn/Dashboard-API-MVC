using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DashboardMVC.Controllers.API
{
    public class AuthenticationController : BaseApiController
    {
        private readonly IStringLocalizer<AuthenticationController> _localizer;
        public AuthenticationController(IStringLocalizer<AuthenticationController> localizer)
        {
            this._localizer = localizer;
        }

        [HttpGet("register")]
        public ActionResult register()
        {
            return Ok(_localizer["chat"].Value);
        }
    }
}