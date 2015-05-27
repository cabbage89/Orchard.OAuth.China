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
    [OrchardFeature("Cabbage.OAuth.Kaixin")]
    public class KaixinSettingsPartDriver : ContentPartDriver<KaixinSettingsPart>
    {
        private readonly IEncryptionService _service;

        public Localizer T { get; set; }

        public KaixinSettingsPartDriver(IEncryptionService service)
        {
            _service = service;
            T = NullLocalizer.Instance;
        }

        protected override string Prefix { get { return "KaixinSettings"; } }

        protected override DriverResult Editor(KaixinSettingsPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Kaixin_SiteSettings",
                               () => shapeHelper.EditorTemplate(TemplateName: "Parts.Kaixin.SiteSettings", Model: part, Prefix: Prefix)).OnGroup("QuickLogOn");
        }

        protected override DriverResult Editor(KaixinSettingsPart part, IUpdateModel updater, dynamic shapeHelper)
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
