using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabbage.OAuth.Models
{
    public class WeiXinUserInfoModel
    {
        public string openid;
        public string nickname;
        public string sex;
        public string province;
        public string city;
        public string country;
        public string headimgurl;
        public string[] privilege;
    }
}
