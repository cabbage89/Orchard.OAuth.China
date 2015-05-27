using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabbage.OAuth.ViewModels
{
    /*
     * http://wiki.open.kaixin001.com/index.php?id=%E4%BD%BF%E7%94%A8Authorization_Code%E8%8E%B7%E5%8F%96Access_Token
     */
    public class KaixinOAuthAuthViewModel
    {
        public string code { get; set; }
        public string state { get; set; }
        public string error_code { get; set; }
        public string request { get; set; }
        public string error { get; set; }
    }
}
