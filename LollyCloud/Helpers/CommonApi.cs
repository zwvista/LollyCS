using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;

namespace LollyCloud
{
    public enum DictWebBrowserStatus
    {
        Ready, Navigating, Automating
    }
    public enum ReviewMode
    {
        ReviewAuto, Test, ReviewManual
    }
    public enum UnitPartToType
    {
        Unit, Part, To
    }
    public static class CommonApi
    {
        public static string LollyUrlAPI = "https://zwvista.tk/lolly/api.php/records/";
        public static string LollyUrlSP = "https://zwvista.tk/lolly/sp.php/";
        public static string CssFolder = "https://zwvista.tk/lolly/css/";
        public static int UserId = 1;

        public static void GoogleString(string str) =>
            Process.Start($"https://www.google.com/search?q={HttpUtility.UrlEncode(str)}");

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
    }

    // https://stackoverflow.com/questions/930433/apply-properties-values-from-one-object-to-another-of-the-same-type-automaticall
    public static class Reflection
    {
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
    }
}
