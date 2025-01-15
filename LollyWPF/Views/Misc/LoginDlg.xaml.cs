using LollyCommon;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LollyWPF
{
    /// <summary>
    /// LoginDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class LoginDlg : Window
    {
        LoginViewModel vm = new();
        public LoginDlg()
        {
            InitializeComponent();
            DataContext = vm;
            // https://stackoverflow.com/questions/339620/how-do-i-remove-minimize-and-maximize-from-a-resizable-window-in-wpf
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
        }

        async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            vm.PASSWORD = passwordBox.Password;
            CommonApi.UserId = await vm.Login();
            if (string.IsNullOrEmpty(CommonApi.UserId))
                MessageBox.Show("Wrong username or password!", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                App.SaveUserId();
                Close();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CommonApi.UserId))
                Environment.Exit(0);
        }
    }
}
