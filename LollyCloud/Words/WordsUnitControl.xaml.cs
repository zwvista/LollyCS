using Hardcodet.Wpf.Util;
using LollyShared;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// WordsUnitControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsUnitControl : WordsBaseControl
    {
        public WordsUnitViewModel vm { get; set; }
        public override DataGrid dgWordsBase => dgWords;
        public override MWordInterface ItemForRow(int row) => vm.Items[row];
        public override SettingsViewModel vmSettings => vm.vmSettings;
        public override WebBrowser wbDictBase => wbDict;
        public override ToolBar ToolBarDictBase => ToolBarDict;

        public WordsUnitControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        public override void dgWords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!popup1.IsOpen)
                base.dgWords_SelectionChanged(sender, e);
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgWords_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dlg = new WordsUnitDetailDlg();
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = (sender as DataGridRow).Item as MUnitWord;
            dlg.vm = vm;
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new WordsUnitDetailDlg();
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = vm.NewUnitWord();
            dlg.vm = vm;
            dlg.ShowDialog();
            vm.Items.Add(dlg.itemOriginal);
        }

        async void dgWords_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var item = vm.Items[e.Row.GetIndex()];
                await vm.Update(item);
            }
        }

        public override async Task OnSettingsChanged()
        {
            vm = await WordsUnitViewModel.CreateAsync(MainWindow.vmSettings, inTextbook: true, needCopy: true);
            dgWords.ItemsSource = vm.Items;
            await base.OnSettingsChanged();
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgWords.SelectedIndex;
            if (row == -1) return;
            var item = vm.Items[row];
            await vm.Delete(item);
        }

        async void tbNewWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return || string.IsNullOrEmpty(vm.NewWord)) return;
            var item = vm.NewUnitWord();
            item.WORD = vmSettings.AutoCorrectInput(vm.NewWord);
            vm.NewWord = "";
            item.ID = await vm.Create(item);
            vm.Items.Add(item);
        }

        public async override Task LevelChanged(int row)
        {
            var item = vm.Items[row];
            await vmSettings.UpdateLevel(item.WORDID, item.LEVEL);
        }

        async void btnToggleToType_Click(object sender, RoutedEventArgs e)
        {
            var row = dgWords.SelectedIndex;
            var part = row == -1 ? vmSettings.Parts[0].Value : vm.Items[row].PART;
            await vmSettings.ToggleToType(part);
            btnRefresh_Click(sender, e);
        }
        async void btnPreviousUnitPart_Click(object sender, RoutedEventArgs e)
        {
            await vmSettings.PreviousUnitPart();
            btnRefresh_Click(sender, e);
        }
        async void btnNextUnitPart_Click(object sender, RoutedEventArgs e)
        {
            await vmSettings.NextUnitPart();
            btnRefresh_Click(sender, e);
        }

        #region DraggedWordItem

        /// <summary>
        /// DraggedWordItem Dependency Property
        /// </summary>
        public static readonly DependencyProperty DraggedWordItemProperty =
            DependencyProperty.Register("DraggedWordItem", typeof(MUnitWord), typeof(WordsUnitControl));

        /// <summary>
        /// Gets or sets the DraggedWordItem property.  This dependency property 
        /// indicates ....
        /// </summary>
        public MUnitWord DraggedWordItem
        {
            get { return (MUnitWord)GetValue(DraggedWordItemProperty); }
            set { SetValue(DraggedWordItemProperty, value); }
        }

        #endregion

        #region edit mode monitoring

        /// <summary>
        /// State flag which indicates whether the grid is in edit
        /// mode or not.
        /// </summary>
        public bool IsEditing { get; set; }

        private void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            IsEditing = true;
            //in case we are in the middle of a drag/drop operation, cancel it...
            if (IsDragging) ResetDragDrop();
        }

        private void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            IsEditing = false;
        }

        #endregion

        #region Drag and Drop Rows

        /// <summary>
        /// Keeps in mind whether
        /// </summary>
        public bool IsDragging { get; set; }

        /// <summary>
        /// Initiates a drag action if the grid is not in edit mode.
        /// </summary>
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsEditing) return;

            var row = UIHelpers.TryFindFromPoint<DataGridRow>((UIElement)sender, e.GetPosition(dgWords));
            if (row == null || row.IsEditing) return;

            //set flag that indicates we're capturing mouse movements
            IsDragging = true;
            DraggedWordItem = (MUnitWord)row.Item;
        }


        /// <summary>
        /// Completes a drag/drop operation.
        /// </summary>
        private async void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsDragging || IsEditing)
            {
                return;
            }

            //get the target item
            MUnitWord targetItem = (MUnitWord)dgWords.SelectedItem;

            if (targetItem == null || !ReferenceEquals(DraggedWordItem, targetItem))
            {
                //remove the source from the list
                vm.Items.Remove(DraggedWordItem);

                //get target index
                var targetIndex = vm.Items.IndexOf(targetItem);

                //move source at the target's location
                vm.Items.Insert(targetIndex, DraggedWordItem);

                //select the dropped item
                dgWords.SelectedItem = DraggedWordItem;

                await vm.Reindex(_ => { });
            }

            //reset
            ResetDragDrop();
        }


        /// <summary>
        /// Closes the popup and resets the
        /// grid to read-enabled mode.
        /// </summary>
        private void ResetDragDrop()
        {
            IsDragging = false;
            popup1.IsOpen = false;
            dgWords.IsReadOnly = false;
        }


        /// <summary>
        /// Updates the popup's position in case of a drag/drop operation.
        /// </summary>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!IsDragging || e.LeftButton != MouseButtonState.Pressed) return;

            //display the popup if it hasn't been opened yet
            if (!popup1.IsOpen)
            {
                //switch to read-only mode
                dgWords.IsReadOnly = true;

                //make sure the popup is visible
                popup1.IsOpen = true;
            }


            Size popupSize = new Size(popup1.ActualWidth, popup1.ActualHeight);
            popup1.PlacementRectangle = new Rect(e.GetPosition(this), popupSize);

            //make sure the row under the grid is being selected
            Point position = e.GetPosition(dgWords);
            var row = UIHelpers.TryFindFromPoint<DataGridRow>(dgWords, position);
            if (row != null) dgWords.SelectedItem = row.Item;
        }

        #endregion
    }
}
