using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Security;
using Cabbage.OAuth.Models;
using Cabbage.OAuth.Services;

namespace Cabbage.OAuth.Drivers
{
    [OrchardFeature("Cabbage.OAuth.Baidu")]
    public class BaiduSettingsPartDriver : ContentPartDriver<BaiduSettingsPart>
    {
        private readonly IEncryptionService _service;

        public Localizer T { get; set; }

        public BaiduSettingsPartDriver(IEncryptionService service)
        {
            _service = service;
            T = NullLocalizer.Instance;
        }

        protected override string Prefix { get { return "BaiduSettings"; } }

        protected override DriverResult Editor(BaiduSettingsPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Baidu_SiteSettings",
                               () => shapeHelper.EditorTemplate(TemplateName: "Parts.Baidu.SiteSettings", Model: part, Prefix: Prefix)).OnGroup("QuickLogOn");
        }

        protected override DriverResult Editor(BaiduSettingsPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            if (updater.TryUpdateModel(part, Prefix, null, null))
            {
                if (!string.IsNullOrWhiteSpace(part.ClientSecret))
                {
                    part.Record.EncryptedClientSecret = _service.Encrypt(part.ClientSecret);
                }
                if (!string.IsNullOrWhiteSpace(part.Additional))
                {
                    part.Record.Additional = part.Additional;
                }
            }
            return Editor(part, shapeHelper);
        }
    }
}
