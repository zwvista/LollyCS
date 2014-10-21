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
            var m = new LollyViewModel()
            {
                LangList = Languages.GetData()
                    .Select(r => new SelectListItem()
                    {
                        Value = r.LANGID.ToString(),
                        Text = r.LANGNAME
                    }).ToList(),
                Word = "一人"
            };
            return View(m);
        }
    }
}