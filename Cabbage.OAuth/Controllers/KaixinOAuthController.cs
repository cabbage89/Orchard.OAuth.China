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
    [OrchardFeature("Cabbage.OAuth.Kaixin")]
    public class KaixinOAuthController : Controller
    {
        private readonly IOrchardServices _services;
        private readonly IKaixinOAuthService _oauthService;

        public Localizer T { get; set; }

        public KaixinOAuthController(IOrchardServices services, IKaixinOAuthService oauthService)
        {
            T = NullLocalizer.Instance;
            _services = services;
            _oauthService = oauthService;
        }

        public ActionResult Auth(KaixinOAuthAuthViewModel model)
        {
            var response = _oauthService.Auth(_services.WorkContext, model.code, null, model.state);
            if (response.Error != null)
            {
                _services.Notifier.Add(NotifyType.Error, response.Error);
            }
            return this.RedirectLocal(response.ReturnUrl);
        }
    }
}
