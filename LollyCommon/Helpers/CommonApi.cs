using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;

namespace LollyCommon
{
    public static class CommonApi
    {
        public static string LollyUrlAPI = "https://zwvista.com/lolly/api.php/records/";
        public static string LollyUrlSP = "https://zwvista.com/lolly/sp.php/";
        public static string CssFolder = "https://zwvista.com/lolly/css/";
        public static string UserId = "";

        public static void Google(this string str) =>
            Process.Start($"https://www.google.com/search?q={HttpUtility.UrlEncode(str)}");
        public static string Replace(this string str, Dictionary<string, string> dic)
        {
            var s = str;
            foreach (var kv in dic)
                s = s.Replace(kv.Key, kv.Value);
            return s;
        }

        // https://stackoverflow.com/questions/273313/randomize-a-listt
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // https://stackoverflow.com/questions/930433/apply-properties-values-from-one-object-to-another-of-the-same-type-automaticall
        /// <summary>
        /// Extension for 'Object' that copies the properties to a destination object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="ignoreProperties">The names of the properties to be ignored while copying.</param>
        public static void CopyProperties(this object source, object destination, params string[] ignoreProperties)
        {
            // If any this null throw an exception
            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null");
            // Getting the Types of the objects
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();
            // Collect all the valid properties to map
            var results = from srcProp in typeSrc.GetProperties()
                          where !ignoreProperties.Contains(srcProp.Name)
                          let targetProperty = typeDest.GetProperty(srcProp.Name)
                          where srcProp.CanRead
                          && targetProperty != null
                          && (targetProperty.GetSetMethod(true) != null && !targetProperty.GetSetMethod(true).IsPrivate)
                          && (targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) == 0
                          && targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType)
                          select new { sourceProperty = srcProp, targetProperty = targetProperty };
            //map the properties
            foreach (var props in results)
            {
                props.targetProperty.SetValue(destination, props.sourceProperty.GetValue(source, null), null);
            }
        }

        // https://stackoverflow.com/questions/50177352/is-there-a-way-to-track-when-reactive-command-finished-its-execution
        public static IObservable<bool> WhenFinishedExecuting<TParam, TResult>(this ReactiveCommand<TParam, TResult> cmd) =>
            cmd.IsExecuting
                .Skip(1) // IsExecuting has an initial value of false.  We can skip that first value
                .Where(isExecuting => !isExecuting); // filter until the executing state becomes false

        public static string SplitUsingCommaAndMerge(this IEnumerable<string> strs) =>
            string.Join(",", strs.SelectMany(s => (s ?? "").Split(',')).Where(s => s.Any()).OrderBy(s => s).Distinct());
    }
}
