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
using Orchard.Users.Models;
using Orchard.Data;

namespace Cabbage.OAuth.Services
{
    public interface IQQOAuthService : IDependency
    {
        QuickLogOnResponse Auth(WorkContext wc, string code, string error, string returnUrl);
    }

    [OrchardFeature("Cabbage.OAuth")]
    public class QQOAuthService : IQQOAuthService
    {
        public readonly string TokenRequestUrl = "https://graph.qq.com/oauth2.0/token?grant_type=authorization_code&client_id={0}&client_secret={1}&code={2}&redirect_uri={3}";
        public readonly string OpenIdRequestUrl = "https://graph.qq.com/oauth2.0/me?access_token={0}";
        public readonly string GetUserInfoUrl = "https://graph.qq.com/user/get_user_info?access_token={0}&oauth_consumer_key={1}&openid={2}&format=json";
        public readonly Regex OpenIdPattern = new Regex("([0-9A-F]{32})", RegexOptions.Compiled);

        private readonly IQuickLogOnService _quickLogOnService;
        private readonly IEncryptionService _oauthHelper;
        private readonly IMembershipService _membershipService;
        private readonly IRepository<QQUserInfoPartRecord> _QQRepository;

        string clientId, clientSecret;

        public Localizer T { get; set; }
        public ILogger Logger { get; set; }

        public QQOAuthService(IEncryptionService oauthHelper
            , IQuickLogOnService quickLogOnService
            , IMembershipService membershipService
            , IRepository<QQUserInfoPartRecord> QQRepository
            )
        {
            _quickLogOnService = quickLogOnService;
            _oauthHelper = oauthHelper;
            _QQRepository = QQRepository;
            _membershipService = membershipService;

            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        private string GetAccessToken(WorkContext wc, string code)
        {
            try
            {
                var part = wc.CurrentSite.As<QQSettingsPart>();
                clientId = part.ClientId;
                clientSecret = _oauthHelper.Decrypt(part.Record.EncryptedClientSecret);

                var urlHelper = new UrlHelper(wc.HttpContext.Request.RequestContext);
                var redirectUrl =
                    new Uri(wc.HttpContext.Request.Url,
                            urlHelper.Action("Auth", "QQOAuth", new { Area = "Cabbage.OAuth" })).ToString();

                var wr = WebRequest.Create(string.Format(TokenRequestUrl, clientId, clientSecret, code, urlHelper.Encode(redirectUrl)));
                wr.Proxy = OAuthHelper.GetProxy();
                wr.ContentType = "application/x-www-form-urlencoded";
                wr.Method = "GET";
                var wres = wr.GetResponse();
                using (var stream = wres.GetResponseStream())
                using (var sr = new StreamReader(stream))
                {
                    var result = HttpUtility.ParseQueryString(sr.ReadToEnd());
                    return result["access_token"];
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

        private string GetOpenId(string token)
        {
            try
            {
                var wr = WebRequest.Create(string.Format(OpenIdRequestUrl, token));
                wr.Method = "GET";
                wr.Proxy = OAuthHelper.GetProxy();
                var wres = wr.GetResponse();
                using (var stream = wres.GetResponseStream())
                using (StreamReader sr = new StreamReader(stream))
                {
                    var result = sr.ReadToEnd();
                    return OpenIdPattern.Match(result).Value;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
            }
            return null;
        }

        public QQUserInfoModel GetUserInfo(string access_token, string openid)
        {
            var wr = WebRequest.Create(string.Format(GetUserInfoUrl, access_token, clientId, openid));
            wr.Method = "GET";
            wr.Proxy = OAuthHelper.GetProxy();
            var wres = wr.GetResponse();
            using (var stream = wres.GetResponseStream())
            {
                return OAuthHelper.FromJson<QQUserInfoModel>(stream);
            }
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
                if (!string.IsNullOrEmpty(token))
                {
                    var openId = GetOpenId(token);
                    if (!string.IsNullOrEmpty(openId))
                    {
                        var request = new QuickLogOnRequest
                        {
                            UserName = openId,
                            RememberMe = false,
                            ReturnUrl = returnUrl
                        };
                        var lowerEmail = request.Email == null ? "" : request.Email.ToLowerInvariant();
                        return _quickLogOnService.LogOn(request, () =>
                        {
                            var model = GetUserInfo(token, openId);

                            if (model.ret < 0)
                                throw new Exception(string.Format("[{0}]{1}", model.ret, model.msg));

                            UserPart user = _membershipService.CreateUser(new CreateUserParams(request.UserName, Guid.NewGuid().ToString(), lowerEmail, null, null, true)) as UserPart;

                            var record = new QQUserInfoPartRecord
                            {
                                UserId = user.Id,
                                nickname = model.nickname,
                                figureurl = model.figureurl,
                                figureurl_1 = model.figureurl_1,
                                figureurl_2 = model.figureurl_2,
                                figureurl_qq_1 = model.figureurl_qq_1,
                                figureurl_qq_2 = model.figureurl_qq_2,
                                gender = model.gender,
                                is_yellow_vip = model.is_yellow_vip,
                                vip = model.vip,
                                yellow_vip_level = model.yellow_vip_level,
                                level = model.level,
                                is_yellow_year_vip = model.is_yellow_year_vip,
                            };
                            _QQRepository.Create(record);

                            return user;
                        });
                    }
                    error = "无效的OpenID";
                }
                else
                {
                    error = "无效的访问令牌";
                }
            }
            return new QuickLogOnResponse { Error = T("QQ登录失败: {0}", error), ReturnUrl = returnUrl };
        }
    }
}
