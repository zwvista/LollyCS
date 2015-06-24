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
            this.overwriteCheckBox = new System.Windows.Forms.CheckBox();
            this.checkAllWordsLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.checkNoneWordsLinkLabel = new System.Windows.Forms.LinkLabel();
            this.checkAllDictsLinkLabel = new System.Windows.Forms.LinkLabel();
            this.checkNoneDictsLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.checkSelectedWordsLinkLabel = new System.Windows.Forms.LinkLabel();
            this.uncheckSelectedWordsLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.checkSelectedDictsLinkLabel = new System.Windows.Forms.LinkLabel();
            this.uncheckSelectedDictsLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
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
            this.bookUnitsRadioButton.Size = new System.Drawing.Size(122, 20);
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
            this.langRadioButton.Size = new System.Drawing.Size(122, 20);
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
            this.wordDataGridView.Location = new System.Drawing.Point(15, 93);
            this.wordDataGridView.Name = "wordDataGridView";
            this.wordDataGridView.RowHeadersVisible = false;
            this.wordDataGridView.RowTemplate.Height = 21;
            this.wordDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.wordDataGridView.Size = new System.Drawing.Size(327, 344);
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
            this.dictDataGridView.Location = new System.Drawing.Point(348, 93);
            this.dictDataGridView.Name = "dictDataGridView";
            this.dictDataGridView.RowHeadersVisible = false;
            this.dictDataGridView.RowTemplate.Height = 21;
            this.dictDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dictDataGridView.Size = new System.Drawing.Size(363, 344);
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
            // checkAllWordsLinkLabel
            // 
            this.checkAllWordsLinkLabel.AutoSize = true;
            this.checkAllWordsLinkLabel.Location = new System.Drawing.Point(73, 73);
            this.checkAllWordsLinkLabel.Name = "checkAllWordsLinkLabel";
            this.checkAllWordsLinkLabel.Size = new System.Drawing.Size(32, 16);
            this.checkAllWordsLinkLabel.TabIndex = 15;
            this.checkAllWordsLinkLabel.TabStop = true;
            this.checkAllWordsLinkLabel.Tag = "0";
            this.checkAllWordsLinkLabel.Text = "All";
            this.checkAllWordsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.checkLinkLabel_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "Check:";
            // 
            // checkNoneWordsLinkLabel
            // 
            this.checkNoneWordsLinkLabel.AutoSize = true;
            this.checkNoneWordsLinkLabel.Location = new System.Drawing.Point(102, 73);
            this.checkNoneWordsLinkLabel.Name = "checkNoneWordsLinkLabel";
            this.checkNoneWordsLinkLabel.Size = new System.Drawing.Size(40, 16);
            this.checkNoneWordsLinkLabel.TabIndex = 15;
            this.checkNoneWordsLinkLabel.TabStop = true;
            this.checkNoneWordsLinkLabel.Tag = "1";
            this.checkNoneWordsLinkLabel.Text = "None";
            this.checkNoneWordsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.checkLinkLabel_LinkClicked);
            // 
            // checkAllDictsLinkLabel
            // 
            this.checkAllDictsLinkLabel.AutoSize = true;
            this.checkAllDictsLinkLabel.Location = new System.Drawing.Point(403, 73);
            this.checkAllDictsLinkLabel.Name = "checkAllDictsLinkLabel";
            this.checkAllDictsLinkLabel.Size = new System.Drawing.Size(32, 16);
            this.checkAllDictsLinkLabel.TabIndex = 15;
            this.checkAllDictsLinkLabel.TabStop = true;
            this.checkAllDictsLinkLabel.Tag = "4";
            this.checkAllDictsLinkLabel.Text = "All";
            this.checkAllDictsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.checkLinkLabel_LinkClicked);
            // 
            // checkNoneDictsLinkLabel
            // 
            this.checkNoneDictsLinkLabel.AutoSize = true;
            this.checkNoneDictsLinkLabel.Location = new System.Drawing.Point(431, 73);
            this.checkNoneDictsLinkLabel.Name = "checkNoneDictsLinkLabel";
            this.checkNoneDictsLinkLabel.Size = new System.Drawing.Size(40, 16);
            this.checkNoneDictsLinkLabel.TabIndex = 15;
            this.checkNoneDictsLinkLabel.TabStop = true;
            this.checkNoneDictsLinkLabel.Tag = "5";
            this.checkNoneDictsLinkLabel.Text = "None";
            this.checkNoneDictsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.checkLinkLabel_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(350, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "Check:";
            // 
            // checkSelectedWordsLinkLabel
            // 
            this.checkSelectedWordsLinkLabel.AutoSize = true;
            this.checkSelectedWordsLinkLabel.Location = new System.Drawing.Point(220, 74);
            this.checkSelectedWordsLinkLabel.Name = "checkSelectedWordsLinkLabel";
            this.checkSelectedWordsLinkLabel.Size = new System.Drawing.Size(48, 16);
            this.checkSelectedWordsLinkLabel.TabIndex = 15;
            this.checkSelectedWordsLinkLabel.TabStop = true;
            this.checkSelectedWordsLinkLabel.Tag = "2";
            this.checkSelectedWordsLinkLabel.Text = "Check";
            this.checkSelectedWordsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.checkLinkLabel_LinkClicked);
            // 
            // uncheckSelectedWordsLinkLabel
            // 
            this.uncheckSelectedWordsLinkLabel.AutoSize = true;
            this.uncheckSelectedWordsLinkLabel.Location = new System.Drawing.Point(266, 74);
            this.uncheckSelectedWordsLinkLabel.Name = "uncheckSelectedWordsLinkLabel";
            this.uncheckSelectedWordsLinkLabel.Size = new System.Drawing.Size(64, 16);
            this.uncheckSelectedWordsLinkLabel.TabIndex = 15;
            this.uncheckSelectedWordsLinkLabel.TabStop = true;
            this.uncheckSelectedWordsLinkLabel.Tag = "3";
            this.uncheckSelectedWordsLinkLabel.Text = "Uncheck";
            this.uncheckSelectedWordsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.checkLinkLabel_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Selected:";
            // 
            // checkSelectedDictsLinkLabel
            // 
            this.checkSelectedDictsLinkLabel.AutoSize = true;
            this.checkSelectedDictsLinkLabel.Location = new System.Drawing.Point(546, 73);
            this.checkSelectedDictsLinkLabel.Name = "checkSelectedDictsLinkLabel";
            this.checkSelectedDictsLinkLabel.Size = new System.Drawing.Size(48, 16);
            this.checkSelectedDictsLinkLabel.TabIndex = 15;
            this.checkSelectedDictsLinkLabel.TabStop = true;
            this.checkSelectedDictsLinkLabel.Tag = "6";
            this.checkSelectedDictsLinkLabel.Text = "Check";
            this.checkSelectedDictsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.checkLinkLabel_LinkClicked);
            // 
            // uncheckSelectedDictsLinkLabel
            // 
            this.uncheckSelectedDictsLinkLabel.AutoSize = true;
            this.uncheckSelectedDictsLinkLabel.Location = new System.Drawing.Point(592, 73);
            this.uncheckSelectedDictsLinkLabel.Name = "uncheckSelectedDictsLinkLabel";
            this.uncheckSelectedDictsLinkLabel.Size = new System.Drawing.Size(64, 16);
            this.uncheckSelectedDictsLinkLabel.TabIndex = 15;
            this.uncheckSelectedDictsLinkLabel.TabStop = true;
            this.uncheckSelectedDictsLinkLabel.Tag = "7";
            this.uncheckSelectedDictsLinkLabel.Text = "Uncheck";
            this.uncheckSelectedDictsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.checkLinkLabel_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(468, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "Selected:";
            // 
            // ExtractWebDictOptionsDlg
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(729, 493);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkNoneDictsLinkLabel);
            this.Controls.Add(this.checkAllDictsLinkLabel);
            this.Controls.Add(this.uncheckSelectedDictsLinkLabel);
            this.Controls.Add(this.checkSelectedDictsLinkLabel);
            this.Controls.Add(this.uncheckSelectedWordsLinkLabel);
            this.Controls.Add(this.checkSelectedWordsLinkLabel);
            this.Controls.Add(this.checkNoneWordsLinkLabel);
            this.Controls.Add(this.checkAllWordsLinkLabel);
            this.Controls.Add(this.overwriteCheckBox);
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
        private System.Windows.Forms.CheckBox overwriteCheckBox;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wordColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.LinkLabel checkAllWordsLinkLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel checkNoneWordsLinkLabel;
        private System.Windows.Forms.LinkLabel checkAllDictsLinkLabel;
        private System.Windows.Forms.LinkLabel checkNoneDictsLinkLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel checkSelectedWordsLinkLabel;
        private System.Windows.Forms.LinkLabel uncheckSelectedWordsLinkLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel checkSelectedDictsLinkLabel;
        private System.Windows.Forms.LinkLabel uncheckSelectedDictsLinkLabel;
        private System.Windows.Forms.Label label4;
    }
}