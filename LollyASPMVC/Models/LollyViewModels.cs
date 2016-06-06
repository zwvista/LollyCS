using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LollyShared;

namespace LollyASPMVC.Models
{
    public class LollyViewModel
    {
        public int SelectedLangID { get; set; }
        public string SelectedDictName { get; set; }
        public string Word { get; set; }

        public List<SelectListItem> LangList =>
            LollyDB.Languages_GetDataNonChinese()
            .Select(r => new SelectListItem()
            {
                Value = r.LANGID.ToString(),
                Text = r.LANGNAME
            }).ToList();
        public string UrlByWord =>
            string.Format(
                LollyDB.DictAll_GetDataByLangDict(SelectedLangID, SelectedDictName).URL,
                HttpUtility.UrlEncode(Word));
    }
}