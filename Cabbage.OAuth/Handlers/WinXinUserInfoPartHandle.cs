using JetBrains.Annotations;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orchard.Environment.Extensions;
using Cabbage.OAuth.Models;

namespace Cabbage.OAuth.Handlers
{
    [OrchardFeature("Cabbage.OAuth.Weixin")]
    [UsedImplicitly]
    public class WinXinUserInfoPartHandle : ContentHandler
    {
        public WinXinUserInfoPartHandle(IRepository<WinXinUserInfoPartRecord> winXinUserInfoPartRecordRepository)
        {
            Filters.Add(new ActivatingFilter<WinXinUserInfoPart>("User"));
            OnInitialized<WinXinUserInfoPart>((context, part) => part.Record.Loader(
                () => winXinUserInfoPartRecordRepository
                .Fetch(x => x.UserId == context.ContentItem.Id).FirstOrDefault()));
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            var part = context.ContentItem.As<WinXinUserInfoPart>();

            if (part != null)
            {
                context.Metadata.DisplayText = part.nickname;
            }
        }
    }
}
