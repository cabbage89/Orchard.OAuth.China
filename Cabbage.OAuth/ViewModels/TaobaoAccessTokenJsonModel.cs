using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Cabbage.OAuth.ViewModels
{
    [DataContract]
    public class TaobaoAccessTokenJsonModel
    {
        [DataMember]
        public string access_token { get; set; }

        [DataMember]
        public string token_type { get; set; }

        [DataMember]
        public string expires_in { get; set; }

        [DataMember]
        public string refresh_token { get; set; }

        [DataMember]
        public string re_expires_in { get; set; }

        [DataMember]
        public string r1_expires_in { get; set; }

        [DataMember]
        public string r2_expires_in { get; set; }

        [DataMember]
        public string w1_expires_in { get; set; }

        [DataMember]
        public string w2_expires_in { get; set; }

        [DataMember]
        public string taobao_user_nick { get; set; }

        [DataMember]
        public string taobao_user_id { get; set; }

        [DataMember]
        public string sub_taobao_user_id { get; set; }

        [DataMember]
        public string sub_taobao_user_nick { get; set; }
    }
}
