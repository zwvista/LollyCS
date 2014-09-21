using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using LollyBase;
using mshtml;

namespace Lolly
{
    public static class ControlExtensions
    {
        public static IHTMLDocument2 GetHTMLDoc(this WebBrowser wb)
        {
            return (IHTMLDocument2)wb.Document.DomDocument;
        }

        public static string ExtractFromWeb(this WebBrowser wb, string transfrom)
        {
            var doc = wb.GetHTMLDoc();
            var text = doc.body.parentElement.outerHTML;
            return ExtensionClass.ExtractFromHtml(text, transfrom);
        }

        public static string ExtractFromWeb(this WebBrowser wb, MDICTALL dictRow, string defaultString = "")
        {
            var text = wb.ExtractFromWeb(dictRow.TRANSFORM_WIN);
            return text == "" ? defaultString : text;
        }

        public static bool DoWebAutomation(this WebBrowser wb, string textAutomation, string word)
        {
            var doc = wb.GetHTMLDoc();
            //var text = doc.body.parentElement.outerHTML;
            var arr = textAutomation.Split(new[] { "\r\n" }, StringSplitOptions.None);
            foreach (var action in arr)
            {
                var arr2 = action.Split(',');
                var elemType = arr2[0];
                var elemAttrName = arr2[1];
                var ctrlAttr = arr2[2];
                IHTMLElement item;
                if (elemAttrName == "") // name or id
                    item = doc.all.item(ctrlAttr, 0) as IHTMLElement;
                else
                {
                    var all = from IHTMLElement i in doc.all select i;
                    item = (
                        elemAttrName == "class" ?
                            all.Where(i => i.className == ctrlAttr) :
                        elemAttrName == "title" ?
                            all.Where(i => i.title == ctrlAttr) :
                        elemAttrName == "href" ?
                            all.Where(i => i is HTMLAnchorElement &&
                            (i as HTMLAnchorElement).href != null &&
                            (i as HTMLAnchorElement).href.EndsWith(ctrlAttr)) :
                        null
                    ).FirstOrDefault();
                }
                if (item == null)
                    return false;
                switch (elemType)
                {
                    case "Anchor":
                    case "Button":
                        item.click();
                        break;
                    case "Input":
                        (item as HTMLInputElement).value = word;
                        break;
                    case "OptionButton":
                        (item as HTMLOptionButtonElement).value = arr2[3];
                        break;
                    case "Select":
                        (item as HTMLSelectElement).value = arr2[3];
                        break;
                }
            }
            return true;
        }

        public static void TryPerformClick(this ToolStripButton btn)
        {
            if (btn.Owner != null)
                btn.PerformClick();
        }

        public static void MoveToAddNew(this DataGridView dgv)
        {
            dgv.CurrentCell = dgv.Rows[dgv.Rows.Count - 1].Cells[dgv.CurrentCell == null ? 0 : dgv.CurrentCell.ColumnIndex];
        }
    }
}
