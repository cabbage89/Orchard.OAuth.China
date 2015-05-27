using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Mvc.Extensions;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Themes;
using Orchard.UI.Notify;
using Cabbage.OAuth.Models;
using Cabbage.OAuth.Services;
using Cabbage.OAuth.ViewModels;

namespace Cabbage.OAuth.Controllers
{
    [HandleError, Themed]
    [OrchardFeature("Cabbage.OAuth.Taobao")]
    public class TaobaoOAuthController : Controller
    {
        private readonly IOrchardServices _services;
        private readonly ITaobaoOAuthService _oauthService;

        public Localizer T { get; set; }

        public TaobaoOAuthController(IOrchardServices services, ITaobaoOAuthService oauthService)
        {
            T = NullLocalizer.Instance;
            _services = services;
            _oauthService = oauthService;
        }

        public ActionResult Auth(TaobaoOAuthAuthViewModel model)
        {
            var response = _oauthService.Auth(_services.WorkContext, model.code, model.error, model.state);
            if (response.Error != null)
            {
                _services.Notifier.Add(NotifyType.Error, response.Error);
            }
            return this.RedirectLocal(response.ReturnUrl);
        }
    }
}
