using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Cabbage.OAuth.ViewModels
{
    /*
     * http://developer.Taobao.com/wiki/index.php?title=docs/oauth/rest/file_data_apis_list#.E8.8E.B7.E5.8F.96.E5.BD.93.E5.89.8D.E7.99.BB.E5.BD.95.E7.94.A8.E6.88.B7.E7.9A.84.E4.BF.A1.E6.81.AF
     */
    [DataContract]
    public class TaobaoUserInfoJsonModel
    {
        [DataMember]
        public string uid { get; set; }

        [DataMember]
        public string uname { get; set; }

        [DataMember]
        public string portrait { get; set; }
    }
}
