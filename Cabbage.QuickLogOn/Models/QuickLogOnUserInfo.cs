using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabbage.QuickLogOn.Models
{
    public class QuickLogOnUserInfo
    {
        public virtual string UniqueId { get; set; }
        public virtual string NickName { get; set; }
        public virtual string Sex { get; set; }
        public virtual string Province { get; set; }
        public virtual string City { get; set; }
        public virtual string Country { get; set; }
        public virtual string HeadimgUrl { get; set; }
        /// <summary>
        /// 第三方提供的原始用户信息
        /// </summary>
        public virtual dynamic Original { get; set; }
    }
}
