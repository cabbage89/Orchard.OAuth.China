using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Cabbage.QuickLogOn
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            ContentDefinitionManager.AlterPartDefinition(
                "QuickLogOnWidgetPart",
                builder => builder.Attachable());

            ContentDefinitionManager.AlterTypeDefinition(
                "QuickLogOnWidget",
                cfg => cfg
                           .WithPart("QuickLogOnWidgetPart")
                           .WithPart("CommonPart")
                           .WithPart("WidgetPart")
                           .WithSetting("Stereotype", "Widget")
                );
            return 1;
        }

    }
}
