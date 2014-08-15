using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lolly
{
    public partial class ReindexDlg : Form
    {
        private ReindexObject[] objs;

        public ReindexDlg(ReindexObject[] objs)
        {
            this.objs = objs;
            InitializeComponent();
        }

        private void ReindexDlg_Load(object sender, EventArgs e)
        {
            itemsDragDropListBox.Items.AddRange(objs); 
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach(ReindexObject obj in itemsDragDropListBox.Items)
                obj.INDEX = ++i;
        }
    }
}
