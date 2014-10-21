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
        public List<SelectListItem> LangList { get; set; }
        public string Word { get; set; }
    }
}