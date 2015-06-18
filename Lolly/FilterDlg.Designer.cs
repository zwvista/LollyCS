namespace Lolly
{
    partial class FilterDlg
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
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new VistaButtonTest.VistaButton();
            this.okButton = new VistaButtonTest.VistaButton();
            this.label2 = new System.Windows.Forms.Label();
            this.filterComboBox = new System.Windows.Forms.ComboBox();
            this.matchWholeWordsCheckBox = new System.Windows.Forms.CheckBox();
            this.wordsPhrasesRadioButton = new System.Windows.Forms.RadioButton();
            this.translationsRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter:";
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BaseColor = System.Drawing.Color.Transparent;
            this.cancelButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cancelButton.ButtonText = "Cancel";
            this.cancelButton.CornerRadius = 20;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(141)))), ((int)(((byte)(189)))));
            this.cancelButton.Location = new System.Drawing.Point(267, 157);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 32);
            this.cancelButton.TabIndex = 5;
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.BaseColor = System.Drawing.Color.Transparent;
            this.okButton.ButtonColor = System.Drawing.Color.MediumBlue;
            this.okButton.ButtonText = "OK";
            this.okButton.CornerRadius = 20;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.GlowColor = System.Drawing.Color.Cyan;
            this.okButton.Location = new System.Drawing.Point(144, 157);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 32);
            this.okButton.TabIndex = 4;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Find In:";
            // 
            // filterComboBox
            // 
            this.filterComboBox.FormattingEnabled = true;
            this.filterComboBox.Location = new System.Drawing.Point(79, 18);
            this.filterComboBox.Name = "filterComboBox";
            this.filterComboBox.Size = new System.Drawing.Size(288, 24);
            this.filterComboBox.TabIndex = 1;
            // 
            // matchWholeWordsCheckBox
            // 
            this.matchWholeWordsCheckBox.AutoSize = true;
            this.matchWholeWordsCheckBox.Checked = true;
            this.matchWholeWordsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.matchWholeWordsCheckBox.Location = new System.Drawing.Point(12, 121);
            this.matchWholeWordsCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.matchWholeWordsCheckBox.Name = "matchWholeWordsCheckBox";
            this.matchWholeWordsCheckBox.Size = new System.Drawing.Size(163, 20);
            this.matchWholeWordsCheckBox.TabIndex = 6;
            this.matchWholeWordsCheckBox.Text = "Match Whole Words";
            this.matchWholeWordsCheckBox.UseVisualStyleBackColor = true;
            // 
            // wordsPhrasesRadioButton
            // 
            this.wordsPhrasesRadioButton.AutoSize = true;
            this.wordsPhrasesRadioButton.Checked = true;
            this.wordsPhrasesRadioButton.Location = new System.Drawing.Point(12, 85);
            this.wordsPhrasesRadioButton.Name = "wordsPhrasesRadioButton";
            this.wordsPhrasesRadioButton.Size = new System.Drawing.Size(130, 20);
            this.wordsPhrasesRadioButton.TabIndex = 7;
            this.wordsPhrasesRadioButton.TabStop = true;
            this.wordsPhrasesRadioButton.Text = "Words/Phrases";
            this.wordsPhrasesRadioButton.UseVisualStyleBackColor = true;
            // 
            // translationsRadioButton
            // 
            this.translationsRadioButton.AutoSize = true;
            this.translationsRadioButton.Location = new System.Drawing.Point(245, 85);
            this.translationsRadioButton.Name = "translationsRadioButton";
            this.translationsRadioButton.Size = new System.Drawing.Size(122, 20);
            this.translationsRadioButton.TabIndex = 7;
            this.translationsRadioButton.Text = "Translations";
            this.translationsRadioButton.UseVisualStyleBackColor = true;
            // 
            // FilterDlg
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(379, 205);
            this.Controls.Add(this.translationsRadioButton);
            this.Controls.Add(this.wordsPhrasesRadioButton);
            this.Controls.Add(this.matchWholeWordsCheckBox);
            this.Controls.Add(this.filterComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Font = new System.Drawing.Font("SimSun", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VistaButtonTest.VistaButton cancelButton;
        private VistaButtonTest.VistaButton okButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox filterComboBox;
        private System.Windows.Forms.CheckBox matchWholeWordsCheckBox;
        private System.Windows.Forms.RadioButton wordsPhrasesRadioButton;
        private System.Windows.Forms.RadioButton translationsRadioButton;
    }
}