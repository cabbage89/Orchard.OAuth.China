using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Configuration;
using Orchard;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Security;

namespace Cabbage.OAuth.Services
{
    public static class OAuthHelper
    {
        public static string Encrypt(this IEncryptionService service, string value)
        {
            return Convert.ToBase64String(service.Encode(Encoding.UTF8.GetBytes(value)));
        }

        public static string Decrypt(this IEncryptionService service, string value)
        {
            return Encoding.UTF8.GetString(service.Decode(Convert.FromBase64String(value)));
        }

        public static string HMACSHA1(string text, string passKey)
        {
            var provider = new System.Security.Cryptography.HMACSHA1(Encoding.ASCII.GetBytes(passKey));
            provider.Initialize();
            var binary = provider.ComputeHash(Encoding.ASCII.GetBytes(text));
            return Convert.ToBase64String(binary);
        }

        public static T FromJson<T>(Stream stream) where T : class
        {
            var js = new DataContractJsonSerializer(typeof(T));
            return js.ReadObject(stream) as T;
        }

        public static string ReadWebExceptionMessage(Exception ex)
        {
            var wex = ex as WebException;
            if (wex != null && wex.Response != null)
            {
                using (var stream = wex.Response.GetResponseStream())
                using (var sr = new StreamReader(stream))
                {
                    return sr.ReadToEnd();
                }
            }
            return null;
        }

        public static IWebProxy GetProxy()
        {
            var httpProxy = WebConfigurationManager.AppSettings["HttpProxy"];
            if (string.IsNullOrEmpty(httpProxy)) return null;
            var parts = httpProxy.Split(";".ToCharArray(), StringSplitOptions.None);
            var url = parts[0];
            var user = parts.Length > 2 ? parts[1] : null;
            var password = parts.Length > 3 ? parts[2] : null;
            var bypassList = parts.Length > 4 ? parts[3].Split(',') : null;

            var p = new WebProxy(url);
            if (bypassList != null && bypassList.Length > 0)
            {
                p.BypassProxyOnLocal = true;
                p.BypassList = bypassList;
            }
            if (string.IsNullOrEmpty(user))
            {
                p.Credentials = new NetworkCredential(user, password ?? string.Empty);
            }
            return p;
        }
    }
}
