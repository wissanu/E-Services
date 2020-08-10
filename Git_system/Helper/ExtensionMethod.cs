using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Git_system.Helper
{
    public static partial class ExtensionMethod
    {
        public static IDictionary<string, string> ToDictionary(this NameValueCollection source)
        {
            return source.Cast<string>().Select(s => new { Key = s, Value = source[s] }).ToDictionary(p => p.Key, p => p.Value);
        }
    }
}