using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabbage.OAuth.ViewModels
{
    public class BaiduOAuthAuthViewModel
    {
        public string code { get; set; }
        public string state { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
    }
}
