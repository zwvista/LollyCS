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
        private string[] chineseNumbers = new []{
            // [0] = 0, [1] = 1 ... [9] = 9
            "零", "一", "二", "三", "四", "五", "六", "七", "八", "九",
            "十", "百", "千", "万", "亿"
        };

        private string[] japaneseNumbers = new []{
            // [0] = 0, [1] = 1 ... [9] = 9
            "ゼロ", "いち", "に", "さん", "よん", "ご", "ろく", "なな", "はち", "きゅう",
            // [10] = 10, [11] = 20 ... [18] = 90
            "じゅう", "にじゅう", "さんじゅう", "よんじゅう", "ごじゅう", "ろくじゅう", "ななじゅう", "はちじゅう", "きゅうじゅう",
            // [19] = 100, [20] = 200 ... [27] = 900
            "ひゃく", "にひゃく", "さんびゃく", "よんひゃく", "ごひゃく", "ろっぴゃく", "ななひゃく", "はっぴゃく", "きゅうひゃく",
            // [28] = 1000, [29] = 2000 ... [36] = 9000
            "せん", "にせん", "さんぜん", "よんせん", "ごせん", "ろくせん", "ななせん", "はっせん", "きゅうせん",
            // [37] = 10000
            "まん", "おく", "ちょう"
        };

        private string[] spanishNumbers = new []{
            // [0] = 0, [1] = 1 ... [9] = 9
            "", "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve",
            // [10] = 10, [11] = 11 ... [19] = 19
            "diez", "once", "doce", "trece", "catorce", "quince", "dieciséis", "diecisiete", "dieciocho", "diecinueve",
            // [20] = 20, [21] = 21 ... [29] = 29
            "veinte", "veintiuno", "veintidós", "veintitrés", "veinticuatro", "veinticinco", "veintiséis", "veintisiete", "veintiocho", "veintinueve",
            // [30] = 30, [31] = 40 ... [36] = 90
            "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa",
            // [37] = 100
            "ciento"
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
            switch (langComboBox.Text)
            {
                case "汉语": textBox1.Text = readNumberInChinese(n); break;
                case "日语": textBox1.Text = readNumberInJapanese(n); break;
                case "西班牙语": textBox1.Text = readNumberInSpanish(n); break;
            }
        }

        private string readNumberInChinese(int n)
        {
            n = n % 10000;
            string s = "";
            int[] a = new int[4];
            for (int i = 0; i < 4; i++)
            {
                a[i] = n % 10;
                n = n / 10;
            }
            for (int i = 3; i >= 0; i--)
                if(a[i] > 0)
                    s += japaneseNumbers[i * 9 + a[i]];
            return s;
        }

        private string readNumberInJapanese(int n)
        {
            Func<int, string> f = num =>
            {
                string str = "";
                for (int i = 0; i < 4; i++)
                {
                    int num2 = num % 10;
                    if (num2 > 0)
                        str = japaneseNumbers[i * 9 + num2] + str;
                    num = num / 10;
                }
                return str;
            };
            string s = "";
            for(int i = 0; i < 2; i++)
            {
                int n2 = n % 10000;
                n = n / 10000;
                s = f(n2) + (i > 0 && n2 > 0 ? japaneseNumbers[36 + i] : "") + s;
            }
            return s.IsEmpty() ? japaneseNumbers[0] : s;
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
