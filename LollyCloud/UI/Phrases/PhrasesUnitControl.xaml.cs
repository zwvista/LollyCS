using Hardcodet.Wpf.Util;
using LollyShared;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// PhrasesUnitControl.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesUnitControl : PhrasesBaseControl
    {
        public PhrasesUnitViewModel vm { get; set; }
        public override SettingsViewModel vmSettings => vm.vmSettings;
        public override DataGrid dgPhrasesBase => dgPhrases;
        public override MPhraseInterface ItemForRow(int row) => vm.PhraseItems[row];

        public PhrasesUnitControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }
        public void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgPhrases_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dlg = new PhrasesUnitDetailDlg();
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = (sender as DataGridRow).Item as MUnitPhrase;
            dlg.vm = vm;
            dlg.ShowDialog();
        }

        void btnBatch_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PhrasesUnitBatchDlg();
            dlg.Owner = Window.GetWindow(this);
            dlg.vmBatch.vm = vm;
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PhrasesUnitDetailDlg();
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = vm.NewUnitPhrase();
            dlg.vm = vm;
            dlg.ShowDialog();
            vm.PhraseItems.Add(dlg.itemOriginal);
        }

        async void dgPhrases_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var item = vm.PhraseItems[e.Row.GetIndex()];
                await vm.Update(item);
            }
        }

        public override async Task OnSettingsChanged()
        {
            vm = new PhrasesUnitViewModel(MainWindow.vmSettings, inTextbook: true, needCopy: true);
            DataContext = vm;
            await base.OnSettingsChanged();
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgPhrases.SelectedIndex;
            if (row == -1) return;
            var item = vm.PhraseItems[row];
            await vm.Delete(item);
        }

        async void btnToggleToType_Click(object sender, RoutedEventArgs e)
        {
            var row = dgPhrases.SelectedIndex;
            var part = row == -1 ? vmSettings.Parts[0].Value : vm.PhraseItems[row].PART;
            await vmSettings.ToggleToType(part);
            vm.Reload();
        }
        async void btnPreviousUnitPart_Click(object sender, RoutedEventArgs e)
        {
            await vmSettings.PreviousUnitPart();
            vm.Reload();
        }
        async void btnNextUnitPart_Click(object sender, RoutedEventArgs e)
        {
            await vmSettings.NextUnitPart();
            vm.Reload();
        }

        #region DraggedPhraseItem

        /// <summary>
        /// DraggedPhraseItem Dependency Property
        /// </summary>
        public static readonly DependencyProperty DraggedPhraseItemProperty =
            DependencyProperty.Register("DraggedPhraseItem", typeof(MUnitPhrase), typeof(WordsUnitControl));

        /// <summary>
        /// Gets or sets the DraggedPhraseItem property.  This dependency property 
        /// indicates ....
        /// </summary>
        public MUnitPhrase DraggedPhraseItem
        {
            get { return (MUnitPhrase)GetValue(DraggedPhraseItemProperty); }
            set { SetValue(DraggedPhraseItemProperty, value); }
        }

        #endregion

        #region edit mode monitoring

        /// <summary>
        /// State flag which indicates whether the grid is in edit
        /// mode or not.
        /// </summary>
        public bool IsEditing { get; set; }

        void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            IsEditing = true;
            originalText = ((TextBlock)e.EditingEventArgs.Source).Text;
            //in case we are in the middle of a drag/drop operation, cancel it...
            if (IsDragging) ResetDragDrop();
        }

        async void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            IsEditing = false;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var text = ((TextBox)e.EditingElement).Text;
                if (text != originalText)
                {
                    var item = vm.PhraseItems[e.Row.GetIndex()];
                    await vm.Update(item);
                }
                dgPhrases.CancelEdit(DataGridEditingUnit.Row);
            }
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
            if (IsEditing || !vm.CanReorder) return;

            var row = UIHelpers.TryFindFromPoint<DataGridRow>((UIElement)sender, e.GetPosition(dgPhrases));
            if (row == null) return;

            //set flag that indicates we're capturing mouse movements
            IsDragging = true;
            DraggedPhraseItem = (MUnitPhrase)row.Item;
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
            MUnitPhrase targetItem = (MUnitPhrase)dgPhrases.SelectedItem;

            if (targetItem == null || !ReferenceEquals(DraggedPhraseItem, targetItem))
            {
                //remove the source from the list
                vm.PhraseItems.Remove(DraggedPhraseItem);

                //get target index
                var targetIndex = vm.PhraseItems.IndexOf(targetItem);

                //move source at the target's location
                vm.PhraseItems.Insert(targetIndex, DraggedPhraseItem);

                //select the dropped item
                dgPhrases.SelectedItem = DraggedPhraseItem;

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
            dgPhrases.IsReadOnly = false;
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
                dgPhrases.IsReadOnly = true;

                //make sure the popup is visible
                popup1.IsOpen = true;
            }


            Size popupSize = new Size(popup1.ActualWidth, popup1.ActualHeight);
            popup1.PlacementRectangle = new Rect(e.GetPosition(this), popupSize);

            //make sure the row under the grid is being selected
            Point position = e.GetPosition(dgPhrases);
            var row = UIHelpers.TryFindFromPoint<DataGridRow>(dgPhrases, position);
            if (row != null) dgPhrases.SelectedItem = row.Item;
        }
        public void dgWords_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && IsDragging)
            {
                ResetDragDrop();
                e.Handled = true;
            }
        }

        #endregion
    }
}
