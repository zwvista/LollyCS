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
    /// TransformTemplateControl.xaml の相互作用ロジック
    /// </summary>
    public partial class TransformTemplateControl : UserControl
    {
        TransformEditViewModelWPF vm;
        public TransformTemplateControl(TransformEditViewModelWPF vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
