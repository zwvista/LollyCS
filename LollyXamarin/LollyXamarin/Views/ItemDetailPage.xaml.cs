using System.ComponentModel;
using Xamarin.Forms;

namespace LollyXamarin
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