using Hardcodet.Wpf.Util;
using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace LollyWPF
{
    /// <summary>
    /// TransformSourceControl.xaml の相互作用ロジック
    /// </summary>
    public partial class TransformSourceControl : UserControl
    {
        TransformEditViewModelWPF vm;
        public TransformSourceControl(TransformEditViewModelWPF vm)
        {
            InitializeComponent();
            DataContext = this.vm = vm;
            vm.WhenAnyValue(x => x.SourceUrl).Where(s => !string.IsNullOrEmpty(s)).Subscribe(_ => Load());
        }
        void wbDict_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                Load();
        }
        void Load()
        {
            if (wbDict.IsInitialized)
                wbDict.Load(vm.SourceUrl);
        }
    }
}
