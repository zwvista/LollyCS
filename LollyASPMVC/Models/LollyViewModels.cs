using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LollyBase;

namespace LollyASPMVC.Models
{
    public class LollyViewModel
    {
        public int SelectedLangID { get; set; }

        public List<SelectListItem> LangList
        {
            get
            {
                return Languages.GetData()
                    .Select(r => new SelectListItem()
                    {
                        Value = r.LANGID.ToString(),
                        Text = r.LANGNAME
                    }).ToList();
            }
        }
        public string SelectedDictName { get; set; }
        public string Word { get; set; }
    }
}