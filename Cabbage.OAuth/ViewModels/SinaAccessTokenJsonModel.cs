using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Cabbage.OAuth.ViewModels
{
    [DataContract]
    public class SinaAccessTokenJsonModel
    {
        [DataMember]
        public string access_token { get; set; }

        [DataMember]
        public string expires_in { get; set; }

        [DataMember]
        public string uid { get; set; }

        [DataMember]
        public string error { get; set; }

        [DataMember]
        public string error_code { get; set; }

        [DataMember]
        public string error_description { get; set; }
    }
}
