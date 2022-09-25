using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace Inbox.Core.Extensions
{
    public static class NameValueCollectionExtension
    {
        /// <summary>
        /// 将名值集合转换成字符串，key1=value1&key2=value2
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToQueryString(this NameValueCollection source)
        {
            if (source is null)
                return string.Empty;

            var sb = new StringBuilder(1024);

            foreach (var key in source.AllKeys)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }
                sb.Append('&');
                sb.Append(WebUtility.UrlEncode(key));
                sb.Append('=');
                var val = source.Get(key);
                if (val != null)
                {
                    sb.Append(WebUtility.UrlEncode(val));
                }
            }

            return sb.Length > 0 ? sb.ToString(1, sb.Length - 1) : "";
        }
    }
}
