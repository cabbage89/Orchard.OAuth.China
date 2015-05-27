using Cabbage.QuickLogOn.Models;
using Orchard;
using Orchard.Security;

namespace Cabbage.QuickLogOn.Providers
{
    public interface IQuickLogOnProvider : IDependency
    {
        string Name { get; }
        string Description { get; }

        string GetLogOnUrl(WorkContext context);

        QuickLogOnUserInfo GetUserInfo(IUser user);
    }
}
