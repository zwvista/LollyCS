using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using LollyBase;

namespace Lolly
{
    public partial class ConfigDictDlg : Form
    {
        private DictLangConfig config;
        public List<DictInfo> dictsInUse = new List<DictInfo>();
        public ConfigDictDlg(DictLangConfig config, List<DictInfo> dictsInUse)
        {
            InitializeComponent();
            this.config = config;
            this.dictsInUse = dictsInUse;
            FillDicts();
        }
        private void FillDicts()
        {
            foreach (var grp in config.dictGroups)
            {
                var node = dictTreeView.Nodes.Add(grp.Key, grp.Key);
                var imageIndex = grp.Value[0].ImageIndex;
                if (imageIndex == DictImage.Offline || imageIndex == DictImage.Online || imageIndex == DictImage.Live)
                    imageIndex--;
                node.SelectedImageIndex = node.ImageIndex = (int)imageIndex;
                foreach (var dictInfo in grp.Value)
                {
                    var node2 = node.Nodes.Add(dictInfo.Name, dictInfo.Name);
                    node2.SelectedImageIndex = node2.ImageIndex = (int)dictInfo.ImageIndex;
                    node.Expand();
                }
            }
            foreach (var dictInfo in dictsInUse)
                dictListView.Items.Add(dictInfo.Name, dictInfo.Name, (int)dictInfo.ImageIndex);
            dictListView.Items[0].Selected = true;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var node = dictTreeView.SelectedNode;
            if (node.Level == 0) return;
            dictListView.Items.Add(node.Name, node.Name, node.ImageIndex);
        }

        private void WithSelectedItem(Action<ListViewItem, int> action)
        {
            var items = dictListView.SelectedItems;
            if (items.Count == 0) return;
            var item = items[0];
            int n = dictListView.Items.IndexOf(item);
            action(item, n);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            WithSelectedItem((item, n) => dictListView.Items.Remove(item));
        }

        private void topButton_Click(object sender, EventArgs e)
        {
            WithSelectedItem((item, n) =>
            {
                if (n == 0) return;
                dictListView.Items.Remove(item);
                dictListView.Items.Insert(0, item);
            });
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            WithSelectedItem((item, n) =>
            {
                if (n == 0) return;
                dictListView.Items.Remove(item);
                dictListView.Items.Insert(n - 1, item);
            });
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            WithSelectedItem((item, n) =>
            {
                if (n == dictListView.Items.Count) return;
                dictListView.Items.Remove(item);
                dictListView.Items.Insert(n + 1, item);
            });
        }

        private void BottomButton_Click(object sender, EventArgs e)
        {
            WithSelectedItem((item, n) =>
            {
                if (n == dictListView.Items.Count) return;
                dictListView.Items.Remove(item);
                dictListView.Items.Add(item);
            });
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            dictsInUse = dictListView.Items.Cast<ListViewItem>()
                .Select(i => new DictInfo
                {
                    Name = i.Name,
                    ImageIndex = (DictImage) i.ImageIndex
                }).ToList();
        }
    }
}
