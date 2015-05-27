using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Mvc;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Security;
using Cabbage.OAuth.Models;
using System.Net;
using Cabbage.OAuth.ViewModels;
using Cabbage.QuickLogOn.Providers;
using Cabbage.QuickLogOn.Services;
using System.Text.RegularExpressions;
using System.Web;

namespace Cabbage.OAuth.Services
{
    public interface ISinaOAuthService : IDependency
    {
        QuickLogOnResponse Auth(WorkContext wc, string code, string error, string returnUrl);
    }

    [OrchardFeature("Cabbage.OAuth.Sina")]
    public class SinaOAuthService : ISinaOAuthService
    {
        public readonly string TokenRequestUrl = "https://api.weibo.com/oauth2/access_token?client_id={0}&client_secret={1}&grant_type=authorization_code&redirect_uri={3}&code={2}";

        private readonly IQuickLogOnService _quickLogOnService;
        private readonly IEncryptionService _oauthHelper;

        public Localizer T { get; set; }
        public ILogger Logger { get; set; }

        public SinaOAuthService(IEncryptionService oauthHelper, IQuickLogOnService quickLogOnService)
        {
            _quickLogOnService = quickLogOnService;
            _oauthHelper = oauthHelper;
            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        private SinaAccessTokenJsonModel GetAccessToken(WorkContext wc, string code)
        {
            try
            {
                var part = wc.CurrentSite.As<SinaSettingsPart>();
                var clientId = part.ClientId;
                var clientSecret = _oauthHelper.Decrypt(part.Record.EncryptedClientSecret);

                var urlHelper = new UrlHelper(wc.HttpContext.Request.RequestContext);
                var redirectUrl =
                    new Uri(wc.HttpContext.Request.Url,
                            urlHelper.Action("Auth", "SinaOAuth", new { Area = "Cabbage.OAuth" })).ToString();

                var encodeUrl = urlHelper.Encode(redirectUrl);

                var wr = WebRequest.Create(string.Format(TokenRequestUrl, clientId, clientSecret, code, encodeUrl));
                wr.Proxy = OAuthHelper.GetProxy();
                wr.ContentType = "application/x-www-form-urlencoded";
                wr.Method = "POST";
                using (var stream = wr.GetRequestStream())
                using (var ws = new StreamWriter(stream, Encoding.UTF8))
                {
                    //此段参数为保险起见,加入的,实际上只需要URL拼接即可
                    ws.Write("client_id={0}&", clientId);
                    ws.Write("client_secret={0}&", clientSecret);
                    ws.Write("grant_type=authorization_code&");
                    ws.Write("code={0}&", code);
                    ws.Write("redirect_uri={0}", encodeUrl);

                }
                var wres = wr.GetResponse();
                using (var stream = wres.GetResponseStream())
                {
                    return OAuthHelper.FromJson<SinaAccessTokenJsonModel>(stream);
                }
            }
            catch (WebException ex)
            {
                var webResponse = ex.Response as HttpWebResponse;
                using (var stream = webResponse.GetResponseStream())
                using (var sr = new StreamReader(stream))
                {
                    var error = sr.ReadToEnd();
                    Logger.Error(ex, error);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
            }

            return null;
        }

        public QuickLogOnResponse Auth(WorkContext wc, string code, string error, string returnUrl)
        {
            if (string.IsNullOrEmpty(code))
            {
                error = "无效的code";
            }
            else
            {
                var token = GetAccessToken(wc, code);
                if (!string.IsNullOrEmpty(token.access_token))
                {
                    var uid = token.uid;
                    if (!string.IsNullOrEmpty(uid))
                    {
                        return _quickLogOnService.LogOn(new QuickLogOnRequest
                        {
                            UserName = uid,
                            RememberMe = false,
                            ReturnUrl = returnUrl
                        });
                    }
                    error = "无效的uid";
                }
                else
                {
                    error = "无效的访问令牌";
                }
            }
            return new QuickLogOnResponse { Error = T("新浪登录失败: {0}", error), ReturnUrl = returnUrl };
        }
    }
}
