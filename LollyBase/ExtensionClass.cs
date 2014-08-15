using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LollyBase.Properties;
using mshtml;

namespace LollyBase
{
    public static class ExtensionClass
    {
        public const string NOTRANS = "<p style=\"color: #0000FF; font-weight: bold\">No translations were found.</p>";
        public static string ExtractFromHtml(string text, string transfrom)
        {
            var logFolder = Settings.Default.LogFolder + "\\";
            if (Debugger.IsAttached)
            {
                File.WriteAllText(logFolder + "0_raw.html", text);
                transfrom = File.ReadAllText(logFolder + "1_transform.txt");
            }
            var arr = transfrom.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var reg = new Regex(arr[0]);
            var match = reg.Match(text);
            if (match.Groups.Count < 2)
                return "";

            text = match.Groups[0].Value;
            Action<string> f = replacer =>
            {
                replacer = replacer.Replace(@"\r", "\r").Replace(@"\n", "\n");
                if (replacer == "<delete>")
                    replacer = "";
                text = reg.Replace(text, replacer);
            };

            f(arr[1]);
            if (Debugger.IsAttached)
                File.WriteAllText(logFolder + "2_extracted.txt", text);
            if (arr.Length > 2)
                for (int i = 2; i < arr.Length; )
                {
                    reg = new Regex(arr[i++]);
                    f(arr[i++]);
                }
            if (Debugger.IsAttached)
                File.WriteAllText(logFolder + "3_cooked.txt", text);
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
