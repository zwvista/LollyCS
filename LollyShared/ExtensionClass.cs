//#define DEBUG_EXTRACT

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using mshtml;

namespace LollyShared
{
    public static class ExtensionClass
    {
        public const string NOTRANSLATION = "<p style=\"color: #0000FF; font-weight: bold\">No translations were found.</p>";
        private static readonly Dictionary<string, string> escapes = new Dictionary<string, string>()
        {
            {"<delete>", ""}, {@"\t", "\t"}, {@"\r", "\r"}, {@"\n", "\n"},
        };
        public static string ExtractFromHtml(string text, string transfrom)
        {
#if DEBUG_EXTRACT
            var logFolder = Settings.Default.LogFolder + "\\";
            File.WriteAllText(logFolder + "0_raw.html", text);
            transfrom = File.ReadAllText(logFolder + "1_transform.txt");
#endif
            var arr = transfrom.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var reg = new Regex(arr[0]);
            var match = reg.Match(text);
            if (match.Groups.Count < 2)
                return "";

            text = match.Groups[0].Value;
            Action<string> f = replacer =>
            {
                foreach (var entry in escapes)
                    replacer = replacer.Replace(entry.Key, entry.Value);
                text = reg.Replace(text, replacer);
            };

            f(arr[1]);
#if DEBUG_EXTRACT
            File.WriteAllText(logFolder + "2_extracted.txt", text);
#endif
            for (int i = 2; i < arr.Length; )
            {
                reg = new Regex(arr[i++]);
                f(arr[i++]);
            }
#if DEBUG_EXTRACT
            File.WriteAllText(logFolder + "3_cooked.txt", text);
#endif
            return text;
        }

        public static List<TConverted> ToNonAnonymousList<TOriginal, TConverted>(this List<TOriginal> originalList, List<TConverted> convertedList)
            where TOriginal : class
            where TConverted : class
        {
            //loop through the calling list:
            foreach (TOriginal item in originalList)
            {
                //convert each object of the list into TConverted object by calling extension ToType<TConverted>()
                //Add this object to newly created list:
                convertedList.Add(item.ToType<TConverted>());
            }
            //return List of TConverted objects:
            return convertedList;
        }

        public static TConverted ToType<TConverted>(this object originalObj)
            where TConverted : class
        {
            //create instance of TConverted type object:
            TConverted convertedObj = Activator.CreateInstance<TConverted>();

            //loop through the properties of the object you want to covert:          
            foreach (PropertyInfo pi in originalObj.GetType().GetProperties())
            {
                try
                {
                    //get the value of property and try to assign it to the property of TConverted type object:
                    convertedObj.GetType().GetProperty(pi.Name).SetValue(convertedObj, pi.GetValue(originalObj, null), null);
                }
                catch { }
            }
            //return the TConverted type object:         
            return convertedObj;
        }
    }
}
