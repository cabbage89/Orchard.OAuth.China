using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Cabbage.OAuth.ViewModels
{
    /*
     * http://wiki.dev.renren.com/wiki/Users.getInfo
     */
    [DataContract]
    public class RenrenUserInfoJsonModel
    {
        [DataMember]
        public string uid { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string sex { get; set; }

        [DataMember]
        public string star { get; set; }

        [DataMember]
        public string zidou { get; set; }

        [DataMember]
        public string vip { get; set; }

        [DataMember]
        public string birthday { get; set; }

        [DataMember]
        public string email_hash { get; set; }

        [DataMember]
        public string tinyurl { get; set; }

        [DataMember]
        public string headurl { get; set; }

        [DataMember]
        public string mainurl { get; set; }

        [DataMember]
        public hometown_location hometown_location { get; set; }

        [DataMember]
        public work_info work_info { get; set; }

        [DataMember]
        public university_info university_info { get; set; }

        [DataMember]
        public hs_info hs_info { get; set; }
    }

    [DataContract]
    public class hometown_location
    {

        [DataMember]
        public string country { get; set; }

        [DataMember]
        public string province { get; set; }

        [DataMember]
        public string city { get; set; }
    }

    [DataContract]
    public class work_info
    {

        [DataMember]
        public string company_name { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public string start_date { get; set; }

        [DataMember]
        public string end_date { get; set; }
    }

    [DataContract]
    public class university_info
    {

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string year { get; set; }

        [DataMember]
        public string department { get; set; }
    }


    [DataContract]
    public class hs_info
    {

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string grad_year { get; set; }
    }
}
