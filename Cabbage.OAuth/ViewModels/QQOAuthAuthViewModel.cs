using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabbage.OAuth.ViewModels
{
    public class QQOAuthAuthViewModel
    {
        public string code { get; set; }
        public string msg { get; set; }
        public string state { get; set; }
        public string usercancel { get; set; }
    }
}
