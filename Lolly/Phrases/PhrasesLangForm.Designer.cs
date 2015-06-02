using LollyBase;

namespace Lolly
{
    partial class PhrasesLangForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhrasesLangForm));
            this.bindingSource1 = new LLBindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.booknameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indexColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phraseColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.translationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(MPHRASELANG);
            this.bindingSource1.Sort = "BOOKNAME,UNIT,ORD";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.booknameColumn,
            this.unitColumn,
            this.partColumn,
            this.indexColumn,
            this.phraseColumn,
            this.translationColumn});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(0, 25);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(794, 446);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            // 
            // booknameColumn
            // 
            this.booknameColumn.DataPropertyName = "BOOKNAME";
            this.booknameColumn.HeaderText = "BOOKNAME";
            this.booknameColumn.Name = "booknameColumn";
            this.booknameColumn.ReadOnly = true;
            this.booknameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.booknameColumn.Width = 200;
            // 
            // unitColumn
            // 
            this.unitColumn.DataPropertyName = "UNIT";
            this.unitColumn.HeaderText = "UNIT";
            this.unitColumn.Name = "unitColumn";
            this.unitColumn.ReadOnly = true;
            this.unitColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.unitColumn.Width = 75;
            // 
            // partColumn
            // 
            this.partColumn.DataPropertyName = "PART";
            this.partColumn.HeaderText = "PART";
            this.partColumn.Name = "partColumn";
            this.partColumn.ReadOnly = true;
            this.partColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.partColumn.Width = 75;
            // 
            // indexColumn
            // 
            this.indexColumn.DataPropertyName = "ORD";
            this.indexColumn.HeaderText = "ORD";
            this.indexColumn.Name = "indexColumn";
            this.indexColumn.ReadOnly = true;
            this.indexColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.indexColumn.Width = 75;
            // 
            // phraseColumn
            // 
            this.phraseColumn.DataPropertyName = "PHRASE";
            this.phraseColumn.HeaderText = "PHRASE";
            this.phraseColumn.Name = "phraseColumn";
            this.phraseColumn.ReadOnly = true;
            this.phraseColumn.Width = 300;
            // 
            // translationColumn
            // 
            this.translationColumn.DataPropertyName = "TRANSLATION";
            this.translationColumn.HeaderText = "TRANSLATION";
            this.translationColumn.Name = "translationColumn";
            this.translationColumn.ReadOnly = true;
            this.translationColumn.Width = 300;
            // 
            // PhrasesLangForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(794, 471);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PhrasesLangForm";
            this.Text = "PhrasesLangForm";
            this.Load += new System.EventHandler(this.PhrasesLangForm_Load);
            this.Controls.SetChildIndex(this.dataGridView1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private LLBindingSource bindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn booknameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn partColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phraseColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn translationColumn;
    }
}