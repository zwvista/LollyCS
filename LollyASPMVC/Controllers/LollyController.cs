using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LollyASPMVC.Models;
using LollyShared;
using System.Net;

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

        [HttpPost]
        public ActionResult DictList(LollyViewModel vm)
        {
            return Json(
                LollyDB.Dictionaries_GetDataByLang(vm.SelectedLangID)
                .Select(r => r.DICTNAME),
                JsonRequestBehavior.AllowGet
            );
        }

        [HttpPost]
        public ActionResult UrlByWord(LollyViewModel vm)
        {
            if(string.IsNullOrWhiteSpace(vm.Word))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Word Required.");
            return Content(vm.UrlByWord);
        }

        [HttpPost]
        public ActionResult Search(LollyViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Word))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Word Required.");
            return Redirect(vm.UrlByWord);
        }
    }
}