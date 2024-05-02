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
    /// TransformIntermediateControl.xaml の相互作用ロジック
    /// </summary>
    public partial class TransformIntermediateControl : UserControl
    {
        public TransformIntermediateControl(TransformEditViewModelWPF vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
