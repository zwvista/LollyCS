using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.Mapping;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using LollyShared;

namespace Lolly
{
    public partial class ConfigDictDlg : Form
    {
        private DictLangConfig config;
        public List<UIDict> uiDicts = new List<UIDict>();
        public ConfigDictDlg(DictLangConfig config, List<UIDict> uiDicts)
        {
            InitializeComponent();
            dictATreeView.ImageList = dictBTreeView.ImageList = sharedImageLists11.imageList1;
            this.config = config;
            this.uiDicts = uiDicts;
            FillTreeA();
            FillTreeB();
        }

        private TreeNode AddTreeNode(TreeNodeCollection nodes, string text, string type, int imageIndex)
        {
            var node = nodes.Add(text, text);
            node.ImageIndex = node.SelectedImageIndex = imageIndex;
            node.Tag = type;
            return node;
        }

        private void FillTreeA()
        {
            foreach (var grp in config.dictGroups)
            {
                var imageIndex = grp.Value[0].ImageIndex;
                if (imageIndex == DictImage.Offline || imageIndex == DictImage.Online || imageIndex == DictImage.Live)
                    imageIndex--;
                var node = AddTreeNode(dictATreeView.Nodes, grp.Key, grp.Key, (int)imageIndex);
                foreach (var item in grp.Value)
                {
                    AddTreeNode(node.Nodes, item.Name, grp.Key, (int)item.ImageIndex);
                    node.Expand();
                }
            }
        }

        private void FillTreeB()
        {
            foreach (var dict in uiDicts)
                if (dict is UIDictItem)
                {
                    var item = dict as UIDictItem;
                    AddTreeNode(dictBTreeView.Nodes, item.Name, item.Type, (int)item.ImageIndex);
                }
                else
                {
                    var col = dict as UIDictCollection;
                    var node = AddTreeNode(dictBTreeView.Nodes, col.Name,
                        col.IsPile ? "Pile" : "Switch", (int)DictImage.Special);
                    foreach (var item in col.Items)
                        AddTreeNode(node.Nodes, item.Name, item.Type, (int)item.ImageIndex);
                }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var nodes = dictATreeView.Nodes.Cast<TreeNode>()
                .SelectMany(n => n.Nodes.Cast<TreeNode>()
                .Where(n2 => n2.Checked)).ToList();
            if (nodes.IsEmpty()) return;
            if (nodes.Count == 1 || sender == addAllbutton)
                foreach (var node in nodes)
                    dictBTreeView.Nodes.Add((TreeNode)node.Clone());
            else
            {
                var nodesGroup = nodes.Select(n => n.Parent).Distinct().ToList();
                var type = sender == addPileButton ? "Pile" : "Switch";
                var name = (nodesGroup.Count == 1 ? nodesGroup[0].Name + "_" : "") + type;
                var node = AddTreeNode(dictBTreeView.Nodes, name, type,
                    (int)DictImage.Special);
                foreach (var node2 in nodes)
                    node.Nodes.Add((TreeNode)node2.Clone());
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            dictBTreeView.Nodes.Clear();
            FillTreeB();
        }

        private void WithSelectedNode(Action<TreeNode, int> action)
        {
            var node = dictBTreeView.SelectedNode;
            if (node == null || node.Level > 0) return;
            int n = dictBTreeView.Nodes.IndexOf(node);
            action(node, n);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            WithSelectedNode((node, n) => dictBTreeView.Nodes.Remove(node));
        }

        private void topButton_Click(object sender, EventArgs e)
        {
            WithSelectedNode((node, n) =>
            {
                if (n == 0) return;
                dictBTreeView.Nodes.Remove(node);
                dictBTreeView.Nodes.Insert(0, node);
                dictBTreeView.SelectedNode = node;
            });
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            WithSelectedNode((node, n) =>
            {
                if (n == 0) return;
                dictBTreeView.Nodes.Remove(node);
                dictBTreeView.Nodes.Insert(n - 1, node);
                dictBTreeView.SelectedNode = node;
            });
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            WithSelectedNode((node, n) =>
            {
                if (n == dictBTreeView.Nodes.Count) return;
                dictBTreeView.Nodes.Remove(node);
                dictBTreeView.Nodes.Insert(n + 1, node);
                dictBTreeView.SelectedNode = node;
            });
        }

        private void BottomButton_Click(object sender, EventArgs e)
        {
            WithSelectedNode((node, n) =>
            {
                if (n == dictBTreeView.Nodes.Count) return;
                dictBTreeView.Nodes.Remove(node);
                dictBTreeView.Nodes.Add(node);
                dictBTreeView.SelectedNode = node;
            });
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Func<TreeNode, UIDictItem> f = n => new UIDictItem
            {
                Name = n.Name,
                Type = (string)n.Tag,
                ImageIndex = (DictImage)n.ImageIndex
            };
            uiDicts = dictBTreeView.Nodes.Cast<TreeNode>()
                .Select(n => 
                    n.Nodes.Count == 0 ? (UIDict)f(n) :
                    new UIDictCollection
                    {
                        IsPile = (string)n.Tag == "Pile",
                        Name = n.Text,
                        Items = n.Nodes.Cast<TreeNode>().Select(f).ToList()
                    }
                ).ToList();
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

        private void dictBTreeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = e.Node.Level > 0;
        }
    }
}
