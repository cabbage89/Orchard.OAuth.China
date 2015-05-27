using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.Localization;
using Orchard.Security;

namespace Cabbage.QuickLogOn.Providers
{
    public class QuickLogOnResponse
    {
        public IUser User { get; set; }
        public LocalizedString Error { get; set; }
        public string ReturnUrl { get; set; }
    }
}
