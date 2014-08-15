using LollyBase;

namespace Lolly
{
    partial class WordsAtWillForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordsAtWillForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bindingSource1 = new LLBindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.indexColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wordColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noteColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.SplitterDistance = 379;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.SetKeyName(0, "Offline0");
            this.imageList1.Images.SetKeyName(1, "Offline1");
            this.imageList1.Images.SetKeyName(2, "Offline2");
            this.imageList1.Images.SetKeyName(3, "Offline3");
            this.imageList1.Images.SetKeyName(4, "Offline4");
            this.imageList1.Images.SetKeyName(5, "Offline5");
            this.imageList1.Images.SetKeyName(6, "Offline6");
            this.imageList1.Images.SetKeyName(7, "Offline7");
            this.imageList1.Images.SetKeyName(8, "Offline8");
            this.imageList1.Images.SetKeyName(9, "Offline9");
            this.imageList1.Images.SetKeyName(10, "Online0");
            this.imageList1.Images.SetKeyName(11, "Online1");
            this.imageList1.Images.SetKeyName(12, "Online2");
            this.imageList1.Images.SetKeyName(13, "Online3");
            this.imageList1.Images.SetKeyName(14, "Online4");
            this.imageList1.Images.SetKeyName(15, "Online5");
            this.imageList1.Images.SetKeyName(16, "Online6");
            this.imageList1.Images.SetKeyName(17, "Online7");
            this.imageList1.Images.SetKeyName(18, "Online8");
            this.imageList1.Images.SetKeyName(19, "Online9");
            this.imageList1.Images.SetKeyName(20, "Live0");
            this.imageList1.Images.SetKeyName(21, "Live1");
            this.imageList1.Images.SetKeyName(22, "Live2");
            this.imageList1.Images.SetKeyName(23, "Live3");
            this.imageList1.Images.SetKeyName(24, "Live4");
            this.imageList1.Images.SetKeyName(25, "Live5");
            this.imageList1.Images.SetKeyName(26, "Live6");
            this.imageList1.Images.SetKeyName(27, "Live7");
            this.imageList1.Images.SetKeyName(28, "Live8");
            this.imageList1.Images.SetKeyName(29, "Live9");
            this.imageList1.Images.SetKeyName(30, "Custom");
            this.imageList1.Images.SetKeyName(31, "Local");
            this.imageList1.Images.SetKeyName(32, "Special");
            this.imageList1.Images.SetKeyName(33, "Conjugator");
            this.imageList1.Images.SetKeyName(34, "Web");
            // 
            // wordTextBox
            // 
            this.wordTextBox.Size = new System.Drawing.Size(379, 26);
            // 
            // splitContainer2
            // 
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer2.Size = new System.Drawing.Size(379, 665);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(MWORDATWILL);
            this.bindingSource1.ListRowChanged = false;
            this.bindingSource1.Sort = "";
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Green;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.indexColumn,
            this.wordColumn,
            this.noteColumn});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(379, 634);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowValidated);
            // 
            // indexColumn
            // 
            this.indexColumn.DataPropertyName = "INDEX";
            this.indexColumn.HeaderText = "INDEX";
            this.indexColumn.Name = "indexColumn";
            this.indexColumn.Width = 75;
            // 
            // wordColumn
            // 
            this.wordColumn.DataPropertyName = "WORD";
            this.wordColumn.HeaderText = "WORD";
            this.wordColumn.Name = "wordColumn";
            this.wordColumn.Width = 150;
            // 
            // noteColumn
            // 
            this.noteColumn.DataPropertyName = "NOTE";
            this.noteColumn.HeaderText = "NOTE";
            this.noteColumn.Name = "noteColumn";
            this.noteColumn.Width = 150;
            // 
            // WordsAtWillForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 715);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WordsAtWillForm";
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private LLBindingSource bindingSource1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wordColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteColumn;
    }
}