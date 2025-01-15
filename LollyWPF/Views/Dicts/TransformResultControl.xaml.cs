using CefSharp;
using CefSharp.Wpf;
using Hardcodet.Wpf.Util;
using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace LollyWPF
{
    /// <summary>
    /// TransformResultControl.xaml の相互作用ロジック
    /// </summary>
    public partial class TransformResultControl : UserControl
    {
        TransformEditViewModelWPF vm;
        public TransformResultControl(TransformEditViewModelWPF vm)
        {
            InitializeComponent();
            DataContext = this.vm = vm;
        }
        void wbDict_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue) return;
            vm.WhenAnyValue(x => x.ResultHtml).Subscribe(_ => Load());
        }
        void Load()
        {
            if (!wbDict.IsInitialized || string.IsNullOrEmpty(vm.ResultHtml)) return;
            wbDict.LoadHtml(vm.ResultHtml);
        }
    }
}
