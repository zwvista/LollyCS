using Dragablz;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LollyShared
{
    // https://stackoverflow.com/questions/43528152/how-to-close-tab-with-a-close-button-in-wpf
    // This class will be the Tab in the TabControl
    public class ActionTabItem
    {
        // This will be the text in the tab control
        public string Header { get; set; }
        // This will be the content of the tab control It is a UserControl whits you need to create manually
        public UserControl Content { get; set; }
    }
    // https://github.com/ButchersBoy/Dragablz/issues/13
    public class ActionInterTabClient : DefaultInterTabClient
    {
        public override TabEmptiedResponse TabEmptiedHandler(TabablzControl tabControl, Window window) =>
            TabEmptiedResponse.DoNothing;
    }
}
