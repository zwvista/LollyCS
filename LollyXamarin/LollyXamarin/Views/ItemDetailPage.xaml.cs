using System.ComponentModel;
using Xamarin.Forms;
using LollyXamarin.ViewModels;

namespace LollyXamarin.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}