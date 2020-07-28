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
    /// TransformStepControl.xaml の相互作用ロジック
    /// </summary>
    public partial class TransformStepControl : UserControl
    {
        public TransformStepControl(TransformEditViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
