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
        public List<UIDict> uiDicts = new List<UIDict>();
        public ConfigDictDlg(DictLangConfig config, List<UIDict> uiDicts)
        {
            InitializeComponent();
            dictATreeView.ImageList = sharedImageLists11.imageList1;
            dictBTreeView.ImageList = sharedImageLists11.imageList1;
            this.config = config;
            this.uiDicts = uiDicts;
            FillDicts();
        }

        private TreeNode AddTreeNode(TreeNodeCollection nodes, string text, int imageIndex)
        {
            var node = nodes.Add(text, text);
            node.ImageIndex = node.SelectedImageIndex = imageIndex;
            return node;
        }

        private void FillDicts()
        {
            foreach (var grp in config.dictGroups)
            {
                var imageIndex = grp.Value[0].ImageIndex;
                if (imageIndex == DictImage.Offline || imageIndex == DictImage.Online || imageIndex == DictImage.Live)
                    imageIndex--;
                var node = AddTreeNode(dictATreeView.Nodes, grp.Key, (int)imageIndex);
                foreach (var item in grp.Value)
                {
                    AddTreeNode(node.Nodes, item.Name, (int)item.ImageIndex);
                    node.Expand();
                }
            }

            foreach (var dict in uiDicts)
                if (dict.Items.Count == 1)
                {
                    var item = dict.Items.First();
                    AddTreeNode(dictBTreeView.Nodes, item.Name, (int)item.ImageIndex);
                }
                else
                {
                    var node = AddTreeNode(dictBTreeView.Nodes, dict.Name, (int)DictImage.Special);
                    foreach (var item in dict.Items)
                        AddTreeNode(node.Nodes, item.Name, (int)item.ImageIndex);
                }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var nodes = dictATreeView.Nodes.Cast<TreeNode>()
                .SelectMany(n => n.Nodes.Cast<TreeNode>()
                .Where(n2 => n2.Checked)).ToList();
            if (!nodes.Any()) return;
            if (nodes.Count == 1 || sender == addAllbutton)
                foreach (var node in nodes)
                    dictBTreeView.Nodes.Add((TreeNode)node.Clone());
            else
            {
                var name = (sender as Button).Text;
                var node = AddTreeNode(dictBTreeView.Nodes, name, (int)DictImage.Special);
                foreach (var node2 in nodes)
                    node.Nodes.Add((TreeNode)node2.Clone());
            }
        }

        private void WithSelectedItem(Action<ListViewItem, int> action)
        {
            //var items = dictListView.SelectedItems;
            //if (items.Count == 0) return;
            //var item = items[0];
            //int n = dictListView.Items.IndexOf(item);
            //action(item, n);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            //WithSelectedItem((item, n) => dictListView.Items.Remove(item));
        }

        private void topButton_Click(object sender, EventArgs e)
        {
            //WithSelectedItem((item, n) =>
            //{
            //    if (n == 0) return;
            //    dictListView.Items.Remove(item);
            //    dictListView.Items.Insert(0, item);
            //});
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            //WithSelectedItem((item, n) =>
            //{
            //    if (n == 0) return;
            //    dictListView.Items.Remove(item);
            //    dictListView.Items.Insert(n - 1, item);
            //});
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            //WithSelectedItem((item, n) =>
            //{
            //    if (n == dictListView.Items.Count) return;
            //    dictListView.Items.Remove(item);
            //    dictListView.Items.Insert(n + 1, item);
            //});
        }

        private void BottomButton_Click(object sender, EventArgs e)
        {
            //WithSelectedItem((item, n) =>
            //{
            //    if (n == dictListView.Items.Count) return;
            //    dictListView.Items.Remove(item);
            //    dictListView.Items.Add(item);
            //});
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            //uiDicts = dictListView.Items.Cast<ListViewItem>()
            //    .Select(i => new UIDictGroup
            //    {
            //        Name = i.Name,
            //        ImageIndex = (DictImage) i.ImageIndex
            //    }).ToList();
        }

        private void dictATreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.Unknown) return;
            var node = e.Node;
            if (node.Level == 0)
                foreach (TreeNode node2 in node.Nodes)
                    node2.Checked = node.Checked;
            else
            {
                var node2 = node.Parent;
                node2.Checked = node2.Nodes.Cast<TreeNode>()
                    .All(n => n.Checked);
            }
        }
    }
}
