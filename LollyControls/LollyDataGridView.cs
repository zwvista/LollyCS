using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lolly
{
    public partial class LollyDataGridView : DataGridView
    {
        protected bool CanSelectRows { get; set; } = true;

        protected override void SetSelectedRowCore(int rowIndex, bool selected)
        {
            if (selected && CanSelectRows)
            {
                base.SetSelectedRowCore(rowIndex, selected);
            }
        }
    }
}
