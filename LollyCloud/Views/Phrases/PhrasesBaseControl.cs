﻿using Hardcodet.Wpf.Util;
using LollyCommon;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    public class PhrasesBaseControl : WordsPhrasesBaseControl
    {
        protected virtual DataGrid dgWordsBase => null;
        protected WordsLangViewModel vmWordsLang;
        protected override WordsBaseViewModel vmWords => vmWordsLang;
        MLangWord SelectedWordItem => (MLangWord)vmWordsLang.SelectedWordItem;
        public override async Task OnSettingsChanged()
        {
            await base.OnSettingsChanged();
            GetWords(0);
        }
        public void OnEndEditWord(object sender, DataGridCellEditEndingEventArgs e) =>
            OnEndEdit(sender, e, "WORD", async item => await vmWordsLang.Update((MLangWord)item));

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        public void dgWords_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UIHelpers.TryFindParent<DataGrid>((DataGridRow)sender).CancelEdit();
            miEditWord_Click(sender, null);
        }
        public void miEditWord_Click(object sender, RoutedEventArgs e)
        {
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new WordsLangDetailDlg(Window.GetWindow(this), vmWordsLang, SelectedWordItem);
            dlg.ShowDialog();
        }
        public override void GetWords(int phraseid) =>
            dgWordsBase.DataContext = vmWordsLang = new WordsLangViewModel(vmSettings, phraseid);
    }
}
