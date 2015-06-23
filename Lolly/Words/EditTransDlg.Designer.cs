namespace Lolly
{
    partial class EditTransDlg
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cancelButton = new VistaButtonTest.VistaButton();
            this.okButton = new VistaButtonTest.VistaButton();
            this.translationTextBox = new LollyTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 386);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 53);
            this.panel1.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BaseColor = System.Drawing.Color.Transparent;
            this.cancelButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cancelButton.ButtonText = "Cancel";
            this.cancelButton.CornerRadius = 20;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(141)))), ((int)(((byte)(189)))));
            this.cancelButton.Location = new System.Drawing.Point(568, 9);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 32);
            this.cancelButton.TabIndex = 15;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.BaseColor = System.Drawing.Color.Transparent;
            this.okButton.ButtonColor = System.Drawing.Color.MediumBlue;
            this.okButton.ButtonText = "OK";
            this.okButton.CornerRadius = 20;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.GlowColor = System.Drawing.Color.Cyan;
            this.okButton.Location = new System.Drawing.Point(462, 9);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 32);
            this.okButton.TabIndex = 14;
            // 
            // translationTextBox
            // 
            this.translationTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.translationTextBox.Font = new System.Drawing.Font("SimSun", 12F);
            this.translationTextBox.Location = new System.Drawing.Point(0, 0);
            this.translationTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.translationTextBox.Multiline = true;
            this.translationTextBox.Name = "translationTextBox";
            this.translationTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.translationTextBox.Size = new System.Drawing.Size(680, 386);
            this.translationTextBox.TabIndex = 0;
            this.translationTextBox.WordWrap = false;
            // 
            // EditTransDlg
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(680, 439);
            this.Controls.Add(this.translationTextBox);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("SimSun", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditTransDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Translation";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public LollyTextBox translationTextBox;
        private VistaButtonTest.VistaButton cancelButton;
        private VistaButtonTest.VistaButton okButton;
    }
}