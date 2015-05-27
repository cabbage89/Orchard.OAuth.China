using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabbage.OAuth.Models
{
    [OrchardFeature("Cabbage.OAuth.Weixin")]
    public class WinXinUserInfoPartRecord
    {
        public virtual int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual string openid { get; set; }
        public virtual string nickname { get; set; }
        public virtual string sex { get; set; }
        public virtual string province { get; set; }
        public virtual string city { get; set; }
        public virtual string country { get; set; }
        public virtual string headimgurl { get; set; }
        public virtual string privilege { get; set; }
    }
}
