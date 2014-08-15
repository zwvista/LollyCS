using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyTools.ViewModels
{
    class MainWindowViewModel
    {
        public Tool1ViewModel Tool1VM { get; set; }

        public MainWindowViewModel()
        {
            Tool1VM = new Tool1ViewModel();
        }
    }
}
