using Cabbage.OAuth.Models;
using Orchard.ContentManagement.Drivers;
using Orchard.Data;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabbage.OAuth.Drivers
{
    [OrchardFeature("Cabbage.OAuth.Weixin")]
    public class WinXinUserInfoPartDriver : ContentPartDriver<WinXinUserInfoPart>
    {
        private readonly IRepository<WinXinUserInfoPartRecord> _winXinUserInfoPartRecord;
        public WinXinUserInfoPartDriver(IRepository<WinXinUserInfoPartRecord> winXinUserInfoPartRecord) 
        {
            _winXinUserInfoPartRecord = winXinUserInfoPartRecord;
        }
        protected override void Importing(WinXinUserInfoPart part, Orchard.ContentManagement.Handlers.ImportContentContext context)
        {
            var openid = context.Attribute(part.PartDefinition.Name, "openid");
            var nickname = context.Attribute(part.PartDefinition.Name, "nickname");
            var sex = context.Attribute(part.PartDefinition.Name, "sex");
            var province = context.Attribute(part.PartDefinition.Name, "province");
            var city = context.Attribute(part.PartDefinition.Name, "city");
            var country = context.Attribute(part.PartDefinition.Name, "country");
            var headimgurl = context.Attribute(part.PartDefinition.Name, "headimgurl");
            var privilege = context.Attribute(part.PartDefinition.Name, "privilege");

            var record = new WinXinUserInfoPartRecord
            {
                openid = openid,
                nickname = nickname,
                sex = sex,
                province = province,
                city = city,
                country = country,
                headimgurl = headimgurl,
                privilege = string.Join(",", privilege),
            };
            _winXinUserInfoPartRecord.Create(record);
        }

        protected override void Exporting(WinXinUserInfoPart part, Orchard.ContentManagement.Handlers.ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("openid", part.openid);
            context.Element(part.PartDefinition.Name).SetAttributeValue("nickname", part.nickname);
            context.Element(part.PartDefinition.Name).SetAttributeValue("sex", part.sex);
            context.Element(part.PartDefinition.Name).SetAttributeValue("province", part.province);
            context.Element(part.PartDefinition.Name).SetAttributeValue("city", part.city);
            context.Element(part.PartDefinition.Name).SetAttributeValue("country", part.country);
            context.Element(part.PartDefinition.Name).SetAttributeValue("headimgurl", part.headimgurl);
            context.Element(part.PartDefinition.Name).SetAttributeValue("privilege", part.privilege);
        }
    }
}
