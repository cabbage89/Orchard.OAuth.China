using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Cabbage.OAuth.ViewModels
{
    [DataContract]
    public class DoubanAccessTokenJsonModel
    {
        [DataMember]
        public string access_token { get; set; }

        [DataMember]
        public string expires_in { get; set; }

        [DataMember]
        public string refresh_token { get; set; }

        [DataMember]
        public string douban_user_id { get; set; }

        //[DataMember]
        //public string code { get; set; }

        //[DataMember]
        //public string msg { get; set; }

        //[DataMember]
        //public string request { get; set; }
    }
}
