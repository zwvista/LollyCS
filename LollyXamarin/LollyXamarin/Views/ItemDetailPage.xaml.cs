using System.ComponentModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

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