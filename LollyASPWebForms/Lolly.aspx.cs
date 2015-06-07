using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LollyShared;

namespace LollyASPWebForms
{
    public partial class Lolly : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var m = odsDictAll.Select().OfType<MDICTALL>().First();
            dictframe.Src = string.Format(m.URL, HttpUtility.UrlEncode(txtWord.Text));
        }
    }
}