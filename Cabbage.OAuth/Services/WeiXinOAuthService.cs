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
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Cabbage.OAuth.Services
{
    public interface IWeiXinOAuthService : IDependency
    {
        QuickLogOnResponse Auth(WorkContext wc, string code, string returnUrl);

        WeiXinUserInfoModel GetUserInfo(string access_token, string openid);
    }

    [OrchardFeature("Cabbage.OAuth.Weixin")]
    public class WeiXinOAuthService : IWeiXinOAuthService
    {
        public readonly string TokenRequestUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
        public readonly string GetUserInfoUrl = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN";
        public readonly Regex OpenIdPattern = new Regex("([0-9A-F]{32})", RegexOptions.Compiled);

        private readonly IQuickLogOnService _quickLogOnService;
        private readonly IEncryptionService _oauthHelper;
        private readonly IMembershipService _membershipService;
        private readonly IOrchardServices _orchardServices;
        private readonly IRepository<WinXinUserInfoPartRecord> _winXinUserInfoPartRecordRepository;

        public Localizer T { get; set; }
        public ILogger Logger { get; set; }

        public WeiXinOAuthService(IEncryptionService oauthHelper, IQuickLogOnService quickLogOnService
            , IMembershipService membershipService
            , IOrchardServices orchardServices
            , IRepository<WinXinUserInfoPartRecord> winXinUserInfoPartRecordRepository
            )
        {
            _quickLogOnService = quickLogOnService;
            _oauthHelper = oauthHelper;
            _membershipService = membershipService;
            _orchardServices = orchardServices;
            _winXinUserInfoPartRecordRepository = winXinUserInfoPartRecordRepository;

            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        private WeiXinAccessTokenJsonModel GetAccessToken(WorkContext wc, string code)
        {
            try
            {
                var part = wc.CurrentSite.As<WeiXinSettingsPart>();
                var clientId = part.ClientId;
                var clientSecret = _oauthHelper.Decrypt(part.Record.EncryptedClientSecret);

                var urlHelper = new UrlHelper(wc.HttpContext.Request.RequestContext);
                var redirectUrl =
                    new Uri(wc.HttpContext.Request.Url,
                            urlHelper.Action("Auth", "WeiXinOAuth", new { Area = "Cabbage.OAuth" })).ToString();

                var wr = WebRequest.Create(string.Format(TokenRequestUrl, clientId, clientSecret, code));
                wr.Proxy = OAuthHelper.GetProxy();
                wr.ContentType = "application/x-www-form-urlencoded";
                wr.Method = "GET";
                var wres = wr.GetResponse();
                using (var stream = wres.GetResponseStream())
                {
                    return OAuthHelper.FromJson<WeiXinAccessTokenJsonModel>(stream);
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
        public QuickLogOnResponse Auth(WorkContext wc, string code, string returnUrl)
        {
            string error = null;
            if (string.IsNullOrEmpty(code))
            {
                error = "无效的code";
            }
            else
            {
                var token = GetAccessToken(wc, code);
                if (!string.IsNullOrEmpty(token.access_token))
                {
                    var request = new QuickLogOnRequest
                    {
                        UserName = token.openid,
                        RememberMe = false,
                        ReturnUrl = returnUrl
                    };
                    var lowerEmail = request.Email == null ? "" : request.Email.ToLowerInvariant();
                    return _quickLogOnService.LogOn(request, () =>
                    {
                        UserPart user = _membershipService.CreateUser(new CreateUserParams(request.UserName, Guid.NewGuid().ToString(), lowerEmail, null, null, true)) as UserPart;

                        var model = GetUserInfo(token.access_token, token.openid);

                        var record = new WinXinUserInfoPartRecord
                        {
                            UserId = user.Id,
                            openid = model.openid,
                            nickname = model.nickname,
                            sex = model.sex,
                            province = model.province,
                            city = model.city,
                            country = model.country,
                            headimgurl = model.headimgurl,
                            privilege = string.Join(",", model.privilege),

                        };
                        _winXinUserInfoPartRecordRepository.Create(record);

                        return user;
                    });
                }
                else
                {
                    error = string.Format("[{0}]{1}", token.errcode, token.errmsg);
                }
            }
            return new QuickLogOnResponse { Error = T("微信登录失败: {0}", error), ReturnUrl = returnUrl };
        }
        public WeiXinUserInfoModel GetUserInfo(string access_token, string openid)
        {
            var wr = WebRequest.Create(string.Format(GetUserInfoUrl, access_token, openid));
            wr.Proxy = OAuthHelper.GetProxy();
            wr.ContentType = "application/x-www-form-urlencoded";
            wr.Method = "GET";
            var wres = wr.GetResponse();
            using (var stream = wres.GetResponseStream())
            {
                return OAuthHelper.FromJson<WeiXinUserInfoModel>(stream);
            }
        }
    }
}
