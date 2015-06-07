using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LollyShared;

namespace Lolly
{
    public partial class ReadNumberDlg : Form
    {
        private string[] spanishNumbers = new []{
            "", "uno", "dos", "tres", "cuatro", "cinco",
            "seis", "siete", "ocho", "nueve", "diez",
            "once", "doce", "trece", "catorce", "quince",
            "dieciséis", "diecisiete", "dieciocho", "diecinueve",
            "veinte", "veintiuno", "veintidós", "veintitrés",
            "veinticuatro", "veinticinco", "veintiséis",
            "veintisiete", "veintiocho", "veintinueve",
            "treinta", "cuarenta", "cincuenta", "sesenta",
            "setenta", "ochenta", "noventa", "ciento"
        };

        public ReadNumberDlg()
        {
            InitializeComponent();
        }

        private void ReadNumberDlg_Load(object sender, EventArgs e)
        {
            langComboBox.DataSource = LollyDB.Languages_GetData();
            langComboBox.SelectedValue = Program.lbuSettings.LangID;
        }

        private void readNumberButton_Click(object sender, EventArgs e)
        {
            var n = (int)numericUpDown1.Value;
            textBox1.Text = readNumberInSpanish(n);
        }

        private string readNumberInSpanish(int n)
        {
            n = n % 100;
            string s;
            if (n < 30)
                s = spanishNumbers[n];
            else
            {
                int d1 = n / 10, d2 = n % 10;
                s = spanishNumbers[30 + d1 - 3];
                if (d2 != 0)
                    s += " y " + spanishNumbers[d2];
            }
            return s;
        }
    }
}
