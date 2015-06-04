using LollyBase;

namespace Lolly
{
    partial class WordsAtWillEBForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordsAtWillEBForm));
            this.bindingSource1 = new LLBindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ordColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            // 
            // splitContainer2
            // 
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataGridView1);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(MWORDATWILL);
            this.bindingSource1.ListRowChanged = false;
            this.bindingSource1.Sort = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ordColumn,
            this.wordColumn,
            this.noteColumn});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(506, 634);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowValidated);
            // 
            // ordColumn
            // 
            this.ordColumn.DataPropertyName = "ORD";
            this.ordColumn.HeaderText = "ORD";
            this.ordColumn.Name = "ordColumn";
            this.ordColumn.Width = 75;
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
            // WordsAtWillEBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1028, 715);
            this.Name = "WordsAtWillEBForm";
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
        private System.Windows.Forms.DataGridViewTextBoxColumn ordColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wordColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteColumn;
    }
}
