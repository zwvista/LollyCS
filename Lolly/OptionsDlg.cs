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
    public partial class OptionsDlg : Form
    {
        public OptionsDlg()
        {
            InitializeComponent();
        }

        private void OptionsDlg_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = Program.options;
        }
    }
}
