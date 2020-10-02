using Hardcodet.Wpf.Util;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace LollyCloud
{
    /// <summary>
    /// TransformInterimControl.xaml の相互作用ロジック
    /// </summary>
    public partial class TransformInterimControl : UserControl
    {
        public TransformInterimControl(TransformEditViewModelWPF vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
