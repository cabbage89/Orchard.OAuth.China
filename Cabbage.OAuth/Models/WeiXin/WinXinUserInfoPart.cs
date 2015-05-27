using Cabbage.OAuth.Models;
using Orchard.ContentManagement;
using Orchard.Core.Common.Utilities;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabbage.OAuth.Models
{
    [OrchardFeature("Cabbage.OAuth.Weixin")]
    public class WinXinUserInfoPart : ContentPart
    {
        public LazyField<WinXinUserInfoPartRecord> Record = new LazyField<WinXinUserInfoPartRecord>();
        public string openid
        {
            get { return Record.Value.openid; }
        }
        public string nickname
        {
            get { return Record.Value.nickname; }
        }
        public string sex
        {
            get { return Record.Value.sex; }
        }
        public string province
        {
            get { return Record.Value.province; }
        }
        public string city
        {
            get { return Record.Value.city; }
        }
        public string country
        {
            get { return Record.Value.country; }
        }
        public string headimgurl
        {
            get { return Record.Value.headimgurl; }
        }

        public string privilege
        {
            get { return Record.Value.privilege; }
        }
    }
}
