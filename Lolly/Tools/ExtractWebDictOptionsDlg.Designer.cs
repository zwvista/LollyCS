namespace Lolly
{
    partial class ExtractWebDictOptionsDlg
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.bookUnitsRadioButton = new System.Windows.Forms.RadioButton();
            this.langRadioButton = new System.Windows.Forms.RadioButton();
            this.wordDataGridView = new System.Windows.Forms.DataGridView();
            this.checkBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.wordColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dictDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkSelectedWordsButton = new System.Windows.Forms.Button();
            this.uncheckSelectedWordsButton = new System.Windows.Forms.Button();
            this.checkSelectedDictsButton = new System.Windows.Forms.Button();
            this.uncheckSelectedDictsButton = new System.Windows.Forms.Button();
            this.overwriteCheckBox = new System.Windows.Forms.CheckBox();
            this.uncheckAllDictsButton = new System.Windows.Forms.Button();
            this.checkAllDictsButton = new System.Windows.Forms.Button();
            this.uncheckAllWordsButton = new System.Windows.Forms.Button();
            this.checkAllWordsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.wordDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dictDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(547, 458);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 13;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(634, 458);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 14;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // bookUnitsRadioButton
            // 
            this.bookUnitsRadioButton.AutoSize = true;
            this.bookUnitsRadioButton.Location = new System.Drawing.Point(6, 25);
            this.bookUnitsRadioButton.Name = "bookUnitsRadioButton";
            this.bookUnitsRadioButton.Size = new System.Drawing.Size(88, 16);
            this.bookUnitsRadioButton.TabIndex = 1;
            this.bookUnitsRadioButton.TabStop = true;
            this.bookUnitsRadioButton.Text = "radioButton1";
            this.bookUnitsRadioButton.UseVisualStyleBackColor = true;
            this.bookUnitsRadioButton.CheckedChanged += new System.EventHandler(this.unitsRadioButton_CheckedChanged);
            // 
            // langRadioButton
            // 
            this.langRadioButton.AutoSize = true;
            this.langRadioButton.Location = new System.Drawing.Point(429, 25);
            this.langRadioButton.Name = "langRadioButton";
            this.langRadioButton.Size = new System.Drawing.Size(88, 16);
            this.langRadioButton.TabIndex = 2;
            this.langRadioButton.TabStop = true;
            this.langRadioButton.Text = "radioButton1";
            this.langRadioButton.UseVisualStyleBackColor = true;
            this.langRadioButton.CheckedChanged += new System.EventHandler(this.langRadioButton_CheckedChanged);
            // 
            // wordDataGridView
            // 
            this.wordDataGridView.AllowUserToAddRows = false;
            this.wordDataGridView.AllowUserToDeleteRows = false;
            this.wordDataGridView.AllowUserToResizeRows = false;
            this.wordDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wordDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkBoxColumn,
            this.wordColumn});
            this.wordDataGridView.Location = new System.Drawing.Point(15, 152);
            this.wordDataGridView.Name = "wordDataGridView";
            this.wordDataGridView.RowHeadersVisible = false;
            this.wordDataGridView.RowTemplate.Height = 21;
            this.wordDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.wordDataGridView.Size = new System.Drawing.Size(327, 285);
            this.wordDataGridView.TabIndex = 6;
            // 
            // checkBoxColumn
            // 
            this.checkBoxColumn.HeaderText = "";
            this.checkBoxColumn.Name = "checkBoxColumn";
            this.checkBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.checkBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.checkBoxColumn.Width = 30;
            // 
            // wordColumn
            // 
            this.wordColumn.DataPropertyName = "WORD";
            this.wordColumn.HeaderText = "WORD";
            this.wordColumn.Name = "wordColumn";
            this.wordColumn.ReadOnly = true;
            this.wordColumn.Width = 200;
            // 
            // dictDataGridView
            // 
            this.dictDataGridView.AllowUserToAddRows = false;
            this.dictDataGridView.AllowUserToDeleteRows = false;
            this.dictDataGridView.AllowUserToResizeRows = false;
            this.dictDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dictDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1});
            this.dictDataGridView.Location = new System.Drawing.Point(348, 152);
            this.dictDataGridView.Name = "dictDataGridView";
            this.dictDataGridView.RowHeadersVisible = false;
            this.dictDataGridView.RowTemplate.Height = 21;
            this.dictDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dictDataGridView.Size = new System.Drawing.Size(363, 285);
            this.dictDataGridView.TabIndex = 11;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 30;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "DICTNAME";
            this.dataGridViewTextBoxColumn1.HeaderText = "DICTNAME";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.langRadioButton);
            this.groupBox1.Controls.Add(this.bookUnitsRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(694, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search for Words in";
            // 
            // checkSelectedWordsButton
            // 
            this.checkSelectedWordsButton.Location = new System.Drawing.Point(15, 118);
            this.checkSelectedWordsButton.Name = "checkSelectedWordsButton";
            this.checkSelectedWordsButton.Size = new System.Drawing.Size(151, 23);
            this.checkSelectedWordsButton.TabIndex = 3;
            this.checkSelectedWordsButton.Tag = "2";
            this.checkSelectedWordsButton.Text = "Check Selected";
            this.checkSelectedWordsButton.UseVisualStyleBackColor = true;
            this.checkSelectedWordsButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // uncheckSelectedWordsButton
            // 
            this.uncheckSelectedWordsButton.Location = new System.Drawing.Point(169, 118);
            this.uncheckSelectedWordsButton.Name = "uncheckSelectedWordsButton";
            this.uncheckSelectedWordsButton.Size = new System.Drawing.Size(168, 23);
            this.uncheckSelectedWordsButton.TabIndex = 5;
            this.uncheckSelectedWordsButton.Tag = "3";
            this.uncheckSelectedWordsButton.Text = "Uncheck Selected";
            this.uncheckSelectedWordsButton.UseVisualStyleBackColor = true;
            this.uncheckSelectedWordsButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // checkSelectedDictsButton
            // 
            this.checkSelectedDictsButton.Location = new System.Drawing.Point(347, 118);
            this.checkSelectedDictsButton.Name = "checkSelectedDictsButton";
            this.checkSelectedDictsButton.Size = new System.Drawing.Size(175, 23);
            this.checkSelectedDictsButton.TabIndex = 9;
            this.checkSelectedDictsButton.Tag = "6";
            this.checkSelectedDictsButton.Text = "Check Selected";
            this.checkSelectedDictsButton.UseVisualStyleBackColor = true;
            this.checkSelectedDictsButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // uncheckSelectedDictsButton
            // 
            this.uncheckSelectedDictsButton.Location = new System.Drawing.Point(522, 118);
            this.uncheckSelectedDictsButton.Name = "uncheckSelectedDictsButton";
            this.uncheckSelectedDictsButton.Size = new System.Drawing.Size(189, 23);
            this.uncheckSelectedDictsButton.TabIndex = 10;
            this.uncheckSelectedDictsButton.Tag = "7";
            this.uncheckSelectedDictsButton.Text = "Uncheck Selected";
            this.uncheckSelectedDictsButton.UseVisualStyleBackColor = true;
            this.uncheckSelectedDictsButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // overwriteCheckBox
            // 
            this.overwriteCheckBox.AutoSize = true;
            this.overwriteCheckBox.Location = new System.Drawing.Point(15, 458);
            this.overwriteCheckBox.Name = "overwriteCheckBox";
            this.overwriteCheckBox.Size = new System.Drawing.Size(275, 20);
            this.overwriteCheckBox.TabIndex = 12;
            this.overwriteCheckBox.Text = "Overwrite Existing Translations";
            this.overwriteCheckBox.UseVisualStyleBackColor = true;
            // 
            // uncheckAllDictsButton
            // 
            this.uncheckAllDictsButton.Location = new System.Drawing.Point(522, 89);
            this.uncheckAllDictsButton.Name = "uncheckAllDictsButton";
            this.uncheckAllDictsButton.Size = new System.Drawing.Size(189, 23);
            this.uncheckAllDictsButton.TabIndex = 8;
            this.uncheckAllDictsButton.Tag = "5";
            this.uncheckAllDictsButton.Text = "Uncheck All";
            this.uncheckAllDictsButton.UseVisualStyleBackColor = true;
            this.uncheckAllDictsButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // checkAllDictsButton
            // 
            this.checkAllDictsButton.Location = new System.Drawing.Point(347, 89);
            this.checkAllDictsButton.Name = "checkAllDictsButton";
            this.checkAllDictsButton.Size = new System.Drawing.Size(175, 23);
            this.checkAllDictsButton.TabIndex = 7;
            this.checkAllDictsButton.Tag = "4";
            this.checkAllDictsButton.Text = "Check All";
            this.checkAllDictsButton.UseVisualStyleBackColor = true;
            this.checkAllDictsButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // uncheckAllWordsButton
            // 
            this.uncheckAllWordsButton.Location = new System.Drawing.Point(169, 89);
            this.uncheckAllWordsButton.Name = "uncheckAllWordsButton";
            this.uncheckAllWordsButton.Size = new System.Drawing.Size(168, 23);
            this.uncheckAllWordsButton.TabIndex = 2;
            this.uncheckAllWordsButton.Tag = "1";
            this.uncheckAllWordsButton.Text = "Uncheck All";
            this.uncheckAllWordsButton.UseVisualStyleBackColor = true;
            this.uncheckAllWordsButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // checkAllWordsButton
            // 
            this.checkAllWordsButton.Location = new System.Drawing.Point(15, 89);
            this.checkAllWordsButton.Name = "checkAllWordsButton";
            this.checkAllWordsButton.Size = new System.Drawing.Size(151, 23);
            this.checkAllWordsButton.TabIndex = 1;
            this.checkAllWordsButton.Tag = "0";
            this.checkAllWordsButton.Text = "Check All";
            this.checkAllWordsButton.UseVisualStyleBackColor = true;
            this.checkAllWordsButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // ExtractWebDictOptionsDlg
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(729, 493);
            this.ControlBox = false;
            this.Controls.Add(this.uncheckAllDictsButton);
            this.Controls.Add(this.checkAllDictsButton);
            this.Controls.Add(this.uncheckAllWordsButton);
            this.Controls.Add(this.checkAllWordsButton);
            this.Controls.Add(this.overwriteCheckBox);
            this.Controls.Add(this.uncheckSelectedDictsButton);
            this.Controls.Add(this.checkSelectedDictsButton);
            this.Controls.Add(this.uncheckSelectedWordsButton);
            this.Controls.Add(this.checkSelectedWordsButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dictDataGridView);
            this.Controls.Add(this.wordDataGridView);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtractWebDictOptionsDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Extract Web Dictionary";
            this.Load += new System.EventHandler(this.ExtractWebDictOptionsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wordDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dictDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.RadioButton bookUnitsRadioButton;
        private System.Windows.Forms.RadioButton langRadioButton;
        private System.Windows.Forms.DataGridView wordDataGridView;
        private System.Windows.Forms.DataGridView dictDataGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button checkSelectedWordsButton;
        private System.Windows.Forms.Button uncheckSelectedWordsButton;
        private System.Windows.Forms.Button checkSelectedDictsButton;
        private System.Windows.Forms.Button uncheckSelectedDictsButton;
        private System.Windows.Forms.CheckBox overwriteCheckBox;
        private System.Windows.Forms.Button uncheckAllDictsButton;
        private System.Windows.Forms.Button checkAllDictsButton;
        private System.Windows.Forms.Button uncheckAllWordsButton;
        private System.Windows.Forms.Button checkAllWordsButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wordColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    }
}