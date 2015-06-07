using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LollyASPMVC.Models;
using LollyBase;

namespace LollyASPMVC.Controllers
{
    public class LollyController : Controller
    {
        // GET: Lolly
        public ActionResult Index()
        {
            var vm = new LollyViewModel()
            {
                Word = "一人"
            };
            return View(vm);
        }

        public ActionResult DictList(int langid)
        {
            return Json(
                LollyDB.Dictionaries_GetDataByLang(langid)
                .Select(r => r.DICTNAME)
                .OrderBy(r => r),
                JsonRequestBehavior.AllowGet
            );
        }

        [HttpPost]
        public ActionResult UrlByWord(LollyViewModel vm)
        {
            var m = LollyDB.DictAll_GetDataByLangDict(vm.SelectedLangID, vm.SelectedDictName);
            var url = string.Format(m.URL, HttpUtility.UrlEncode(vm.Word));
            return Content(url);
        }
    }
}