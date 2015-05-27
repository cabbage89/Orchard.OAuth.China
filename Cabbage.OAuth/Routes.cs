using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Routes;

namespace Cabbage.OAuth
{
    public abstract class Routes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes()) routes.Add(routeDescriptor);
        }

        public abstract IEnumerable<RouteDescriptor> GetRoutes();
    }

    [OrchardFeature("Cabbage.OAuth")]
    public class QQRoutes : Routes
    {
        public override IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]
            {
                new RouteDescriptor 
                {    
                    Priority = 10,
                    Route = new Route(
                        "QuickLogOn/QQAuth",
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"}, {"controller", "QQOAuth"}, {"action", "Auth"}, },
                        new RouteValueDictionary (),
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"} },
                        new MvcRouteHandler())
                }
            };
        }
    }

    [OrchardFeature("Cabbage.OAuth.WeiXin")]
    public class WeiXinRoutes : Routes
    {
        public override IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]
            {
                new RouteDescriptor 
                {    
                    Priority = 10,
                    Route = new Route(
                        "QuickLogOn/WeiXinAuth",
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"}, {"controller", "WeiXin"}, {"action", "Auth"}, },
                        new RouteValueDictionary (),
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"} },
                        new MvcRouteHandler())
                },
                new RouteDescriptor 
                {    
                    Priority = 10,
                    Route = new Route(
                        "wxtest",
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"}, {"controller", "WeiXin"}, {"action", "test"}, },
                        new RouteValueDictionary (),
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"} },
                        new MvcRouteHandler())
                }
            };
        }
    }

    [OrchardFeature("Cabbage.OAuth.Sina")]
    public class SinaRoutes : Routes
    {
        public override IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]
            {
                new RouteDescriptor 
                {    
                    Priority = 10,
                    Route = new Route(
                        "QuickLogOn/SinaAuth",
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"}, {"controller", "SinaOAuth"}, {"action", "Auth"}, },
                        new RouteValueDictionary (),
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"} },
                        new MvcRouteHandler())
                }
            };
        }
    }

    [OrchardFeature("Cabbage.OAuth.Renren")]
    public class RenrenRoutes : Routes
    {
        public override IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]
            {
                new RouteDescriptor 
                {    
                    Priority = 10,
                    Route = new Route(
                        "QuickLogOn/RenrenAuth",
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"}, {"controller", "RenrenOAuth"}, {"action", "Auth"}, },
                        new RouteValueDictionary (),
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"} },
                        new MvcRouteHandler())
                }
            };
        }
    }

    [OrchardFeature("Cabbage.OAuth.Kaixin")]
    public class KaixinRoutes : Routes
    {
        public override IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]
            {
                new RouteDescriptor 
                {    
                    Priority = 10,
                    Route = new Route(
                        "QuickLogOn/KaixinAuth",
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"}, {"controller", "KaixinOAuth"}, {"action", "Auth"}, },
                        new RouteValueDictionary (),
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"} },
                        new MvcRouteHandler())
                }
            };
        }
    }

    [OrchardFeature("Cabbage.OAuth.Douban")]
    public class DoubanRoutes : Routes
    {
        public override IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]
            {
                new RouteDescriptor 
                {    
                    Priority = 10,
                    Route = new Route(
                        "QuickLogOn/DoubanAuth",
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"}, {"controller", "DoubanOAuth"}, {"action", "Auth"}, },
                        new RouteValueDictionary (),
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"} },
                        new MvcRouteHandler())
                }
            };
        }
    }

    [OrchardFeature("Cabbage.OAuth.Baidu")]
    public class BaiduRoutes : Routes
    {
        public override IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]
            {
                new RouteDescriptor 
                {    
                    Priority = 10,
                    Route = new Route(
                        "QuickLogOn/BaiduAuth",
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"}, {"controller", "BaiduOAuth"}, {"action", "Auth"}, },
                        new RouteValueDictionary (),
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"} },
                        new MvcRouteHandler())
                }
            };
        }
    }

    [OrchardFeature("Cabbage.OAuth.Taobao")]
    public class TaobaoRoutes : Routes
    {
        public override IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]
            {
                new RouteDescriptor 
                {    
                    Priority = 10,
                    Route = new Route(
                        "QuickLogOn/TaobaoAuth",
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"}, {"controller", "TaobaoOAuth"}, {"action", "Auth"}, },
                        new RouteValueDictionary (),
                        new RouteValueDictionary { {"area", "Cabbage.OAuth"} },
                        new MvcRouteHandler())
                }
            };
        }
    }
}