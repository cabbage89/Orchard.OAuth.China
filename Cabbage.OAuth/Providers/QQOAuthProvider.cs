using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Cabbage.OAuth.Models;
using Cabbage.QuickLogOn.Providers;
using System.Web.Mvc;
using Orchard.Data;
using Cabbage.QuickLogOn.Models;

namespace Cabbage.OAuth.Providers
{
    [OrchardFeature("Cabbage.OAuth")]
    public class QQOAuthProvider : IQuickLogOnProvider
    {
        public const string Url = "https://graph.qq.com/oauth2.0/authorize?response_type=code&client_id={0}&redirect_uri={1}&state={2}{3}";

        public string Name
        {
            get { return "QQ"; }
        }

        public string Description
        {
            get { return "QQ快速登录"; }
        }
        private readonly IRepository<QQUserInfoPartRecord> _repository;
        public QQOAuthProvider(IRepository<QQUserInfoPartRecord> repository)
        {
            _repository = repository;
        }

        public string GetLogOnUrl(WorkContext context)
        {
            var urlHelper = new UrlHelper(context.HttpContext.Request.RequestContext);
            var part = context.CurrentSite.As<QQSettingsPart>();
            var clientId = part.ClientId;
            var additional = part.Additional;
            //用于第三方应用防止CSRF攻击，成功授权后回调时会原样带回。请务必严格按照流程检查用户与state参数状态的绑定.
            var returnUrl = context.HttpContext.Request.Url;
            var state = urlHelper.Encode(returnUrl.ToString());
            var redirectUrl = new Uri(returnUrl, urlHelper.Action("Auth", "QQOAuth", new { Area = "Cabbage.OAuth" })).ToString();
            return string.Format(Url, clientId, urlHelper.Encode(redirectUrl), state, (string.IsNullOrWhiteSpace(additional) ? "" : ("&" + additional)));
        }


        public QuickLogOn.Models.QuickLogOnUserInfo GetUserInfo(Orchard.Security.IUser user)
        {
            var part = user.As<QQUserInfoPart>();
            if (part != null)
            {
                part.Record.Loader(
                 () => _repository
                 .Fetch(x => x.UserId == user.Id).FirstOrDefault());
                var record = part.Record.Value;
                if (record != null)
                {
                    var model = new QuickLogOnUserInfo
                    {
                        UniqueId = record.openid,
                        NickName = record.nickname,
                        HeadimgUrl = record.figureurl_qq_1,
                        Sex = record.gender,
                        Original = record
                    };
                    return model;
                }
            }
            return null;
        }
    }
}
