using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Cabbage.OAuth.ViewModels
{
    /*
     * http://wiki.open.kaixin001.com/index.php?id=%E8%8E%B7%E5%8F%96%E5%BD%93%E5%89%8D%E7%99%BB%E5%BD%95%E7%94%A8%E6%88%B7%E7%9A%84%E8%B5%84%E6%96%99
     */
    [DataContract]
    public class KaixinUserInfoJsonModel
    {
        [DataMember]
        public string uid { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string gender { get; set; }

        [DataMember]
        public string hometown { get; set; }

        [DataMember]
        public string city { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public string logo120 { get; set; }

        [DataMember]
        public string logo50 { get; set; }

        [DataMember]
        public string birthday { get; set; }

        [DataMember]
        public string bodyform { get; set; }

        [DataMember]
        public string blood { get; set; }

        [DataMember]
        public string marriage { get; set; }

        [DataMember]
        public string trainwith { get; set; }

        [DataMember]
        public string interest { get; set; }

        [DataMember]
        public string favbook { get; set; }

        [DataMember]
        public string favmovie { get; set; }

        [DataMember]
        public string favtv { get; set; }

        [DataMember]
        public string idol { get; set; }

        [DataMember]
        public string motto { get; set; }

        [DataMember]
        public string wishlist { get; set; }

        [DataMember]
        public string intro { get; set; }

        [DataMember]
        public List<Education> education { get; set; }

        [DataMember]
        public List<Career> career { get; set; }

        [DataMember]
        public Pinyin pinyin { get; set; }

        [DataMember]
        public string isStar { get; set; }
    }
    [DataContract]
    public class Pinyin
    {

        [DataMember]
        public string firststr { get; set; }

        [DataMember]
        public string pinyinstr { get; set; }
    }

    [DataContract]
    public class Education
    {

        [DataMember]
        public string schooltype { get; set; }

        [DataMember]
        public string school { get; set; }

        [DataMember]
        public string @class { get; set; }

        [DataMember]
        public string year { get; set; }
    }

    [DataContract]
    public class Career
    {

        [DataMember]
        public string company { get; set; }

        [DataMember]
        public string dept { get; set; }

        [DataMember]
        public string beginyear { get; set; }

        [DataMember]
        public string beginmonth { get; set; }

        [DataMember]
        public string endyear { get; set; }

        [DataMember]
        public string endmonth { get; set; }
    }
}
