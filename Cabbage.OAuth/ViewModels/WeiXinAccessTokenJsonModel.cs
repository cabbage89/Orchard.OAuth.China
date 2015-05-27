using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Cabbage.OAuth.ViewModels
{
    /*
     * http://wiki.open.kaixin001.com/index.php?id=%E4%BD%BF%E7%94%A8Authorization_Code%E8%8E%B7%E5%8F%96Access_Token
     */
    [DataContract]
    public class WeiXinAccessTokenJsonModel
    {
        [DataMember]
        public string access_token { get; set; }

        [DataMember]
        public string expires_in { get; set; }

        [DataMember]
        public string refresh_token { get; set; }

        [DataMember]
        public string openid { get; set; }

        [DataMember]
        public string scope { get; set; }

        [DataMember]
        public string errcode { get; set; }

        [DataMember]
        public string errmsg { get; set; }
    }
}
