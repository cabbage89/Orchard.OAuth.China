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
    [OrchardFeature("Cabbage.OAuth")]
    public class QQUserInfoPart : ContentPart
    {
        public LazyField<QQUserInfoPartRecord> Record = new LazyField<QQUserInfoPartRecord>();
        public string nickname { get { return Record.Value.nickname; } }
        public string openid { get { return Record.Value.openid; } }
        public string figureurl { get { return Record.Value.figureurl; } }
        public string figureurl_1 { get { return Record.Value.figureurl_1; } }
        public string figureurl_2 { get { return Record.Value.figureurl_2; } }
        public string figureurl_qq_1 { get { return Record.Value.figureurl_qq_1; } }
        public string figureurl_qq_2 { get { return Record.Value.figureurl_qq_2; } }
        public string gender { get { return Record.Value.gender; } }
        public string is_yellow_vip { get { return Record.Value.is_yellow_vip; } }
        public string vip { get { return Record.Value.vip; } }
        public string yellow_vip_level { get { return Record.Value.yellow_vip_level; } }
        public string level { get { return Record.Value.level; } }
        public string is_yellow_year_vip { get { return Record.Value.is_yellow_year_vip; } }
    }
}
