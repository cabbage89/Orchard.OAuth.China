using Cabbage.OAuth.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;
using System;
using Orchard.ContentManagement;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabbage.OAuth.Handlers
{
    [OrchardFeature("Cabbage.OAuth")]
    public class QQUserInfoPartHandle : ContentHandler
    {
        public QQUserInfoPartHandle(IRepository<QQUserInfoPartRecord> repository)
        {
            Filters.Add(new ActivatingFilter<QQUserInfoPart>("User"));
            OnInitialized<QQUserInfoPart>((context, part) => part.Record.Loader(() =>
                repository.Fetch(x => x.UserId == context.ContentItem.Id).FirstOrDefault()));
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            var part = context.ContentItem.As<QQUserInfoPart>();

            if (part != null)
            {
                context.Metadata.DisplayText = part.nickname;
            }
        }
    }
}
