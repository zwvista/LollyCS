using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LollyShared;
using System.Net;

namespace LollyASPWebForms
{
    public partial class Lolly : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private string UrlByWord()
        {
            var m = odsDictAll.Select().OfType<MDICTALL>().First();
            var url = string.Format(m.URL, HttpUtility.UrlEncode(txtWord.Text));
            return url;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWord.Text))
            {
                lblError.Text = "Word Required.";
                dictframe.Src = "about:blank";
            }
            else
            {
                lblError.Text = "";
                dictframe.Src = UrlByWord();
            }
        }

        protected void btnSearchRedirect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWord.Text))
            {
                lblError.Text = "Word Required.";
                dictframe.Src = "about:blank";
            }
            else
            {
                Response.Redirect(UrlByWord());
            }
        }
    }
}