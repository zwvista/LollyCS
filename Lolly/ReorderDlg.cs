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
    public partial class ReorderDlg : Form
    {
        private ReorderObject[] objs;

        public ReorderDlg(ReorderObject[] objs)
        {
            this.objs = objs;
            InitializeComponent();
        }

        private void ReorderDlg_Load(object sender, EventArgs e)
        {
            itemsDragDropListBox.Items.AddRange(objs);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach(ReorderObject obj in itemsDragDropListBox.Items)
                obj.ORD = ++i;
        }
    }
}
