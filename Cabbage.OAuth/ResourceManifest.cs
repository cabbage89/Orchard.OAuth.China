using Orchard.UI.Resources;

namespace Cabbage.OAuth
{
    public class ResourceManifest : IResourceManifestProvider 
    {
        public void BuildManifests(ResourceManifestBuilder builder) 
        {
            var manifest = builder.Add();

            //manifest.DefineScript("jQueryLabelify").SetUrl("jquery.labelify.js").SetVersion("1.3").SetDependencies("jQuery");
            //manifest.DefineScript("SmartresponderSubscriptionForm").SetUrl("sr.subscription.form.js").SetVersion("1.0").SetDependencies("jQueryLabelify");
            //manifest.DefineStyle("SmartresponderSubscriptionForm").SetUrl("sr.subscription.form.css");
        }
    }
}
