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
    [OrchardFeature("Cabbage.OAuth.Renren")]
    public class RenrenSettingsPartDriver : ContentPartDriver<RenrenSettingsPart>
    {
        private readonly IEncryptionService _service;

        public Localizer T { get; set; }

        public RenrenSettingsPartDriver(IEncryptionService service)
        {
            _service = service;
            T = NullLocalizer.Instance;
        }

        protected override string Prefix { get { return "RenrenSettings"; } }

        protected override DriverResult Editor(RenrenSettingsPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Renren_SiteSettings",
                               () => shapeHelper.EditorTemplate(TemplateName: "Parts.Renren.SiteSettings", Model: part, Prefix: Prefix)).OnGroup("QuickLogOn");
        }

        protected override DriverResult Editor(RenrenSettingsPart part, IUpdateModel updater, dynamic shapeHelper)
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
