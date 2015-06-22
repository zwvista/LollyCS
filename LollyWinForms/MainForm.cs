using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using LollyShared;

namespace LollyWinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            mLANGUAGEBindingSource.DataSource = LollyDB.Languages_GetDataNonChinese();
            langComboBox_SelectionChangeCommitted(null, null);
        }

        private void langComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            mDICTALLBindingSource.DataSource = LollyDB.DictAll_GetDataByLang((long)langComboBox.SelectedValue);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            var row = mDICTALLBindingSource.Current as MDICTALL;
            var url = string.Format(row.URL, HttpUtility.UrlEncode(wordTextBox.Text));
            dictWebBrowser.Navigate(url);
        }
    }
}
