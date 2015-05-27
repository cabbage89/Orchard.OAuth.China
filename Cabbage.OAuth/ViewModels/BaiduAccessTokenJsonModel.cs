using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Cabbage.OAuth.ViewModels
{
    [DataContract]
    public class BaiduAccessTokenJsonModel
    {
        [DataMember]
        public string access_token { get; set; }

        [DataMember]
        public string expires_in { get; set; }

        [DataMember]
        public string refresh_token { get; set; }

        [DataMember]
        public string scope { get; set; }

        [DataMember]
        public string session_key { get; set; }

        [DataMember]
        public string session_secret { get; set; }

        [DataMember]
        public string error { get; set; }

        [DataMember]
        public string error_description { get; set; }
    }
}
