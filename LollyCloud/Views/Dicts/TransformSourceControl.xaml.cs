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
    /// TransformSourceControl.xaml の相互作用ロジック
    /// </summary>
    public partial class TransformSourceControl : UserControl
    {
        TransformEditViewModel vm;
        public TransformSourceControl(TransformEditViewModel vm)
        {
            InitializeComponent();
            DataContext = this.vm = vm;
        }
        async void btnGetHtml_Click(object sender, RoutedEventArgs e)
        {
            vm.SourceText = await MainWindow.vmSettings.client.GetStringAsync(vm.SourceURL);
        }
    }
}
