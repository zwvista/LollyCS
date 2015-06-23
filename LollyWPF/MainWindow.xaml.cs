using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using LollyShared;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace LollyWPF
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            Languages = new ObservableCollection<MLANGUAGE>(LollyDB.Languages_GetDataNonChinese());
            Word = "一人";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<MLANGUAGE> _Languages;
        public ObservableCollection<MLANGUAGE> Languages
        {
            get
            { return _Languages; }
            set
            {
                if (_Languages == value)
                    return;
                _Languages = value;
                NotifyPropertyChanged(nameof(Languages));
            }
        }

        private ObservableCollection<MDICTALL> _DictAll;
        public ObservableCollection<MDICTALL> DictAll
        {
            get
            { return _DictAll; }
            set
            {
                if (_DictAll == value)
                    return;
                _DictAll = value;
                NotifyPropertyChanged(nameof(DictAll));
            }
        }

        private string _Word;
        public string Word
        {
            get
            { return _Word; }
            set
            {
                if (_Word == value)
                    return;
                _Word = value;
                NotifyPropertyChanged(nameof(Word));
            }
        }

        private void LangComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var langId = (long)LangComboBox.SelectedValue;
            DictAll = new ObservableCollection<MDICTALL>(LollyDB.DictAll_GetDataByLang(langId));
        }
    }
}
