using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabbage.QuickLogOn.Providers
{
    public class QuickLogOnRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
