using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Security;
using Orchard.Users.Models;
using Cabbage.QuickLogOn.Providers;
using System.Web.Mvc;
using Cabbage.QuickLogOn.Models;

namespace Cabbage.QuickLogOn.Services
{
    public interface IQuickLogOnService : IDependency
    {
        IEnumerable<IQuickLogOnProvider> GetProviders();
        QuickLogOnResponse LogOn(QuickLogOnRequest request);

        QuickLogOnResponse LogOn(QuickLogOnRequest request, Func<UserPart> createUser);
        QuickLogOnResponse LogOn(QuickLogOnRequest request, Func<UserPart> createUser, Func<UserPart> findUser);

        QuickLogOnUserInfo GetUserInfo(IUser user);
    }

    public class QuickLogOnService : IQuickLogOnService
    {
        private readonly IOrchardServices _orchardServices;
        private readonly IMembershipService _membershipService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEnumerable<IQuickLogOnProvider> _providers = null;

        public ILogger Logger { get; set; }
        public Localizer T { get; set; }

        public QuickLogOnService(IEnumerable<IQuickLogOnProvider> providers,
                                 IMembershipService membershipService,
                                 IAuthenticationService authenticationService,
                                 IOrchardServices orchardServices)
        {
            _providers = providers;
            _membershipService = membershipService;
            _authenticationService = authenticationService;
            _orchardServices = orchardServices;

            Logger = NullLogger.Instance;
            T = NullLocalizer.Instance;
        }
        public IEnumerable<IQuickLogOnProvider> GetProviders()
        {
            return _providers;
        }
        public QuickLogOnResponse LogOn(QuickLogOnRequest request)
        {
            var userName = request.UserName;
            var lowerEmail = request.Email == null ? "" : request.Email.ToLowerInvariant();

            return LogOn(request, () =>
            {
                var user = _membershipService.CreateUser(new CreateUserParams(userName, Guid.NewGuid().ToString(), lowerEmail, null, null, true)) as UserPart;
                return user;
            });
        }

        public QuickLogOnResponse LogOn(QuickLogOnRequest request, Func<UserPart> createUser)
        {
            var userName = request.UserName;
            var lowerEmail = request.Email == null ? "" : request.Email.ToLowerInvariant();

            return LogOn(request, createUser, () =>
            {
                UserPart user = null;
                //var user = _orchardServices.ContentManager.Query<UserPart, UserPartRecord>().Where(u => u.NormalizedUserName == lowerName).List().FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    user = _orchardServices.ContentManager.Query<UserPart, UserPartRecord>().Where(u => u.UserName == userName).List().FirstOrDefault();
                }
                else if (!string.IsNullOrWhiteSpace(lowerEmail))
                {
                    user = _orchardServices.ContentManager.Query<UserPart, UserPartRecord>().Where(u => u.Email == lowerEmail).List().FirstOrDefault();
                }
                return user;
            });
        }
        public QuickLogOnResponse LogOn(QuickLogOnRequest request, Func<UserPart> createUser, Func<UserPart> findUser)
        {
            var currentUser = _authenticationService.GetAuthenticatedUser();
            if (currentUser != null) _authenticationService.SignOut();

            UserPart user = findUser();

            if (user == null)
            {
                try
                {
                    user = createUser();
                    if (user == null)
                    {
                        return new QuickLogOnResponse { User = null, Error = T("用户无法被指定的快速登录凭据创建!") };
                    }
                }
                catch (Exception ex)
                {
                    return new QuickLogOnResponse { User = null, Error = T("{0}", ex.Message) };
                }
            }

            if (user.RegistrationStatus != UserStatus.Approved)
            {
                return new QuickLogOnResponse { User = null, Error = T("用户已被管理员禁用!"), ReturnUrl = request.ReturnUrl };
            }

            _authenticationService.SignIn(user, request.RememberMe);

            return new QuickLogOnResponse { User = user, ReturnUrl = request.ReturnUrl };
        }

        public QuickLogOnUserInfo GetUserInfo(IUser user)
        {
            QuickLogOnUserInfo info = null;
            foreach (var p in _providers)
            {
                info = p.GetUserInfo(user);
                if (info != null)
                    return info;
            }
            return info;
        }
    }
}
