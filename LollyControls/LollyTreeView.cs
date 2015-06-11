using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DllLolly
{
    // http://stackoverflow.com/questions/3174412/winforms-treeview-recursively-check-child-nodes-problem?rq=1
    public partial class LollyTreeView : TreeView
    {
        public LollyTreeView()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            // Filter WM_LBUTTONDBLCLK
            if (m.Msg != 0x203) base.WndProc(ref m);
        }
    }
}
