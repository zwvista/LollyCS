using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lolly
{
    public partial class LollyBindingSource : BindingSource
    {
        public LollyBindingSource()
        {

        }

        public LollyBindingSource(IContainer container)
            : base(container)
        {
            
        }

        public LollyBindingSource(Object dataSource, string dataMember)
            : base(dataSource, dataMember)
        {
        }

        public event ListChangedEventHandler ListItemAdded;
        public event ListChangedEventHandler ListItemMoved;
        public event ListChangedEventHandler ListItemChanged;
        public event ListChangedEventHandler ListItemDeleted;
        public bool ListRowChanged { get; set; }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            base.OnListChanged(e);
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    OnListItemAdded(e);
                    break;
                case ListChangedType.ItemMoved:
                    OnListItemMoved(e);
                    break;
                case ListChangedType.ItemChanged:
                    OnListItemChanged(e);
                    break;
                case ListChangedType.ItemDeleted:
                    OnListItemDeleted(e);
                    break;
            }
        }

        protected override void OnPositionChanged(EventArgs e)
        {
            ListRowChanged = false;
            base.OnPositionChanged(e);
        }

        protected virtual void OnListItemAdded(ListChangedEventArgs e)
        {
            if(ListItemAdded != null)
                ListItemAdded(this, e);
        }

        protected virtual void OnListItemMoved(ListChangedEventArgs e)
        {
            if (ListItemMoved != null)
                ListItemMoved(this, e);
        }

        protected virtual void OnListItemChanged(ListChangedEventArgs e)
        {
            ListRowChanged = true;
            if (ListItemChanged != null)
                ListItemChanged(this, e);
        }

        protected virtual void OnListItemDeleted(ListChangedEventArgs e)
        {
            if (ListItemDeleted != null)
                ListItemDeleted(this, e);
        }
    }
}
