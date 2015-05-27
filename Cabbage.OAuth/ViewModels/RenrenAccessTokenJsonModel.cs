using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Cabbage.OAuth.ViewModels
{
    [DataContract]
    public class RenrenAccessTokenJsonModel
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
        public string error { get; set; }

        [DataMember]
        public string error_description { get; set; }

        [DataMember]
        public string error_uri { get; set; }
    }
}
