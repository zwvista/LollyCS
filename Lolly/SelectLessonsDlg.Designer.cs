namespace Lolly
{
    partial class SelectLessonsDlg
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
            this.langComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bookComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lessonFromNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lessonToNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lessonInAllFromLabel = new System.Windows.Forms.Label();
            this.activeIncludedCheckBox = new System.Windows.Forms.CheckBox();
            this.toCheckBox = new System.Windows.Forms.CheckBox();
            this.lessonInAllToLabel = new System.Windows.Forms.Label();
            this.partFromComboBox = new System.Windows.Forms.ComboBox();
            this.partToComboBox = new System.Windows.Forms.ComboBox();
            this.okButton = new VistaButtonTest.VistaButton();
            this.cancelButton = new VistaButtonTest.VistaButton();
            ((System.ComponentModel.ISupportInitialize)(this.lessonFromNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lessonToNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Language:";
            // 
            // langComboBox
            // 
            this.langComboBox.DisplayMember = "CHNNAME";
            this.langComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.langComboBox.FormattingEnabled = true;
            this.langComboBox.Location = new System.Drawing.Point(99, 17);
            this.langComboBox.MaxDropDownItems = 10;
            this.langComboBox.Name = "langComboBox";
            this.langComboBox.Size = new System.Drawing.Size(261, 24);
            this.langComboBox.TabIndex = 1;
            this.langComboBox.ValueMember = "LANGID";
            this.langComboBox.SelectedValueChanged += new System.EventHandler(this.langComboBox_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 51);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Book:";
            // 
            // bookComboBox
            // 
            this.bookComboBox.DisplayMember = "BOOKNAME";
            this.bookComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bookComboBox.FormattingEnabled = true;
            this.bookComboBox.Location = new System.Drawing.Point(99, 51);
            this.bookComboBox.Name = "bookComboBox";
            this.bookComboBox.Size = new System.Drawing.Size(261, 24);
            this.bookComboBox.TabIndex = 3;
            this.bookComboBox.ValueMember = "BOOKID";
            this.bookComboBox.SelectedValueChanged += new System.EventHandler(this.bookComboBox_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 89);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Lessons:";
            // 
            // lessonFromNumericUpDown
            // 
            this.lessonFromNumericUpDown.Location = new System.Drawing.Point(99, 87);
            this.lessonFromNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lessonFromNumericUpDown.Name = "lessonFromNumericUpDown";
            this.lessonFromNumericUpDown.Size = new System.Drawing.Size(44, 26);
            this.lessonFromNumericUpDown.TabIndex = 5;
            this.lessonFromNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lessonFromNumericUpDown.ValueChanged += new System.EventHandler(this.lessonFromNumericUpDown_ValueChanged);
            // 
            // lessonToNumericUpDown
            // 
            this.lessonToNumericUpDown.Location = new System.Drawing.Point(99, 118);
            this.lessonToNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lessonToNumericUpDown.Name = "lessonToNumericUpDown";
            this.lessonToNumericUpDown.Size = new System.Drawing.Size(44, 26);
            this.lessonToNumericUpDown.TabIndex = 9;
            this.lessonToNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lessonToNumericUpDown.ValueChanged += new System.EventHandler(this.lessonToNumericUpDown_ValueChanged);
            // 
            // lessonInAllFromLabel
            // 
            this.lessonInAllFromLabel.AutoSize = true;
            this.lessonInAllFromLabel.Location = new System.Drawing.Point(146, 89);
            this.lessonInAllFromLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lessonInAllFromLabel.Name = "lessonInAllFromLabel";
            this.lessonInAllFromLabel.Size = new System.Drawing.Size(104, 16);
            this.lessonInAllFromLabel.TabIndex = 6;
            this.lessonInAllFromLabel.Text = "(100 in all)";
            // 
            // activeIncludedCheckBox
            // 
            this.activeIncludedCheckBox.AutoSize = true;
            this.activeIncludedCheckBox.Location = new System.Drawing.Point(13, 150);
            this.activeIncludedCheckBox.Name = "activeIncludedCheckBox";
            this.activeIncludedCheckBox.Size = new System.Drawing.Size(347, 20);
            this.activeIncludedCheckBox.TabIndex = 12;
            this.activeIncludedCheckBox.Text = "Also Change the Active Window\'s Settings";
            this.activeIncludedCheckBox.UseVisualStyleBackColor = true;
            // 
            // toCheckBox
            // 
            this.toCheckBox.AutoSize = true;
            this.toCheckBox.Location = new System.Drawing.Point(15, 117);
            this.toCheckBox.Name = "toCheckBox";
            this.toCheckBox.Size = new System.Drawing.Size(43, 20);
            this.toCheckBox.TabIndex = 8;
            this.toCheckBox.Text = "To";
            this.toCheckBox.UseVisualStyleBackColor = true;
            this.toCheckBox.CheckedChanged += new System.EventHandler(this.toCheckBox_CheckedChanged);
            // 
            // lessonInAllToLabel
            // 
            this.lessonInAllToLabel.AutoSize = true;
            this.lessonInAllToLabel.Location = new System.Drawing.Point(146, 121);
            this.lessonInAllToLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lessonInAllToLabel.Name = "lessonInAllToLabel";
            this.lessonInAllToLabel.Size = new System.Drawing.Size(104, 16);
            this.lessonInAllToLabel.TabIndex = 10;
            this.lessonInAllToLabel.Text = "(100 in all)";
            // 
            // partFromComboBox
            // 
            this.partFromComboBox.DisplayMember = "PARTNAME";
            this.partFromComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.partFromComboBox.FormattingEnabled = true;
            this.partFromComboBox.Location = new System.Drawing.Point(251, 89);
            this.partFromComboBox.Name = "partFromComboBox";
            this.partFromComboBox.Size = new System.Drawing.Size(109, 24);
            this.partFromComboBox.TabIndex = 7;
            this.partFromComboBox.ValueMember = "PARTID";
            this.partFromComboBox.SelectedIndexChanged += new System.EventHandler(this.partFromComboBox_SelectedIndexChanged);
            // 
            // partToComboBox
            // 
            this.partToComboBox.DisplayMember = "PARTNAME";
            this.partToComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.partToComboBox.FormattingEnabled = true;
            this.partToComboBox.Location = new System.Drawing.Point(251, 119);
            this.partToComboBox.Name = "partToComboBox";
            this.partToComboBox.Size = new System.Drawing.Size(109, 24);
            this.partToComboBox.TabIndex = 11;
            this.partToComboBox.ValueMember = "PARTID";
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
            this.okButton.Location = new System.Drawing.Point(43, 181);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 32);
            this.okButton.TabIndex = 13;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
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
            this.cancelButton.Location = new System.Drawing.Point(215, 181);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 32);
            this.cancelButton.TabIndex = 13;
            // 
            // SelectLessonsDlg
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(377, 232);
            this.ControlBox = false;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.toCheckBox);
            this.Controls.Add(this.activeIncludedCheckBox);
            this.Controls.Add(this.lessonToNumericUpDown);
            this.Controls.Add(this.lessonFromNumericUpDown);
            this.Controls.Add(this.partToComboBox);
            this.Controls.Add(this.partFromComboBox);
            this.Controls.Add(this.bookComboBox);
            this.Controls.Add(this.langComboBox);
            this.Controls.Add(this.lessonInAllToLabel);
            this.Controls.Add(this.lessonInAllFromLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectLessonsDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Lessons";
            this.Load += new System.EventHandler(this.SelectLessonsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lessonFromNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lessonToNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox langComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox bookComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown lessonFromNumericUpDown;
        private System.Windows.Forms.NumericUpDown lessonToNumericUpDown;
        private System.Windows.Forms.Label lessonInAllFromLabel;
        private System.Windows.Forms.CheckBox activeIncludedCheckBox;
        private System.Windows.Forms.CheckBox toCheckBox;
        private System.Windows.Forms.Label lessonInAllToLabel;
        private System.Windows.Forms.ComboBox partFromComboBox;
        private System.Windows.Forms.ComboBox partToComboBox;
        private VistaButtonTest.VistaButton okButton;
        private VistaButtonTest.VistaButton cancelButton;
    }
}