using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabbage.OAuth.Models
{
    [OrchardFeature("Cabbage.OAuth")]
    public class QQUserInfoPartRecord
    {
        public virtual int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual string openid { get; set; }
        public virtual string nickname { get; set; }
        public virtual string figureurl { get; set; }
        public virtual string figureurl_1 { get; set; }
        public virtual string figureurl_2 { get; set; }
        public virtual string figureurl_qq_1 { get; set; }
        public virtual string figureurl_qq_2 { get; set; }
        public virtual string gender { get; set; }
        public virtual string is_yellow_vip { get; set; }
        public virtual string vip { get; set; }
        public virtual string yellow_vip_level { get; set; }
        public virtual string level { get; set; }
        public virtual string is_yellow_year_vip { get; set; }
    }
}
