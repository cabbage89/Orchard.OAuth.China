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
    public interface IBaiduOAuthService : IDependency
    {
        QuickLogOnResponse Auth(WorkContext wc, string code, string error, string returnUrl);
    }

    [OrchardFeature("Cabbage.OAuth.Baidu")]
    public class BaiduOAuthService : IBaiduOAuthService
    {
        public readonly string TokenRequestUrl = "https://openapi.baidu.com/oauth/2.0/token?client_id={0}&client_secret={1}&code={2}&redirect_uri={3}&grant_type=authorization_code";
        public readonly string UserInfoUrl = "https://openapi.baidu.com/rest/2.0/passport/users/getLoggedInUser?access_token={0}";

        private readonly IQuickLogOnService _quickLogOnService;
        private readonly IEncryptionService _oauthHelper;

        public Localizer T { get; set; }
        public ILogger Logger { get; set; }

        public BaiduOAuthService(IEncryptionService oauthHelper, IQuickLogOnService quickLogOnService)
        {
            _quickLogOnService = quickLogOnService;
            _oauthHelper = oauthHelper;
            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        private BaiduAccessTokenJsonModel GetAccessToken(WorkContext wc, string code)
        {
            try
            {
                var part = wc.CurrentSite.As<BaiduSettingsPart>();
                var clientId = part.ClientId;
                var clientSecret = _oauthHelper.Decrypt(part.Record.EncryptedClientSecret);

                var urlHelper = new UrlHelper(wc.HttpContext.Request.RequestContext);
                var redirectUrl =
                    new Uri(wc.HttpContext.Request.Url,
                            urlHelper.Action("Auth", "BaiduOAuth", new { Area = "Cabbage.OAuth" })).ToString();

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
                    return OAuthHelper.FromJson<BaiduAccessTokenJsonModel>(stream);
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

        private BaiduUserInfoJsonModel GetUserInfo(string token)
        {
            try
            {
                var wr = WebRequest.Create(string.Format(UserInfoUrl, token));
                wr.Proxy = OAuthHelper.GetProxy();
                wr.ContentType = "application/x-www-form-urlencoded";
                wr.Method = "POST";
                using (var stream = wr.GetRequestStream())
                using (var ws = new StreamWriter(stream, Encoding.UTF8))
                {
                    //此段参数为保险起见,加入的,实际上只需要URL拼接即可
                    ws.Write("access_token={0}", token);
                }
                var wres = wr.GetResponse();
                using (var stream = wres.GetResponseStream())
                using (var sr = new StreamReader(stream))
                {
                    //var json = sr.ReadToEnd();
                    var result = OAuthHelper.FromJson<BaiduUserInfoJsonModel>(stream);
                    return result;
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
                    var info = GetUserInfo(token.access_token);
                    if (!string.IsNullOrEmpty(info.uid))
                    {
                        return _quickLogOnService.LogOn(new QuickLogOnRequest
                        {
                            UserName = info.uid,
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
            return new QuickLogOnResponse { Error = T("百度登录失败: {0}", error), ReturnUrl = returnUrl };
        }
    }
}
