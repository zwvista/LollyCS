using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LollyCommon;
using Plugin.Clipboard;
using Xamarin.Essentials;

namespace LollyXamarin
{
    public partial class PhrasesReviewPage : ContentPage
    {

        public PhrasesReviewPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void ToolbarItemNewTest_Clicked(object sender, EventArgs e)
        {
        }
    }
}